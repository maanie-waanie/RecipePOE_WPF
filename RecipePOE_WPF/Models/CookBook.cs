using System;
using System.Collections.Generic;
using System.Linq;

namespace Recipe_POE
{
    public class CookBook
    {
        public List<Recipe> Recipes { get; set; }

        // Constructor to initialize the cookbook with an empty list of recipes
        public CookBook()
        {
            Recipes = new List<Recipe>();
        }

        // Add a recipe to the cookbook and sort recipes alphabetically by name
        public void AddRecipe(Recipe recipe)
        {
            Recipes.Add(recipe);
            Recipes = Recipes.OrderBy(r => r.Name).ToList();
        }

        // Display all recipe names in the cookbook
        public List<string> ListRecipes()
        {
            return Recipes.Select(r => r.Name).ToList();
        }

        // Retrieve a recipe by its name, returning null if not found
        public Recipe GetRecipeByName(string name)
        {
            return Recipes.FirstOrDefault(r => r.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        // Filter recipes by ingredient name
        public List<Recipe> FilterByIngredient(string ingredientName)
        {
            return Recipes.Where(r => r.Ingredients.Any(i => i.Name.Equals(ingredientName, StringComparison.OrdinalIgnoreCase))).ToList();
        }

        // Filter recipes by food group
        public List<Recipe> FilterByFoodGroup(string foodGroup)
        {
            return Recipes.Where(r => r.Ingredients.Any(i => i.FoodGroup.Equals(foodGroup, StringComparison.OrdinalIgnoreCase))).ToList();
        }

        // Filter recipes by maximum number of calories
        public List<Recipe> FilterByMaxCalories(int maxCalories)
        {
            return Recipes.Where(r => r.calculateTotalCalories() <= maxCalories).ToList();
        }
    }
}
