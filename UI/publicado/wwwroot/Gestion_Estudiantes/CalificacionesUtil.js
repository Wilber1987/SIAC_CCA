import { Calificacion_Group } from "../Model/Estudiantes.js";

//@ts-check
export class CalificacionesUtil {
    static /**
    * @param {Array< Asignatura_Group|Estudiante_Group>} Dataset
    * @param {number} [maxDetails]
    */
   UpdateCalificaciones(Dataset, maxDetails) {
       //console.log(instance.Calificaciones.length, maxDetails);  
       Dataset.forEach(instance => {
           if (maxDetails && instance.Calificaciones.length < maxDetails) {
               let isFirstOrder = instance.Calificaciones[0].Order == 1
               for (let index = 0; index <= (maxDetails - instance.Calificaciones.length + 1); index++) {
                   if (isFirstOrder) {
                       instance.Calificaciones.push(new Calificacion_Group({ Evaluacion: "", Resultado: "-" }));
                   } else {
                       instance.Calificaciones.unshift(new Calificacion_Group({ Evaluacion: "", Resultado: "-" }));
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
               const letra = calificacion.Evaluacion == null
                   || calificacion.Evaluacion == ""
                   || calificacion.Evaluacion == undefined ? "E" : calificacion.Evaluacion;

               // Incrementar el contador para la letra correspondiente
               if (!counters[letra]) {
                   counters[letra] = 1;
               } else {
                   counters[letra]++;
               }

               // Actualizar la evaluación con el número romano
               const numeroRomano = toRoman(counters[letra]);
               if (letra.toUpperCase() != "F") {
                   calificacion.Evaluacion = `${numeroRomano}${letra}`;
                   calificacion.EvaluacionCompleta = `${numeroRomano} ${calificacion.EvaluacionCompleta ?? "Evaluación"}`;
               }
               return calificacion;
           });
           instance.Calificaciones = updatedCalificaciones;
       });
   }

}