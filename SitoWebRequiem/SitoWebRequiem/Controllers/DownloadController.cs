using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("api/game")]
public class GameController : ControllerBase
{
    [Authorize]
    [HttpGet("download")]
    public IActionResult DownloadGame()
    {
        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "Files", "Requiem.zip");

        var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read, FileShare.Read);

        return File(stream, "application/zip", "Requiem.zip");
    }
}