using System.ComponentModel.DataAnnotations;

namespace NewProject.Application.Filters;

public class PersonFilter : FilterModel
{
    public string? Fullname { get; set; } = String.Empty;
    public string? Email { get; set; } = String.Empty;
    public string? PhoneNumber { get; set; } = String.Empty;
}