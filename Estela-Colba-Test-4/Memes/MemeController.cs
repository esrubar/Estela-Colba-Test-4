using Estela_Colba_Test_4.Thumbnails;
using Microsoft.AspNetCore.Mvc;

namespace Estela_Colba_Test_4.Memes;

[ApiController]
[Route ("[controller]")]
public class MemesController: ControllerBase
{
    [HttpGet("{name}")]
    public IActionResult GetByName([FromRoute] string name)
    {
        using (var db = new ThumbnailsContext())
        {
            var thumbnail = db.Thumbnails.FirstOrDefault(t => t.Name.ToLower() == name.ToLower());
            if (thumbnail is null)
            {
                return NotFound();
            }
            var thumbnailRoute = thumbnail.ThumbnailRoute;
            return new ObjectResult(thumbnailRoute)
            {
                StatusCode = StatusCodes.Status200OK
            };
        }
    }
    
}