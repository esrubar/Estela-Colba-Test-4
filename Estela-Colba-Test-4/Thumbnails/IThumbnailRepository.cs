using Estela_Colba_Test_4.Pagination;
using Estela_Colba_Test_4.Thumbnails.Models;

namespace Estela_Colba_Test_4.Thumbnails;

public interface IThumbnailRepository
{
    public Task<List<Thumbnail>> GetAllAsync();
    public Task<Thumbnail> CreateThumbnail(CreateThumbnailRequest createThumbnailRequest);
    public Task<Thumbnail?> GetById(Guid id);
    public Task<Thumbnail?> UpdateThumbnail(Guid id, CreateThumbnailRequest createThumbnailRequest);
    public Task<Thumbnail?> DeleteThumbnail(Guid id);
    public Task<Thumbnail?> GetMostViewed();
    public Task<SearchByNamePaginationResponse> SearchByName(SearchByNameFilter filter);
}