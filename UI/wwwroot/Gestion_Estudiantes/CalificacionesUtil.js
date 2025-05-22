import { Calificacion_Group } from "../Model/Estudiantes.js";
const HeaderEvaluacionesCompletas = ["I BIMESTRE", "II BIMESTRE", "I SEMESTRE", "III BIMESTRE", "IV BIMESTRE", "II SEMESTRE", "FINAL"];
//@ts-check
export class CalificacionesUtil {
    /**
    * @param {Array< Asignatura_Group|Estudiante_Group>} Dataset
    * @param {number} [maxDetails]
    * @param {Array<String>|null} [maxDetailsHeaders]
    */
    static UpdateCalificaciones(Dataset, maxDetails, maxDetailsHeaders) {
        //console.log(instance.Calificaciones.length, maxDetails);  
        const evaluaciones = [];

        Dataset.forEach(instance => {
            if (maxDetails && instance.Calificaciones.length < maxDetails) {
                let isFirstOrder = instance.Calificaciones[0]?.Order == 1
                if (maxDetailsHeaders == null) {
                    for (let index = 0; index <= (maxDetails - (instance.Calificaciones.length + 1)); index++) {
                        if (isFirstOrder) {
                            instance.Calificaciones.push(new Calificacion_Group({ Evaluacion: "", Resultado: "-", TipoColumna: "Relleno" }));
                        } else {
                            instance.Calificaciones.unshift(new Calificacion_Group({ Evaluacion: "", Resultado: "-", TipoColumna: "Relleno" }));
                        }
                    }
                }
                const indexF = instance.Calificaciones.findIndex(calificacion => calificacion.Evaluacion === "F");

                if (indexF !== -1) {
                    // Sacar el objeto del array
                    const objetoF = instance.Calificaciones.splice(indexF, 1)[0];
                    // Insertar el objeto al final del array
                    instance.Calificaciones.push(objetoF);
                }
            }
            function toRoman(num) {
                const roman = ['I', 'II', 'III', 'IV', 'V', 'VI', 'VII', 'VIII', 'IX', 'X'];
                return roman[num - 1];
            }

            const counters = {};
            // Mapear sobre las calificaciones para modificar la propiedad "Evaluacion"
            const updatedCalificaciones = instance.Calificaciones.map(calificacion => {
                calificacion.Parciales = calificacion.evaluacion;
                const letra = (calificacion.Evaluacion == null
                    || calificacion.Evaluacion == ""
                    || calificacion.Evaluacion == undefined)
                    && calificacion.TipoColumna != "Relleno" ? "E" : calificacion.Evaluacion;

                // Incrementar el contador para la letra correspondiente
                if (!counters[letra]) {
                    counters[letra] = 1;
                } else {
                    counters[letra]++;
                }

                // Actualizar la evaluación con el número romano
                const numeroRomano = toRoman(counters[letra]) ?? "";
                if (letra.toUpperCase() != "F") {
                    calificacion.Evaluacion = `${numeroRomano}${letra}`;
                    calificacion.EvaluacionCompleta = `${calificacion.EvaluacionCompleta ?? ""}`;
                    //calificacion.Periodo = 
                }
                if (!evaluaciones.find(ev => ev.ev == `${letra}${counters[letra]}`) && calificacion.TipoColumna != "Relleno") {
                    evaluaciones.push({
                        Evaluacion: letra,
                        Parciales: calificacion.Evaluacion,
                        Cantidad: counters[letra],
                        ev: `${letra}${counters[letra]}`,
                        EvaluacionCompleta: calificacion.EvaluacionCompleta
                    });
                }
                
                return calificacion;
            });
            instance.Calificaciones = updatedCalificaciones;
            if (maxDetailsHeaders != null) {
                const updateCalidicacionesWithHeaders = [];
                maxDetailsHeaders.forEach(header => {
                    const evaluacion = updatedCalificaciones.find(ev => ev.Evaluacion == header);
                    if (evaluacion) {
                        updateCalidicacionesWithHeaders.push(evaluacion);
                    } else {
                        updateCalidicacionesWithHeaders.push(new Calificacion_Group({ Evaluacion: header, Resultado: "-", TipoColumna: "Relleno" }));
                    }
                })
                instance.Calificaciones = updateCalidicacionesWithHeaders;
            }
        });
        if (maxDetailsHeaders != null) {
            return maxDetailsHeaders.map((header, index) => {                
                const validResults = Dataset.filter(A => A.Descripcion != "CONDUCTA").flatMap(instance => instance.Calificaciones)
                    .filter(ev => ev.Evaluacion == header &&  ev.Resultado && ev.Resultado != "-"  && !isNaN(ev.Resultado));
                
                const evFilts = Dataset.filter(A => A.Descripcion != "CONDUCTA").flatMap(instance => instance.Calificaciones)
                    .filter(ev => ev.Evaluacion == header  && !isNaN(ev.Resultado)).map(ev => parseFloat(ev.Resultado == "-" ? 0 : ev.Resultado));
                const suma = evFilts.filter(f=> !isNaN(f)).reduce((a, b) => a + b, 0);

                const Promedio = isNaN(suma / validResults.length) ? 0 : suma / validResults.length;

                //console.log(suma, header, Promedio, Dataset.length);
                return {
                    ev: header,
                    Suma: suma,
                    Promedio: Promedio,
                    Evaluacion: header,
                    EvaluacionCompleta: HeaderEvaluacionesCompletas[index],
                };
            })
        }
        return evaluaciones;
    }


}