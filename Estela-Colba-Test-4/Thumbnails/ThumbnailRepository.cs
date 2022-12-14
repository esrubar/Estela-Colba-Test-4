using Estela_Colba_Test_4.Pagination;
using Estela_Colba_Test_4.Thumbnails.Models;

namespace Estela_Colba_Test_4.Thumbnails;

public class ThumbnailRepository : IThumbnailRepository
{
    private readonly ThumbnailsContext _db;
    //private readonly IThumbnailRepository _thumbnailRepository;
    private const int InitialVisits = 0;


    public ThumbnailRepository(ThumbnailsContext db)
    {
        _db = db;
        //_thumbnailRepository = thumbnailRepository;
    }

   public async Task<List<Thumbnail>> GetAllAsync()
    {
        //using var db = new ThumbnailsContext();
        var thumbnails = await Task.Run(() => _db.Thumbnails.ToList());
        return thumbnails;
    }

   public Thumbnail CreateThumbnail(CreateThumbnailResponse createThumbnailResponse)
   {
       //using var db = new ThumbnailsContext();
       var thumbnail = new Thumbnail
       {
           Id = Guid.NewGuid(),
           Visits = InitialVisits,
           Name = createThumbnailResponse.Name,
           Description = createThumbnailResponse.Description,
           Width = createThumbnailResponse.Width,
           Height = createThumbnailResponse.Height,
           OriginalRoute = createThumbnailResponse.OriginalRoute,
           ThumbnailRoute = createThumbnailResponse.ThumbnailRoute
       };

       _db.Thumbnails.Add(thumbnail);
       _db.SaveChanges();
       return thumbnail;
   }

   public Thumbnail? GetById(Guid id)
   {
       //using var db = new ThumbnailsContext();
       var thumbnail = _db.Thumbnails.FirstOrDefault(thumbnail => thumbnail.Id == id);
       if (thumbnail is not null)
       {
           thumbnail.Visits += 1;
       }
       _db.SaveChanges();
       return thumbnail;
   }


   public Thumbnail? UpdateThumbnail(Guid id, CreateThumbnailResponse createThumbnailResponse)
   {
       //using var db = new ThumbnailsContext();
       var thumbnail = GetById(id);
       if (thumbnail is not null)
       {
           thumbnail.Name = createThumbnailResponse.Name;    
           thumbnail.Description = createThumbnailResponse.Description;  
           thumbnail.Width = createThumbnailResponse.Width;
           thumbnail.Height = createThumbnailResponse.Height;
           thumbnail.OriginalRoute = createThumbnailResponse.OriginalRoute;
           thumbnail.ThumbnailRoute = createThumbnailResponse.ThumbnailRoute; 
           _db.SaveChanges();
       }
       return thumbnail;
   }

   public Thumbnail? DeleteThumbnail(Guid id)
   {
       var thumbnail = GetById(id);
       if (thumbnail is not null)
       {
           _db.Remove(thumbnail);
           _db.SaveChanges();
       }
       return thumbnail;
   }

   public Thumbnail? GetMostViewed()
   {
       //using var db = new ThumbnailsContext();
       return _db.Thumbnails.OrderByDescending(t => t.Visits).FirstOrDefault();
   }

   public SearchByNamePaginationResponse SearchByName(SearchByNameFilter filter)
   {
       var elementsInPage = new List<Thumbnail>();
       var thumbnails = _db.Thumbnails.Local
           .Where(t => t.Name.ToLower().Contains(filter.Name.ToLower()) 
                       && t.Description.ToLower().Contains(filter.Description.ToLower())).ToList();
       
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