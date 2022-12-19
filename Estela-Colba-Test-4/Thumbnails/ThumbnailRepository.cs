using Estela_Colba_Test_4.Pagination;
using Estela_Colba_Test_4.Thumbnails.Models;
using Microsoft.EntityFrameworkCore;

namespace Estela_Colba_Test_4.Thumbnails;

public class ThumbnailRepository : IThumbnailRepository
{
    private readonly ThumbnailsContext _db;
    private const int InitialVisits = 0;


    public ThumbnailRepository(ThumbnailsContext db)
    {
        _db = db;
    }

   public async Task<List<Thumbnail>> GetAllAsync()
    {
        //using var db = new ThumbnailsContext();
        //var thumbnails = await Task.Run(() => _db.Thumbnails.ToList());
        return await _db.Thumbnails.ToListAsync();
    }

   public async Task<Thumbnail> CreateThumbnail(CreateThumbnailRequest? createThumbnailRequest)
   {
       if (createThumbnailRequest is null) return null;
       //using var db = new ThumbnailsContext();
       var thumbnail = new Thumbnail
       {
           Id = Guid.NewGuid(),
           Visits = InitialVisits,
           Name = createThumbnailRequest.Name,
           Description = createThumbnailRequest.Description,
           Width = createThumbnailRequest.Width,
           Height = createThumbnailRequest.Height,
           OriginalRoute = createThumbnailRequest.OriginalRoute,
           ThumbnailRoute = createThumbnailRequest.ThumbnailRoute
       };
       //await Task.Run(() => _db.Thumbnails.Add(thumbnail)); no hace falta que el add sea async
       _db.Thumbnails.Add(thumbnail);
       await _db.SaveChangesAsync();
       return thumbnail;
   }

   public async Task<Thumbnail?> GetById(Guid id)
   {
       //using var db = new ThumbnailsContext();
       //var thumbnail = await Task.Run(() => _db.Thumbnails.FirstOrDefault(thumbnail => thumbnail.Id == id));
       var thumbnail = await _db.Thumbnails.FirstOrDefaultAsync(thumbnail => thumbnail.Id == id);
       if (thumbnail is not null)
       {
           thumbnail.Visits += 1;
       }
       await _db.SaveChangesAsync();
       return thumbnail;
   }


   public async Task<Thumbnail?> UpdateThumbnail(Guid id, CreateThumbnailRequest createThumbnailRequest)
   {
       //using var db = new ThumbnailsContext();
       var thumbnail = await GetById(id);
       if (thumbnail is null) return thumbnail;
       thumbnail.Name = createThumbnailRequest.Name;    
       thumbnail.Description = createThumbnailRequest.Description;  
       thumbnail.Width = createThumbnailRequest.Width;
       thumbnail.Height = createThumbnailRequest.Height;
       thumbnail.OriginalRoute = createThumbnailRequest.OriginalRoute;
       thumbnail.ThumbnailRoute = createThumbnailRequest.ThumbnailRoute; 
       await _db.SaveChangesAsync();
       return thumbnail;
   }

   public async Task<Thumbnail?> DeleteThumbnail(Guid id)
   {
       var thumbnail = await GetById(id);
       if (thumbnail is null) return thumbnail;
       _db.Remove(thumbnail);
       await _db.SaveChangesAsync();
       return thumbnail;
   }

   public async Task<Thumbnail?> GetMostViewed()
   {
       //using var db = new ThumbnailsContext();
       //var thumbnail = await Task.Run(() => _db.Thumbnails.OrderByDescending(t => t.Visits).FirstOrDefault());
       //return thumbnail;
       return await _db.Thumbnails.OrderByDescending(t => t.Visits).FirstOrDefaultAsync();
   }

   public async Task<SearchByNamePaginationResponse> SearchByName(SearchByNameFilter filter)
   {
      var elementsInPage = new List<Thumbnail>();
      var thumbnails = await _db.Thumbnails
           .Where(t => t.Name.ToLower().Contains(filter.Name.ToLower()) 
                       && t.Description.ToLower().Contains(filter.Description.ToLower())).ToListAsync();
/*
      var thumbnails = await Task.Run(() => _db.Thumbnails
          .Where(t => t.Name.ToLower().Contains(filter.Name.ToLower()) 
                      && t.Description.ToLower().Contains(filter.Description.ToLower())).ToList());
*/
       for (var i = 0; i <= filter.ElementsInPageCount - 1; i++)
       {
           var element = thumbnails.ElementAtOrDefault(filter.ElementsInPageCount * (filter.Page - 1) + i);
           if (element != null) elementsInPage.Add(element); 
       }
       var total = (double)thumbnails.Count / filter.ElementsInPageCount;
       var totalPages = (int)Math.Ceiling(total);
       var searchByNamePaginationResponse =
           new SearchByNamePaginationResponse(filter.Page, totalPages, filter.ElementsInPageCount, elementsInPage);
       return searchByNamePaginationResponse;

   }
   
}