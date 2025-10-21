using System.ComponentModel.DataAnnotations;

namespace Portfolio.Server.Data;

public class Project {
    public int Id { get; set; }
    [MaxLength(100)] public required string Title { get; set; }
    // ReSharper disable once EntityFramework.ModelValidation.UnlimitedStringLength (We allow long descriptions)
    public required string Description { get; set; }
}