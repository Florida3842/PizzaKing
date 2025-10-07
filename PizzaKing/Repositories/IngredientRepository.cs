using Microsoft.EntityFrameworkCore;
using PizzaKing.Interfaces;
using PizzaKing.Models;
namespace PizzaKing.Repositories
{
    public class IngredientRepository : IIngradient
    {
        private readonly ApplicationContext _context;


        public IngredientRepository(ApplicationContext context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Ingredient>> GetAllAsync()
        {
            return await _context.Ingredients.OrderBy(i => i.Category).ThenBy(i => i.Name).ToListAsync();
        }


        public async Task<IEnumerable<Ingredient>> GetByCategoryAsync(IngredientCategory category)
        {
            return await _context.Ingredients.Where(i => i.Category == category).OrderBy(i => i.Name).ToListAsync();
        }


        public async Task<Ingredient> GetByIdAsync(int id)
        {
            return await _context.Ingredients.FirstOrDefaultAsync(i => i.Id == id);
        }
    }
}
