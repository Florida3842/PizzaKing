using PizzaKing.Models;

namespace PizzaKing.Interfaces
{
    public interface IIngradient
    {
        Task<IEnumerable<Ingredient>> GetAllAsync();
        Task<IEnumerable<Ingredient>> GetByCategoryAsync(IngredientCategory category);
        Task<Ingredient> GetByIdAsync(int id);
    }
}
