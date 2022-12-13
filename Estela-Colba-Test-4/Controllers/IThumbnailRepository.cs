namespace Estela_Colba_Test_4.Models;

public interface IThumbnailRepository
{
    public List<Thumbnail> GetAllAsync();
    public Thumbnail CreateThumbnail(Thumbnail thumbnail);
    public Thumbnail? GetById(Guid id);
    public Thumbnail? UpdateThumbnail(Guid id, Thumbnail newThumbnail);
    public Thumbnail? DeleteThumbnail(Guid id);
    public Thumbnail? GetMostViewed();
}