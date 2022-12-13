using Estela_Colba_Test_4.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace Estela_Colba_Test_4.Controllers;


[ApiController]
[Route ("[controller]")]
public class ThumbnailsController: ControllerBase
{
    //private static readonly List<Thumbnail> _thumbnails = ThumbnailsData.Thumbnails;
    private ThumbnailRepository _thumbnailRepository;

    public ThumbnailsController(ThumbnailRepository thumbnailRepository)
    {
        _thumbnailRepository = thumbnailRepository;
    }

    [HttpGet]
    public List<Thumbnail> GetAll()
    {
        var thumbnails = _thumbnailRepository.GetAllAsync();
        return thumbnails;
    }

    [HttpPost]
    public Thumbnail Create([FromBody] Thumbnail thumbnail)
    {
        return _thumbnailRepository.CreateThumbnail(thumbnail);
        
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        var thumbnail = _thumbnailRepository.GetById(id);
        if (thumbnail is null)
        {
            return NotFound();
        }
        return new ObjectResult(thumbnail) { StatusCode = StatusCodes.Status200OK };
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromQuery] Thumbnail newThumbnail)
    {
        var thumbnail = _thumbnailRepository.UpdateThumbnail(id, newThumbnail);
        if (thumbnail is null)
        {
            return NotFound();
        }
        return new ObjectResult(thumbnail) { StatusCode = StatusCodes.Status200OK };
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        //using var db = new ThumbnailsContext();
        var thumbnail = _thumbnailRepository.DeleteThumbnail(id);
        if (thumbnail is null)
        {
            return NotFound();
        }
        return NoContent();
    }

    [HttpGet("search")]
    public SearchByNamePaginationResponse SearchByName([FromQuery]SearchByNameFilter filter)
    {
        return _thumbnailRepository.SearchByName(filter);
    }

    [HttpGet("mostViewed")]
    public IActionResult GetMostViewed()
    {
        var thumbnail = _thumbnailRepository.GetMostViewed();
        if (thumbnail is null)
        {
            return NotFound();
        }
        return new ObjectResult(thumbnail) { StatusCode = StatusCodes.Status200OK };
    }
    
    
}
