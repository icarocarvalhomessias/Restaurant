namespace Restaurant.API.Controllers.Inputs
{
    public record ProductInput
    {
        public string Name { get; init; }
        public decimal Price { get; init; }
        public string Description { get; init; }
        public string Image { get; set; }
        public int Stock { get; set; }

        public ProductInput(string name, decimal price, string description, string image, int stock)
        {
            Name = name;
            Price = price;
            Description = description;
            Image = image;
            Stock = stock;
        }


    }
}
