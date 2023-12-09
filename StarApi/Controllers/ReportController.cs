using Microsoft.AspNetCore.Mvc;
using OfficeOpenXml;
using StarApi.Model;
using StarApi.ModelView;
using Xceed.Document.NET;
using Xceed.Words.NET;

namespace StarApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController: ControllerBase
    {
        private readonly StarContext _context;

        public ReportController(StarContext context)
        {
            _context = context;
        }

        private ReportView generateReportView(Guid gId)
        {
            var planets = _context.Planets.ToList();
            var stars = _context.Stars.Where(x => x.GalaxyId == gId).ToList();
            var galaxy = _context.Galaxies.FirstOrDefault(x => x.Id == gId);

            if (galaxy == null)
            {
                return null;
            }
            var planets_str = "";
            var stars_str = "";
            var planets_in_galaxy = stars.Select(x => x.Planets).ToList();
            planets_in_galaxy.ForEach(x => x.ToList().ForEach(x => planets_str += x.Name + ", "));

            stars.ToList().ForEach(x => stars_str += x.Name + ", ");


            return new ReportView(galaxy.Name, galaxy.Shape, galaxy.Size, stars_str, planets_str, galaxy.Composition);
        }
        private byte[] generateExcelReport(ReportView report)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("GalaxyReport");

                worksheet.Cells[1, 1].Value = "Название категории";
                worksheet.Cells[2, 1].Value = "Имя";
                worksheet.Cells[3, 1].Value = "Форма";
                worksheet.Cells[4, 1].Value = "Размер";
                worksheet.Cells[5, 1].Value = "Звезды";
                worksheet.Cells[6, 1].Value = "Известные планеты";
                worksheet.Cells[7, 1].Value = "Состав";

                worksheet.Cells[1, 2].Value = "Описание";
                worksheet.Cells[2, 2].Value = report.Name;
                worksheet.Cells[3, 2].Value = report.Shape;
                worksheet.Cells[4, 2].Value = report.Size;
                worksheet.Cells[5, 2].Value = report.Stars;
                worksheet.Cells[6, 2].Value = report.Planets;
                worksheet.Cells[7, 2].Value = report.Composition;

                return package.GetAsByteArray();
            }
        }
        private byte[] generateWordReport(ReportView report)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                using (DocX document = DocX.Create(ms))
                {
                    document.InsertParagraph("Отчет по галактике").FontSize(18d).Bold().Alignment = Alignment.center;
                    document.InsertParagraph($"Имя: {report.Name}");
                    document.InsertParagraph($"Размер: {report.Size}");
                    document.InsertParagraph($"Форма: {report.Shape ?? "N/A"}");
                    document.InsertParagraph($"Звезды: {report.Stars}");
                    document.InsertParagraph($"Планеты: {report.Planets}");
                    document.InsertParagraph($"Состав: {report.Composition ?? "N/A"}");

                    document.Save();
                }

                return ms.ToArray();
            }
        }

        [HttpGet("data")]
        public ReportView GetReport(Guid gId)
        {
            return generateReportView(gId);
        }

        [HttpGet("excel")]
        public IActionResult GetExcel(Guid gId)
        {
            try
            {
                var report = generateReportView(gId);
                var excelFile = generateExcelReport(report);
                return File(excelFile, "application/octet-stream", "report.xlsx");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }   
        }

        [HttpGet("word")]
        public IActionResult GetWord(Guid gId)
        {
            try
            {
                var report = generateReportView(gId);
                var wordFile = generateWordReport(report);
                return File(wordFile, "application/octet-stream", "report.docx");
            } catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
