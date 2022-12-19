using System.ComponentModel.DataAnnotations;
using static System.Double;

namespace Estela_Colba_Test_4.Thumbnails.Models;

public class Thumbnail
{
    public Thumbnail()
    {
        
    }
    public Thumbnail(Guid id, string name, string description, double width, double height, string originalRoute,string thumbnailRoute)
    {
        Id = id;
        Name = name;
        Description = description;
        Width = width;
        Height = height;
        OriginalRoute = originalRoute;
        ThumbnailRoute = thumbnailRoute;
    }

    public Guid Id { get; set; }
    
    [Required]
    [MaxLength(60, ErrorMessage = "No more than 60 characters")]
    public string? Name { get; set; }
    
    public string? Description { get; set; }
    
    [Range(0, MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
    public double Width { get; set; }
    
    [Range(0, MaxValue, ErrorMessage = "Value should be greater than or equal to 0")]
    public double Height { get; set; }
    
    [RegularExpression("(^[A-Za-z0-9\\D]+$)", ErrorMessage = "It has to be a URl")]
    public string? OriginalRoute { get; set; }
    
    [RegularExpression("(^[A-Za-z0-9\\D]+$)", ErrorMessage = "It has to be a URl")]
    public string? ThumbnailRoute { get; set; }
    public int? Visits { get; set; }
}