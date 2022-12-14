using Estela_Colba_Test_4.Thumbnails.Models;
using Microsoft.EntityFrameworkCore;

namespace Estela_Colba_Test_4.Thumbnails;

public class ThumbnailsContext: DbContext
{
    public DbSet<Thumbnail> Thumbnails { get; set; }
    public string DbPath { get; }
    
    public ThumbnailsContext()
    {
        var folder = Environment.SpecialFolder.LocalApplicationData;
        var path = Environment.GetFolderPath(folder);
        DbPath = Path.Join(path, "thumbnails.db");
    }
    protected override void OnConfiguring(DbContextOptionsBuilder options)
        => options.UseSqlite($"Data Source={DbPath}");
}