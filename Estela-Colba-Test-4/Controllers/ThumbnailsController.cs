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
    private int InitialVisits = 0;

    [HttpGet]
    public List<Thumbnail> GetAll()
    {
        using (var db = new ThumbnailsContext())
        {
            return db.Thumbnails.ToList();
        }
        
    }

    [HttpPost]
    public Thumbnail Create([FromBody] Thumbnail thumbnail)
    {
        using (var db = new ThumbnailsContext())
        {
            thumbnail.Id = Guid.NewGuid();
            thumbnail.Visits = InitialVisits;

            db.Thumbnails.Add(thumbnail);
            db.SaveChanges();
        }
        return thumbnail;
    }
    
    [HttpGet("{id:guid}")]
    public IActionResult GetById([FromRoute] Guid id)
    {
        using (var db = new ThumbnailsContext())
        {
           var  thumbnail = db.Thumbnails.FirstOrDefault(thumbnail => thumbnail.Id == id);
            if (thumbnail is null)
            {
                return NotFound();
            }
            thumbnail.Visits += 1;
            db.SaveChanges();
            return new ObjectResult(thumbnail) { StatusCode = StatusCodes.Status200OK };
        }
    }

    [HttpPut("{id:guid}")]
    public IActionResult Update([FromRoute] Guid id, [FromQuery] Thumbnail updatedThumbnail)
    {
        using (var db = new ThumbnailsContext())
        {
            var thumbnail = db.Thumbnails.FirstOrDefault(t => t.Id == id);
            if (thumbnail is null)
            {
                return NotFound();
            }

            thumbnail = updatedThumbnail;
            db.SaveChanges();
            return new ObjectResult(thumbnail) { StatusCode = StatusCodes.Status200OK };

        }
    }

    [HttpDelete("{id:guid}")]
    public IActionResult Delete([FromRoute] Guid id)
    {
        using (var db = new ThumbnailsContext())
        {
            var thumbnail = db.Thumbnails.FirstOrDefault(thumbnail => thumbnail.Id == id);
            if (thumbnail is null)
            {
                return NotFound();
            }
            db.Remove(thumbnail);
            db.SaveChanges();
            return NoContent();

        }
    }

    [HttpGet("search")]
    public SearchByNamePaginationResponse SearchByName([FromQuery]SearchByNameFilter filter)
    {
        using (var db = new ThumbnailsContext())
        {
            var elementsInPage = new List<Thumbnail>();
    
            var thumbnails = db.Thumbnails.
                    Where(t => t.Name.ToLower().Contains(filter.Name.ToLower()) 
                    && t.Description.ToLower().Contains(filter.Description.ToLower())).ToList();

            for (var i = 0; i <= filter.ElementsInPageCount - 1; i++)
            {
                var element = thumbnails.ElementAtOrDefault(filter.ElementsInPageCount * (filter.Page - 1) + i);
                if (element != null) elementsInPage.Add(element); }
         
            var total = (double)thumbnails.Count / filter.ElementsInPageCount;
            var totalPages = (int)Math.Ceiling(total);

            var searchByNamePaginationResponse =
                new SearchByNamePaginationResponse(filter.Page, totalPages, filter.ElementsInPageCount, elementsInPage);
            return searchByNamePaginationResponse;

        }

    }

    [HttpGet("mostViewed")]
    public IActionResult GetMostViewed()
    {
        using (var db = new ThumbnailsContext())
        {
            var thumbnail = db.Thumbnails.OrderByDescending(t => t.Visits).FirstOrDefault();
            if (thumbnail is null)
            {
                return NotFound();
            }
            return new ObjectResult(thumbnail) { StatusCode = StatusCodes.Status200OK };
        }
    }
    
    
}
