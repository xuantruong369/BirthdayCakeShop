namespace Presentation.Models
{
    public class AddCartItem
    {
        public int? ProductDetailId {get; set;}
        public int? Quantity {get; set;} = 1;
        public int? ProductID {get; set;}

    }
}