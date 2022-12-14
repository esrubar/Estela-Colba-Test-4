using Estela_Colba_Test_4.Pagination;
using Estela_Colba_Test_4.Thumbnails.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estela_Colba_Test_4.Thumbnails;


[ApiController]
[Route ("[controller]")]
public class ThumbnailsController: ControllerBase
{
    //private static readonly List<Thumbnail> _thumbnails = ThumbnailsData.Thumbnails;
    private IThumbnailRepository _thumbnailRepository;

    public ThumbnailsController(IThumbnailRepository thumbnailRepository)
    {
        _thumbnailRepository = thumbnailRepository;
    }

    [HttpGet]
    public async Task<List<Thumbnail>> GetAll()
    {
        var thumbnails = await _thumbnailRepository.GetAllAsync();
        return thumbnails;
    }

    [HttpPost]
    public Thumbnail Create([FromBody] CreateThumbnailResponse createThumbnailResponse)
    {
        return _thumbnailRepository.CreateThumbnail(createThumbnailResponse);
        
    }
    
    [HttpGet("{id:guid}")]
    public ActionResult<Thumbnail> GetById([FromRoute] Guid id)
    {
        var thumbnail = _thumbnailRepository.GetById(id);
        if (thumbnail is null)
        {
            return NotFound();
        }
        //return new ObjectResult(thumbnail) { StatusCode = StatusCodes.Status200OK };
        return Ok(thumbnail);
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromBody] CreateThumbnailResponse createThumbnailResponse)
    {
        var thumbnail = _thumbnailRepository.UpdateThumbnail(id, createThumbnailResponse);
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
