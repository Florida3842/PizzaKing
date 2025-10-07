using PizzaKing.Models;

namespace PizzaKing.ViewModels
{
    public class PizzaBuilderViewModel
    {
        public IEnumerable<Ingredient> Crusts { get; set; }
        public IEnumerable<Ingredient> Sauces { get; set; }
        public IEnumerable<Ingredient> Toppings { get; set; }


        public int SelectedCrustId { get; set; }
        public int SelectedSauceId { get; set; }
        public List<int> SelectedToppingIds { get; set; } = new List<int>();
    }
}
