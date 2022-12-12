namespace Estela_Colba_Test_4.Models;

public class Thumbnail
{
    public Guid Id { get; set; }
    
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    public double Width { get; set; }
    
    public double Height { get; set; }
    
    public string? OriginalRoute { get; set; }
    
    public string? ThumbnailRoute { get; set; }
    public int? Visits { get; set; }
}