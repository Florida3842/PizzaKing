using PizzaKing.Models;

namespace PizzaKing.ViewModels
{
    public class PizzaSummaryViewModel
    {
        public Ingredient Crust { get; set; }
        public Ingredient Sauce { get; set; }
        public List<Ingredient> Toppings { get; set; } = new List<Ingredient>();
        public decimal TotalPrice { get; set; }
    }
}
