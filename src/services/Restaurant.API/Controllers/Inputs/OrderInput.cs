namespace Restaurant.API.Controllers.Inputs;

public record OrderInput
{
    public string ClientName { get; set; }
    public string ClientAddress { get; set; }
    public string ClientPhone { get; set; }
    public List<Guid> ProductIds { get; init; }
}
