using Estela_Colba_Test_4.Pagination;
using Estela_Colba_Test_4.Thumbnails.Models;

namespace Estela_Colba_Test_4.Thumbnails;

public interface IThumbnailRepository
{
    public Task<List<Thumbnail>> GetAllAsync();
    public Thumbnail CreateThumbnail(CreateThumbnailResponse createThumbnailResponse);
    public Thumbnail? GetById(Guid id);
    public Thumbnail? UpdateThumbnail(Guid id, CreateThumbnailResponse createThumbnailResponse);
    public Thumbnail? DeleteThumbnail(Guid id);
    public Thumbnail? GetMostViewed();
    public SearchByNamePaginationResponse SearchByName(SearchByNameFilter filter);
}