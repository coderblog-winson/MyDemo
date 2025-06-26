
using Microsoft.AspNetCore.Mvc;
using FastReport;
using FastReport.Export.PdfSimple;

namespace MyDemo.Controllers;


[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
  protected readonly ILogger<UploadFileController> _logger;
  public ReportController(ILogger<UploadFileController> logger)
  {
    _logger = logger;
  }

  [HttpGet("generate")]
  public IActionResult Generate()
  {
    try
    {

      List<ReportUser> users = new List<ReportUser>
                {
                    new ReportUser { Id = 1, Name = "John Doe", Password = "password123", Email = "test1@abc.com", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                    new ReportUser { Id = 2, Name = "Jane Smith", Password = "password456", Email = "test2@abc.com", IsActive = false, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now },
                    new ReportUser { Id = 3, Name = "Alice Johnson", Password = "password789", Email = "test3@abc.com", IsActive = true, CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now }
                };

      Report report = new Report();
      //load the report template
      report.Load(Path.Combine("./Reports", "Demo.frx"));
      //register the data source
      report.RegisterData(users, "UserTable");
      //prepare the report  
      report.Prepare();
      //export to PDF 
      string pdfFile = "./temp/Demo.pdf";

      //export the report template to PDF 
      PDFSimpleExport pdfExport = new PDFSimpleExport();
      pdfExport.Export(report, pdfFile);

      //Send the PDF to client side
      byte[] pdfBytes = System.IO.File.ReadAllBytes(pdfFile);
      MemoryStream stream = new MemoryStream(pdfBytes);

      //Delete the file after download 
      System.IO.File.Delete(pdfFile);

      //Return the PDF file as a FileStreamResult
      return new FileStreamResult(stream, "application/pdf");
    }
    catch (Exception ex)
    {
      // Log the exception (optional)
      _logger.LogError(ex, "Report Error");
      Console.WriteLine(ex.Message);
      return StatusCode(500, "An error occurred while generating the report.");
    }
  }
}

