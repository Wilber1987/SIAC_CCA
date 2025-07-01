
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using APPCORE;
using DataBaseModel;

namespace CAPA_NEGOCIO.Oparations
{
    public class ImageOperation
    {
          private readonly string sourcePath = "C:\\Users\\Alder\\Documents\\Colegio Centro America\\Fotos\\foto";
        private readonly string destinationPath = "C:\\Users\\Alder\\Documents\\Colegio Centro America\\Fotos\\Seleccionadas_Estudiantes";


        public bool MigrateImagenes()
        {
            // Crear la carpeta de destino si no existe
            if (!Directory.Exists(destinationPath))
            {
                Directory.CreateDirectory(destinationPath);
            }

            // Obtener la lista de estudiantes de la base de datos
            var listaEstudiantes = new Estudiantes().Get<Estudiantes>();

            foreach (var estudiante in listaEstudiantes)
            {
                string studentId = estudiante.Id.ToString();
                string imageName = estudiante.Foto;

                // Verifica la carpeta del estudiante
                string studentFolder = Path.Combine(sourcePath, studentId);
                if (Directory.Exists(studentFolder))
                {
                    if (string.IsNullOrEmpty(imageName))
                    {
                        // Si el campo foto está nulo, buscar la imagen más reciente que no tenga prefijo
                        var latestImage = GetLatestImage(studentFolder);
                        if (latestImage != null)
                        {
                            // Actualizar la base de datos con el nombre de la imagen más reciente
                            estudiante.Foto = latestImage;
                            estudiante.Update(); // Este método debe implementar la actualización en la base de datos

                            // Mover la imagen al destino
                            string imagePath = Path.Combine(studentFolder, latestImage);
                            MoveImage(imagePath, studentId);
                        }
                        else
                        {
                            Console.WriteLine($"No se encontró una imagen válida en la carpeta {studentFolder}.");
                        }
                    }
                    else
                    {
                        // Si el campo foto no es nulo, buscar la imagen correspondiente
                        string imagePath = Path.Combine(studentFolder, imageName);

                        if (File.Exists(imagePath))
                        {
                            // Si la imagen existe, moverla al destino
                            MoveImage(imagePath, studentId);
                        }
                        else
                        {
                            Console.WriteLine($"La imagen {imageName} no se encontró en la carpeta {studentFolder}.");
                        }
                    }
                }
                else
                {
                    Console.WriteLine($"La carpeta para el estudiante con ID {studentId} no existe.");
                }
            }

            return true;
        }

        private string GetLatestImage(string folderPath)
        {
            // Filtrar imágenes que no tengan los prefijos "normal_" o "thumbnail_"
            var images = Directory.GetFiles(folderPath, "*.jpg")
                                  .Where(f => !Path.GetFileName(f).StartsWith("normal_") && !Path.GetFileName(f).StartsWith("thumbnail_"))
                                  .OrderByDescending(f => File.GetCreationTime(f))
                                  .Select(f => Path.GetFileName(f))
                                  .ToList();

            // Retornar la imagen más reciente si existe
            return images.FirstOrDefault();
        }

        private void MoveImage(string imagePath, string studentId)
        {
            // Crear la carpeta de destino específica para el estudiante si no existe
            string destFolder = Path.Combine(destinationPath, studentId);
            if (!Directory.Exists(destFolder))
            {
                Directory.CreateDirectory(destFolder);
            }

            // Mover la imagen al destino
            string destImagePath = Path.Combine(destFolder, Path.GetFileName(imagePath));
            File.Copy(imagePath, destImagePath, true);
            Console.WriteLine($"Imagen {Path.GetFileName(imagePath)} movida a {destImagePath}.");
        }
    }
}
