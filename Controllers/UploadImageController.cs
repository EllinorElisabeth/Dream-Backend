using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace Dream.Controller;

[ApiController]
[Route("[controller]")]

public class UploadImageController : ControllerBase
{
    private readonly IWebHostEnvironment _webHostEnvironment;

    public UploadImageController(IWebHostEnvironment webHostEnvironment)
    {
        _webHostEnvironment = webHostEnvironment;
    }

    [HttpPost]
    public async Task<ActionResult>Post(IFormFile file)
    {
        try
        {
            string webKootPath = _webHostEnvironment.WebRootPath;
            string absolutePath = Path.Combine(webKootPath, "images", file.FileName);

            using (var fileStream = new FileStream(absolutePath, FileMode.Create))
            {
                await file.CopyToAsync(fileStream);
            }

            return Created();
        }
        catch
        {
            return StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}