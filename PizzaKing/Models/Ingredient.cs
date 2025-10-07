using System.ComponentModel.DataAnnotations;
using PizzaKing.Models.Enums;

namespace PizzaKing.Models
{
    public class Ingredient
    {
        [Key]
        public int Id { get; set; }


        [Required]
        public string Name { get; set; }


        public IngredientCategory Category { get; set; }


        // Path relative to web root, e.g. "/assets/ingredients/tomato.png"
        public string ImagePath { get; set; }


        // Доп. данные
        public decimal Price { get; set; }


        public bool IsDefault { get; set; }
    }
}
