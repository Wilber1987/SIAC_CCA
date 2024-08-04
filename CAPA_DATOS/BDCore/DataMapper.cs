using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace CAPA_DATOS.BDCore.Abstracts
{
    public class WDataMapper
    {
        public WDataMapper(GDatosAbstract GDatos, BDQueryBuilderAbstract QueryBuilder)
        {
            this.GDatos = GDatos;
            this.QueryBuilder = QueryBuilder;
        }

        public GDatosAbstract GDatos { get; set; }
        public BDQueryBuilderAbstract QueryBuilder { get; set; }

        #region ORM INSERT, DELETE, UPDATES METHODS

        public object? InsertObject(EntityClass entity)
        {
            // Agrega un mensaje de registro indicando que se está insertando un objeto en la base de datos
            LoggerServices.AddMessageInfo("--> InsertObject(" + entity.GetType().Name + ")");

            // Obtiene las propiedades del objeto utilizando reflexión
            List<PropertyInfo> entityProps = entity.GetType().GetProperties().ToList();

            // Filtra las propiedades que tienen el atributo [PrimaryKey]
            List<PropertyInfo> primaryKeyProperties = entityProps.Where(p => Attribute.GetCustomAttribute(p, typeof(PrimaryKey)) != null).ToList();

            // Filtra las propiedades que tienen el atributo [ManyToOne]
            List<PropertyInfo> manyToOneProperties = entityProps.Where(p => Attribute.GetCustomAttribute(p, typeof(ManyToOne)) != null).ToList();

            // Establece los valores de las claves externas para las relaciones ManyToOne
            SetManyToOneProperties(entity, manyToOneProperties);


            // Construye la consulta de inserción y los parámetros correspondientes
            (string? strQuery, List<IDbDataParameter>? parameters) = QueryBuilder.BuildInsertQueryByObjectParameters(entity);

            // Si no se pudo construir la consulta, retorna nulo
            if (strQuery == null)
            {
                return null;
            }
            // Ejecuta la consulta SQL para insertar el objeto en la base de datos
            object? idGenerated = GDatos?.ExcuteSqlQuery(strQuery, parameters);

            // Si solo hay una propiedad de clave primaria y es autoincremental, establece su valor generado automáticamente
            if (primaryKeyProperties.Count == 1)
            {
                PrimaryKey? pkInfo = (PrimaryKey?)Attribute.GetCustomAttribute(primaryKeyProperties[0], typeof(PrimaryKey));
                if (pkInfo?.Identity == true)
                {
                    Type? pkType = Nullable.GetUnderlyingType(primaryKeyProperties[0].PropertyType);
                    primaryKeyProperties[0].SetValue(entity, Convert.ChangeType(idGenerated, pkType));
                }
            }

            // Filtra las propiedades que tienen el atributo [OneToOne]
            List<PropertyInfo> oneToOneProperties = entityProps.Where(p => Attribute.GetCustomAttribute(p, typeof(OneToOne)) != null).ToList();

            // Itera sobre las propiedades OneToOne
            foreach (var oneToOneProp in oneToOneProperties)
            {
                // Obtiene el nombre y el valor de la propiedad
                string? attributeName = oneToOneProp.Name;
                var attributeValue = oneToOneProp.GetValue(entity);

                // Si el valor no es nulo
                if (attributeValue != null)
                {
                    // Obtiene la información de la relación OneToOne
                    OneToOne? oneToOne = (OneToOne?)Attribute.GetCustomAttribute(oneToOneProp, typeof(OneToOne));

                    // Obtiene las propiedades de la clave primaria y extranjera
                    PropertyInfo? keyColumn = entity?.GetType().GetProperty(oneToOne?.KeyColumn);
                    PropertyInfo? foreignKeyColumn = attributeValue.GetType().GetProperty(oneToOne?.ForeignKeyColumn);

                    // Si la columna de clave externa existe
                    if (foreignKeyColumn != null)
                    {
                        // Obtiene el valor de la clave primaria y lo asigna a la columna de clave externa
                        var primaryKeyValue = entity?.GetType()?.GetProperty(keyColumn?.Name)?.GetValue(entity);
                        foreignKeyColumn.SetValue(attributeValue, primaryKeyValue);

                        // Inserta el objeto relacionado
                        InsertObject((EntityClass)attributeValue);
                    }
                }
            }

            // Filtra las propiedades que tienen el atributo [OneToMany]
            List<PropertyInfo> oneToManyProperties = entityProps.Where(p => Attribute.GetCustomAttribute(p, typeof(OneToMany)) != null).ToList();

            // Itera sobre las propiedades OneToMany
            foreach (var oneToManyProp in oneToManyProperties)
            {
                // Obtiene el nombre y el valor de la propiedad
                string? attributeName = oneToManyProp.Name;
                var attributeValue = oneToManyProp.GetValue(entity);

                // Si el valor no es nulo
                if (attributeValue != null)
                {
                    // Obtiene la información de la relación OneToMany
                    OneToMany? oneToMany = (OneToMany?)Attribute.GetCustomAttribute(oneToManyProp, typeof(OneToMany));

                    // Itera sobre los elementos de la colección
                    foreach (var value in ((IEnumerable)attributeValue))
                    {
                        // Obtiene las propiedades de la clave primaria y extranjera
                        PropertyInfo? keyColumn = entity?.GetType().GetProperty(oneToMany?.KeyColumn);
                        PropertyInfo? foreignKeyColumn = value.GetType().GetProperty(oneToMany?.ForeignKeyColumn);

                        // Si la columna de clave externa existe
                        if (foreignKeyColumn != null)
                        {
                            // Obtiene el valor de la clave primaria y llama a InsertRelationatedObject para insertar el objeto relacionado
                            var primaryKeyValue = entity?.GetType()?.GetProperty(keyColumn?.Name)?.GetValue(entity);
                            InsertRelationatedObject(primaryKeyValue, (EntityClass)value, foreignKeyColumn);
                        }
                    }
                }
            }

            // Retorna el objeto
            return entity;

        }
        /*Este método SetManyToOneProperties se encarga de establecer las propiedades de las relaciones de muchos a uno (ManyToOne) en el objeto entity.
		Este método es responsable de manejar las relaciones de muchos a uno, asegurando que las claves externas se establezcan correctamente y, 
		si es necesario, insertando objetos relacionados que aún no estén en la base de datos.
		*/
        private void SetManyToOneProperties(EntityClass entity, List<PropertyInfo> manyToOneProps)
        {
            // Verifica si la lista de propiedades ManyToOne es nula
            if (manyToOneProps == null) return;

            // Itera sobre las propiedades ManyToOne
            foreach (var manyToOneProp in manyToOneProps)
            {
                // Obtiene el valor de la propiedad ManyToOne en el objeto entity
                var attributeValue = manyToOneProp.GetValue(entity);

                // Si el valor de la propiedad no es nulo
                if (attributeValue != null)
                {
                    // Obtiene la información del atributo ManyToOne
                    ManyToOne? manyToOne = (ManyToOne?)Attribute.GetCustomAttribute(manyToOneProp, typeof(ManyToOne));

                    // Obtiene las propiedades de la clave primaria y externa
                    PropertyInfo? keyColumn = attributeValue.GetType().GetProperty(manyToOne?.KeyColumn);
                    PropertyInfo? foreignKeyColumn = entity.GetType().GetProperty(manyToOne?.ForeignKeyColumn);

                    // Si la columna de clave primaria no es nula
                    if (keyColumn != null)
                    {
                        // Si el valor de la clave primaria en el objeto relacionado es nulo, inserta el objeto relacionado
                        if (keyColumn?.GetValue(attributeValue) == null)
                        {
                            this.InsertObject((EntityClass)attributeValue);
                        }
                    }

                    // Si tanto la columna de clave primaria como la externa no son nulas
                    if (keyColumn != null && foreignKeyColumn != null)
                    {
                        // Obtiene la propiedad de la clave externa en el objeto entity
                        var foreignKey = entity.GetType().GetProperty(foreignKeyColumn.Name);

                        // Obtiene el valor de la clave primaria del objeto relacionado
                        var keyValue = attributeValue?.GetType()?.GetProperty(keyColumn?.Name)?.GetValue(attributeValue);

                        // Si el valor de la clave primaria del objeto relacionado no es nulo, lo asigna a la clave externa en entity
                        if (keyValue != null)
                        {
                            foreignKey?.SetValue(entity, keyValue);
                        }
                    }
                }
            }
        }

        /*Este método asegura que el objeto relacionado se inserte en la base de datos correctamente. Primero, establece la clave externa 
		en el objeto relacionado. Luego, verifica si todas las propiedades de la clave primaria tienen valores. Si es así, actualiza 
		el objeto en la base de datos; de lo contrario, lo inserta como un nuevo registro.*/
        private void InsertRelationatedObject(object foreignKeyValue, EntityClass entity, PropertyInfo foreignKeyColumn)
        {
            // Agrega un mensaje de registro indicando que se está insertando un objeto relacionado
            LoggerServices.AddMessageInfo("--> InsertRelationatedObject(" + entity.GetType().Name + "): ");

            // Establece el valor de la clave externa en el objeto relacionado
            foreignKeyColumn.SetValue(entity, foreignKeyValue);

            // Obtiene las propiedades del objeto relacionado
            List<PropertyInfo> entityProps = entity.GetType().GetProperties().ToList();

            // Filtra las propiedades que tienen el atributo [PrimaryKey]
            var primaryKeyProperties = entityProps.Where(p => (PrimaryKey?)Attribute.GetCustomAttribute(p, typeof(PrimaryKey)) != null).ToList();

            // Filtra las propiedades que tienen un valor no nulo de la clave primaria
            var values = primaryKeyProperties.Where(p => p.GetValue(entity) != null).ToList();

            // Si todas las propiedades de la clave primaria tienen valores, actualiza el objeto, de lo contrario, lo inserta
            if (primaryKeyProperties.Count == values.Count)
            {
                UpdateObject(entity, primaryKeyProperties.Select(p => p.Name).ToArray());
            }
            else
            {
                this.InsertObject(entity);
            }
        }

        /*Este método es responsable de manejar la actualización de un objeto en la base de datos. Primero, establece los valores
		de las claves externas para las relaciones ManyToOne. Luego, construye la consulta de actualización y la ejecuta.
		Finalmente, itera sobre las propiedades OneToMany para manejar las relaciones y actualiza o inserta los objetos
		relacionados según sea necesario.*/
        public object? UpdateObject(EntityClass entity, string[] IdObject)
        {
            // Agrega un mensaje de registro indicando que se está actualizando un objeto
            LoggerServices.AddMessageInfo("--> UpdateObject(EntityClass Inst, string[] IdObject)");

            // Obtiene las propiedades del objeto utilizando reflexión
            List<PropertyInfo> entityProps = entity.GetType().GetProperties().ToList();

            // Filtra las propiedades que tienen el atributo [PrimaryKey]
            List<PropertyInfo> primaryKeyProperties = entityProps.Where(p => Attribute.GetCustomAttribute(p, typeof(PrimaryKey)) != null).ToList();

            // Filtra las propiedades que tienen el atributo [ManyToOne]
            List<PropertyInfo> manyToOneProperties = entityProps.Where(p => Attribute.GetCustomAttribute(p, typeof(ManyToOne)) != null).ToList();

            // Establece los valores de las claves externas para las relaciones ManyToOne
            SetManyToOneProperties(entity, manyToOneProperties);

            // Construye la consulta de actualización y los parámetros correspondientes
            (string? strQuery, List<IDbDataParameter>? parameters) = QueryBuilder.BuildUpdateQueryByObject(entity, IdObject);

            // Filtra las propiedades que tienen el atributo [OneToMany]
            List<PropertyInfo> oneToManyProperties = entityProps.Where(p => Attribute.GetCustomAttribute(p, typeof(OneToMany)) != null).ToList();

            // Itera sobre las propiedades OneToMany
            foreach (var oneToManyProp in oneToManyProperties)
            {
                string? attributeName = oneToManyProp.Name;
                var attributeValue = oneToManyProp.GetValue(entity);
                if (attributeValue != null)
                {
                    // Obtiene la información del atributo OneToMany
                    OneToMany? oneToMany = (OneToMany?)Attribute.GetCustomAttribute(oneToManyProp, typeof(OneToMany));

                    // Itera sobre los elementos de la colección en el objeto relacionado
                    foreach (var value in (IEnumerable)attributeValue)
                    {
                        // Obtiene las propiedades de la clave primaria y externa en el objeto relacionado
                        PropertyInfo? keyColumn = entity?.GetType().GetProperty(oneToMany?.KeyColumn);
                        PropertyInfo? foreignKeyColumn = value.GetType().GetProperty(oneToMany?.ForeignKeyColumn);

                        // Si la columna de clave externa existe
                        if (foreignKeyColumn != null)
                        {
                            // Obtiene el valor de la clave primaria del objeto principal y llama a InsertRelationatedObject para insertar el objeto relacionado
                            var primaryKeyValue = entity?.GetType()?.GetProperty(keyColumn?.Name)?.GetValue(entity);
                            InsertRelationatedObject(primaryKeyValue, (EntityClass)value, foreignKeyColumn);
                        }
                    }
                }
            }

            // Si la consulta de actualización no es nula, ejecuta la consulta SQL
            if (strQuery != null)
            {
                GDatos?.ExcuteSqlQuery(strQuery, parameters);
            }

            // Retorna el objeto actualizado
            return entity;
        }
        /*Este método UpdateObject parece ser una versión simplificada de la función UpdateObject 
		que acepta un solo identificador (en lugar de un array de identificadores como en la versión anterior). 
		
		*Este método verifica si la propiedad de identificación especificada no es nula. Si es nula, lanza una 
		excepción indicando que no se puede realizar la actualización. Luego, construye la consulta de actualización 
		y la ejecuta, retornando el resultado de la ejecución de la consulta.*/
        public object? UpdateObject(EntityClass Inst, string IdObject)
        {
            // Agrega un mensaje de registro indicando que se está actualizando un objeto
            LoggerServices.AddMessageInfo("--> UpdateObject(EntityClass Inst, string IdObject)");

            // Verifica si el valor de la propiedad de identificación es nulo
            if (Inst.GetType().GetProperty(IdObject)?.GetValue(Inst) == null)
            {
                // Lanza una excepción si el valor de la propiedad de identificación es nulo
                throw new Exception("El valor de la propiedad "
                    + IdObject + " en la instancia "
                    + Inst.GetType().Name + " es nulo y no se puede actualizar.");
            }

            // Construye la consulta de actualización y los parámetros correspondientes
            (string? strQuery, List<IDbDataParameter>? parameters) = QueryBuilder.BuildUpdateQueryByObject(Inst, IdObject);

            // Ejecuta la consulta SQL y retorna el resultado
            return GDatos?.ExcuteSqlQuery(strQuery, parameters);
        }
        public object? Delete(EntityClass Inst)
        {
            LoggerServices.AddMessageInfo("-- > Delete(EntityClass Inst)");
            (string? strQuery, List<IDbDataParameter>? parameters) = QueryBuilder.BuildDeleteQuery(Inst);
            return GDatos?.ExcuteSqlQuery(strQuery, parameters);
        }

        #endregion

        #region LECTURA DE OBJETOS
        /*Este método TakeList<T> se encarga de obtener una lista de objetos del tipo T de la base de datos, 
		utilizando una instancia de un objeto Inst como referencia y opcionalmente aplicando una condición SQL adicional.
		
		*Este método es útil para recuperar conjuntos de datos de la
		base de datos y convertirlos en una lista de objetos del tipo especificado por T. La construcción de
		la tabla de datos y la conversión a objetos se realizan utilizando otros métodos internos
		(BuildTable y AdapterUtil.ConvertDataTable). La condición SQL opcional proporciona flexibilidad 
		para filtrar los resultados según sea necesario.*/
        public List<T> TakeList<T>(EntityClass Inst, bool fullEntity, string CondSQL = "")
        {
            // Construye una tabla de datos utilizando la instancia proporcionada y, opcionalmente, la condición SQL
            DataTable? Table = BuildTable(Inst, ref CondSQL, fullEntity, false);

            // Convierte la tabla de datos en una lista de objetos del tipo T
            List<T> ListD = AdapterUtil.ConvertDataTable<T>(Table, Inst);

            // Retorna la lista de objetos
            return ListD;
        }
        // public List<T> TakeList<T>(EntityClass Inst, string queryString)
        // {
        // 	DataTable Table = GDatos?.TraerDatosSQL(queryString);
        // 	List<T> ListD = AdapterUtil.ConvertDataTable<T>(Table, Inst);
        // 	return ListD;
        // }
        /*
		Este método TakeObject<T> se utiliza para obtener un único objeto del tipo T de la base de datos, 
		utilizando una instancia de un objeto Inst como referencia y opcionalmente aplicando una condición 
		SQL adicional. 
		*Este método es útil para recuperar un único objeto de la base de datos que cumpla con una determinada 
		condición. La construcción de la consulta SELECT y la ejecución de la consulta se realizan internamente. 
		Si la consulta no incluye una cláusula WHERE,
		se lanza una excepción, ya que se espera que la consulta devuelva un solo objeto y no una lista. 
		Si la consulta devuelve filas, la primera fila se convierte en un objeto del tipo T y se devuelve.
		Si la consulta no devuelve filas, se retorna el valor predeterminado para el tipo T, 
		que normalmente es null. Si ocurre algún error durante el proceso, se registra el error y se relanza la excepción.
		*/
        public T? TakeObject<T>(EntityClass Inst, string CondSQL = "")
        {
            // Construye la consulta SELECT utilizando la instancia proporcionada y, opcionalmente, la condición SQL
            (string queryString, string queryCount, List<IDbDataParameter>? parameters) = QueryBuilder.BuildSelectQuery(Inst, CondSQL, true, true);

            try
            {
                // Verifica si la consulta contiene la cláusula WHERE, ya que es necesaria para obtener un solo objeto
                if (!queryString.ToUpper().Contains(" WHERE "))
                {
                    // Si la consulta no contiene la cláusula WHERE, lanza una excepción
                    throw new Exception($"No es posible buscar el objeto. La entidad {Inst.GetType().Name} requiere filtros o parámetros con valores para hacer la comparación.");
                }

                // Ejecuta la consulta SQL para obtener los datos
                DataTable? Table = GDatos?.TraerDatosSQL(queryString, parameters);

                // Si la tabla contiene filas, convierte la primera fila en un objeto del tipo T
                if (Table?.Rows.Count != 0)
                {
                    var CObject = AdapterUtil.ConvertRow<T>(Inst, Table?.Rows[0]);
                    return CObject;
                }
                else
                {
                    // Si la tabla está vacía, retorna el valor predeterminado para el tipo T (normalmente null)
                    return default;
                }
            }
            catch (System.Exception e)
            {
                // Si ocurre un error durante el proceso, registra el error y relanza la excepción
                GDatos?.ReStartData(e);
                LoggerServices.AddMessageError($"ERROR: TakeList - {Inst.GetType().Name} - {queryString}", e);
                throw;
            }
        }
        /*Este método BuildTable se encarga de construir una tabla de datos basada en una instancia de un objeto Inst, 
		opcionalmente aplicando una condición SQL adicional (CondSQL), y determinando si se debe recuperar la entidad 
		completa o no, así como si es una operación de búsqueda (isFind). 
		*
		Este método es utilizado para preparar la consulta SQL y obtener los datos de la base de datos. Toma en cuenta 
		los parámetros proporcionados para determinar qué datos se deben recuperar y cómo se debe construir la consulta.
		Si la consulta se ejecuta correctamente, se retorna una tabla de datos que contiene los resultados. Si ocurre
		algún error durante el proceso, se registra el error y se relanza la excepción para ser manejada en niveles 
		superiores.*/
        /*
		* Interfaz de tipo que permitirá retornar una lista del tipo específico de la entidad 
		* que correspondería, en lugar de una lista genérica.
		*
		* A diferencia del anterior de paginación, acá se reciben los dos string de SQL directo para ejecutarse
		* (Raw SQL).
		Este método TakeListPaginated<T> se utiliza para obtener una lista paginada de objetos del tipo T. 
		Toma una instancia de un objeto Inst como referencia, así como las consultas SQL directas queryString y 
		queryCount para recuperar los datos paginados y el total de registros, respectivamente. 
		
		*Este método es útil para obtener una lista paginada de objetos 
		del tipo T, junto con el número total de registros que coinciden con la consulta. Se realiza la conversión
		 de los datos obtenidos en la tabla de datos a una lista de objetos del tipo T. Si ocurre algún error 
		 durante el proceso, se registra el error y se relanza la excepción para ser manejada en niveles superiores.
		*/
        public void TakeListPaginated<T>(EntityClass Inst, string queryString, string queryCount, out List<T> data, out int totalRecordsQuery, List<IDbDataParameter>? parameters)
        {
            try
            {
                // Construye la tabla de datos paginada y obtiene el número total de registros
                (DataTable Table, int totalRecords) = GDatos.BuildTablePaginated(queryString, queryCount, parameters);

                // Convierte la tabla de datos en una lista de objetos del tipo T
                List<T> ListD = AdapterUtil.ConvertDataTable<T>(Table, Inst);

                // Asigna los valores obtenidos a los parámetros de salida
                data = ListD;
                totalRecordsQuery = totalRecords;
            }
            catch (Exception e)
            {
                // Si ocurre un error durante el proceso, registra el error y relanza la excepción
                GDatos?.ReStartData(e);
                LoggerServices.AddMessageError($"ERROR: BuildTablePaginated {queryString}", e);
                throw;
            }
        }

        public List<T> TakeListWithProcedure<T>(StoreProcedureClass Inst, List<Object> Params)
        {
            try
            {
                DataTable? Table = GDatos?.ExecuteProcedureWithSQL(Inst, Params);
                List<T> ListD = AdapterUtil.ConvertDataTable<T>(Table, Inst);
                return ListD;
            }
            catch (Exception e)
            {
                GDatos?.ReStartData(e);
                LoggerServices.AddMessageError("ERROR: TakeListWithProcedure", e);
                throw;
            }
        }
        /*Solo lo usa Elvis*/

        public DataTable? BuildTable(EntityClass Inst, ref string CondSQL, bool fullEntity = true, bool isFind = true)
        {
            // Construye la consulta SELECT utilizando la instancia proporcionada y, opcionalmente, la condición SQL
            (string queryString, string queryCount, List<IDbDataParameter>? parameters) = QueryBuilder.BuildSelectQuery(Inst, CondSQL, fullEntity, isFind);

            try
            {
                // Ejecuta la consulta SQL para obtener los datos y construye la tabla de datos
                DataTable? Table = GDatos?.TraerDatosSQL(queryString, parameters);
                // Retorna la tabla de datos
                return Table;
            }
            catch (Exception e)
            {
                // Si ocurre un error durante el proceso, registra el error y relanza la excepción
                GDatos?.ReStartData(e);
                LoggerServices.AddMessageError($"ERROR: BuildTable - {Inst.GetType().Name} - {queryString}", e);
                throw;
            }
        }
        /*
        * Utlizado para la lectura de los datos
        */
        protected (DataTable, int)? BuildTablePaginated(EntityClass Inst, ref string CondSQL, int pageNum, int pageSize, string orderBy, string orderDir,
            bool fullEntity = true, bool isFind = true)
        {
            (string queryString, string queryCount, List<IDbDataParameter>? parameters) = QueryBuilder.BuildSelectQueryPaginated(Inst, CondSQL, pageNum, pageSize, orderBy, orderDir, fullEntity, isFind);
            return GDatos?.BuildTablePaginated(queryString, queryCount, parameters);
        }
        #endregion
    }

    public enum SqlEnumType
    {
        SQL_SERVER, POSTGRES_SQL, MYSQL
    }
}