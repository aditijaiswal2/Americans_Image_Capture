using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

public class MaagAmericansImage
{
    [Key]
    public int Id { get; set; }
    [Required]
    public string Location { get; set; } = string.Empty;
    [Required]
    public string Department { get; set; } = string.Empty;
    [Required]
    public string Username { get; set; } = string.Empty;
    public DateTime? Date { get; set; } = DateTime.UtcNow;
    public string ProjectKent { get; set; } = string.Empty;
    [Required]
    public List<Imagedata> Images { get; set; } = new();
}

public class Imagedata
{
    [Key]
    public int Id { get; set; }
    [Required]
    public int MAAGImageId { get; set; } // Foreign Key
    [Required]
    public string ImageFilePath { get; set; } = string.Empty;

    [JsonIgnore] // Prevent circular serialization
    public MaagAmericansImage? MAAGImage { get; set; }
}
