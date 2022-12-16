using Estela_Colba_Test_4.Pagination;
using Estela_Colba_Test_4.Thumbnails.Models;
using Microsoft.AspNetCore.Mvc;

namespace Estela_Colba_Test_4.Thumbnails;


[ApiController]
[Route ("[controller]")]
public class ThumbnailsController: ControllerBase
{
    //private static readonly List<Thumbnail> _thumbnails = ThumbnailsData.Thumbnails;
    private readonly IThumbnailRepository _thumbnailRepository;
    private readonly ILogger<ThumbnailsController> _logger;

    public ThumbnailsController(IThumbnailRepository thumbnailRepository, ILogger<ThumbnailsController> logger)
    {
        _thumbnailRepository = thumbnailRepository;
        _logger = logger;
    }

    [HttpGet]
    public async Task<List<Thumbnail>> GetAll()
    {
        var thumbnails = await _thumbnailRepository.GetAllAsync();
        return thumbnails;
    }

    [HttpPost]
    public async Task<Thumbnail> Create([FromBody] CreateThumbnailRequest createThumbnailRequest)
    {
        return await _thumbnailRepository.CreateThumbnail(createThumbnailRequest);
        // TODO comprobar si es nulo que devuelva 500 y hacer test
    }
    
    [HttpGet("{id:guid}")]
    public async Task<ActionResult<Thumbnail>> GetById([FromRoute] Guid id)
    {
        var thumbnail = await _thumbnailRepository.GetById(id);
        if (thumbnail is not null) return Ok(thumbnail);
        _logger.LogWarning($"Not found {id}");
        return NotFound(id);
        //return new ObjectResult(thumbnail) { StatusCode = StatusCodes.Status200OK };
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] CreateThumbnailRequest createThumbnailRequest)
    {
        var thumbnail = await _thumbnailRepository.UpdateThumbnail(id, createThumbnailRequest);
        if (thumbnail is not null) return new ObjectResult(thumbnail) { StatusCode = StatusCodes.Status200OK };
        _logger.LogWarning($"Not found {id}");
        return NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete([FromRoute] Guid id)
    {
        //using var db = new ThumbnailsContext();
        var thumbnail = await _thumbnailRepository.DeleteThumbnail(id);
        if (thumbnail is not null) return NoContent();
        _logger.LogWarning($"Not found {id}");
        return NotFound();
    }

    [HttpGet("search")]
    public async Task<SearchByNamePaginationResponse> SearchByName([FromQuery]SearchByNameFilter filter)
    {
        return await _thumbnailRepository.SearchByName(filter);
    }

    [HttpGet("mostViewed")]
    public async Task<IActionResult> GetMostViewed()
    {
        var thumbnail = await _thumbnailRepository.GetMostViewed();
        if (thumbnail is not null) return new ObjectResult(thumbnail) { StatusCode = StatusCodes.Status200OK };
        _logger.LogWarning($"Could not find the thumbnail with the most views");
        return NotFound();
    }
    
    
}
