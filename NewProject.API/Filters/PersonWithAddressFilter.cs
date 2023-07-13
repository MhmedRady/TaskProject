namespace NewProject.Application.Filters;

public class PersonWithAddressFilter : FilterModel
{
    public string? Fullname { get;}
    public string? Email { get; }
    public string? Address { get; }
    public string? City { get; }
}