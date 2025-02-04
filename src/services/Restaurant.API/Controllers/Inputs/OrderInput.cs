namespace Restaurant.API.Controllers.Inputs;

public record OrderInput
{
    public string ClientName { get; set; }
    public string ClientAddress { get; set; }
    public string ClientPhone { get; set; }
    public List<OrderInputProductInput> Products { get; init; }
    public Guid? UsuarioId { get; set; }

}


public record OrderInputProductInput
{
    public Guid ProductId { get; set; }
    public int Quantity { get; set; }
}
