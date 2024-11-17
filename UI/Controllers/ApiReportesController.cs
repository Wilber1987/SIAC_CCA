using API.Controllers;
using CAPA_DATOS;
using CAPA_DATOS.Security;
using CAPA_NEGOCIO.UpdateModule.Model;
using CAPA_NEGOCIO.UpdateModule.Operations;
using DataBaseModel;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using ClosedXML.Excel;

namespace UI.Controllers
{
	[ApiController]
	[Route("api/[controller]/[action]")]
	public class ApiReportesController : ControllerBase
	{
		[HttpPost]
		[AuthController(Permissions.UPDATE_FAMILY_DATA)]
		public List<Estudiantes_Data_Update> GetEstudiantesConRecorridos(Estudiantes_Data_Update inst)
		{
			return inst.GetEstudiantesConRecorridos();
		}

		[HttpPost]
		[AuthController]
		public IActionResult ExportToExcel([FromBody] ExportRequest request)
		{
			try
			{
				// Decodificar el HTML en Base64
				string decodedHtml = Encoding.UTF8.GetString(Convert.FromBase64String(request.DocumentHtml));

				// Crear un archivo Excel
				using (var workbook = new XLWorkbook())
				{
					var worksheet = workbook.Worksheets.Add("Datos");

					// Extraer las filas de la tabla HTML
					var rowMatches = Regex.Matches(decodedHtml, @"<tr.*?>(.*?)<\/tr>", RegexOptions.Singleline);

					int currentRow = 1;

					foreach (Match rowMatch in rowMatches)
					{
						var rowContent = rowMatch.Groups[1].Value;

						// Extraer las celdas de cada fila
						var cellMatches = Regex.Matches(rowContent, @"<t[hd].*?>(.*?)<\/t[hd]>", RegexOptions.Singleline);

						int currentCol = 1;

						foreach (Match cellMatch in cellMatches)
						{
							var cellContent = cellMatch.Groups[1].Value;

							// Buscar si la celda tiene atributos de estilo en línea
							var styleMatch = Regex.Match(cellMatch.Value, @"style=['""]([^'""]+)['""]");
							string cellStyle = styleMatch.Success ? styleMatch.Groups[1].Value : string.Empty;

							// Aplicar los estilos manualmente si existen
							if (!string.IsNullOrEmpty(cellStyle))
							{
								// Aquí puedes agregar lógica para analizar y aplicar estilos como color de fondo, borde, etc.
								if (cellStyle.Contains("background-color"))
								{
									var bgColorMatch = Regex.Match(cellStyle, @"background-color:\s*([^;]+)");
									if (bgColorMatch.Success)
									{
										string bgColor = bgColorMatch.Groups[1].Value;
										worksheet.Cell(currentRow, currentCol).Style.Fill.SetBackgroundColor(XLColor.FromHtml(bgColor));
									}
								}

								if (cellStyle.Contains("color"))
								{
									var colorMatch = Regex.Match(cellStyle, @"color:\s*([^;]+)");
									if (colorMatch.Success)
									{
										//string textColor = colorMatch.Groups[1].Value;
										string textColor = "#000000";
										worksheet.Cell(currentRow, currentCol).Style.Font.SetFontColor(XLColor.FromHtml(textColor));
									}
								}

								if (cellStyle.Contains("font-weight"))
								{
									if (cellStyle.Contains("bold"))
									{
										worksheet.Cell(currentRow, currentCol).Style.Font.SetBold();
									}
								}
							}

							// Verificar si la celda contiene una imagen en base64
							var base64Match = Regex.Match(cellContent, @"data:image\/.*?;base64,(.*?)['""]");

							if (base64Match.Success)
							{
								// Extraer y convertir la imagen base64
								string base64Data = base64Match.Groups[1].Value;
								byte[] imageBytes = Convert.FromBase64String(base64Data);

								// Guardar temporalmente la imagen
								string tempImagePath = Path.Combine(Path.GetTempPath(), Guid.NewGuid() + ".png");
								System.IO.File.WriteAllBytes(tempImagePath, imageBytes);

								// Insertar la imagen en el Excel
								var image = worksheet.AddPicture(tempImagePath)
													  .MoveTo(worksheet.Cell(currentRow, currentCol));

								// Ajustar tamaño de celda
								worksheet.Row(currentRow).Height = 50;
								worksheet.Column(currentCol).Width = 50;

								// Eliminar archivo temporal
								System.IO.File.Delete(tempImagePath);
							}
							else
							{
								// Insertar texto normal
								worksheet.Cell(currentRow, currentCol).Value = cellContent;
							}

							currentCol++;
						}

						currentRow++;
					}

					// Guardar el archivo en memoria
					using (var stream = new MemoryStream())
					{
						 // Ajustar el tamaño de las celdas (adaptar a contenido, pero no mayor de 300px)
						worksheet.Columns().AdjustToContents();  // Esto ajustará el tamaño de las columnas automáticamente
						//worksheet.Columns().Width = 200;
						workbook.SaveAs(stream);
						stream.Seek(0, SeekOrigin.Begin);

						// Devolver el archivo Excel como respuesta
						return File(stream.ToArray(),
									"application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
									"TablaConImagenes.xlsx");
					}
				}
			}
			catch (Exception ex)
			{
				return BadRequest(new { message = "Error al generar el archivo Excel", error = ex.Message });
			}
		}

	}

	public class ExportRequest
	{
		public required string DocumentHtml { get; set; }
	}
}
