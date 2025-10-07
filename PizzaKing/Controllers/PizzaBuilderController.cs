using Microsoft.AspNetCore.Mvc;
using PizzaKing.Interfaces;
using PizzaKing.Models;
using PizzaKing.ViewModels;

namespace PizzaKing.Controllers
{
    public class PizzaBuilderController : Controller
    {
        private readonly IIngradient _ingredientRepo;

        public PizzaBuilderController(IIngradient ingredientRepo)
        {
            _ingredientRepo = ingredientRepo;
        }

        // GET: /PizzaBuilder
        public async Task<IActionResult> Index()
        {
            var vm = new PizzaBuilderViewModel();
            await PopulateIngredientsAsync(vm);
            // установить значения по умолчанию (если есть)
            vm.SelectedCrustId = vm.Crusts.FirstOrDefault(c => c.IsDefault)?.Id ?? vm.Crusts.FirstOrDefault()?.Id ?? 0;
            vm.SelectedSauceId = vm.Sauces.FirstOrDefault(s => s.IsDefault)?.Id ?? vm.Sauces.FirstOrDefault()?.Id ?? 0;
            vm.SelectedToppingIds = vm.Toppings.Where(t => t.IsDefault).Select(t => t.Id).ToList();

            return View(vm);
        }

        // POST: /PizzaBuilder/Build
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Build(PizzaBuilderViewModel model)
        {
            // проверить обязательные выборы
            if (model.SelectedCrustId == 0)
            {
                ModelState.AddModelError(nameof(model.SelectedCrustId), "Выберите корж.");
            }

            if (model.SelectedSauceId == 0)
            {
                ModelState.AddModelError(nameof(model.SelectedSauceId), "Выберите соус.");
            }

            // при ошибке - заново заполнить списки и вернуть форму
            if (!ModelState.IsValid)
            {
                await PopulateIngredientsAsync(model);
                return View("Index", model);
            }

            // получить объекты ингредиентов и расчитать цену
            var crust = await _ingredientRepo.GetByIdAsync(model.SelectedCrustId);
            var sauce = await _ingredientRepo.GetByIdAsync(model.SelectedSauceId);

            var toppingTasks = (model.SelectedToppingIds ?? new List<int>()).Select(id => _ingredientRepo.GetByIdAsync(id));
            var toppings = (await Task.WhenAll(toppingTasks)).Where(t => t != null).ToList();

            decimal total = 0m;
            if (crust != null) total += crust.Price;
            if (sauce != null) total += sauce.Price;
            total += toppings.Sum(t => t.Price);

            var summary = new PizzaSummaryViewModel
            {
                Crust = crust,
                Sauce = sauce,
                Toppings = toppings,
                TotalPrice = total
            };

            return View("Summary", summary);
        }

        // вспомогательный метод для заполнения списков ингредиентов в VM
        private async Task PopulateIngredientsAsync(PizzaBuilderViewModel vm)
        {
            vm.Crusts = await _ingredientRepo.GetByCategoryAsync(IngredientCategory.Crust);
            vm.Sauces = await _ingredientRepo.GetByCategoryAsync(IngredientCategory.Sauce);
            vm.Toppings = await _ingredientRepo.GetByCategoryAsync(IngredientCategory.Topping);
        }
    }
}
