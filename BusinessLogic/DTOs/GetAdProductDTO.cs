namespace BusinessLogic.DTOs
{
    public class GetAdProductDTO
    {
        public int ProductId {get; set;}
        public string Name { get; set; }
        public string ImageUrl { get; set; }
        public decimal Price { get; set; }
        public string ShortDescription { get; set; }
        public string CategoryName { get; set; } 
    }
}