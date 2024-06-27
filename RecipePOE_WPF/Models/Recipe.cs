using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Aman Adams
/// ST10290748
/// PROG6221
/// PROG POE PART 3
/// Code included in this POE has been created with using this website as help: https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/builtin-types/arrays 
/// </summary>

namespace Recipe_POE
{
    public class Recipe

    {
        public string Name { get; set; }
        public List<Ingredient> Ingredients { get; set; }
        public List<string> Steps { get; set; }

        public delegate void calorieNotification(string message); //Delegate for calorie notification
        public event calorieNotification OnCaloriesExceeded; //Event for calorie notification

        public delegate int calorieCalculator(); //Delegate for calorie calculation
        public calorieCalculator calculateTotalCaloriesDelegate; //Delegate instance

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Constructor to initialize a recipe with its name, ingredients, steps and calorie calculation
        public Recipe(string name)
        {
            this.Name = name;
            this.Ingredients = new List<Ingredient>();
            this.Steps = new List<string>();
            this.calculateTotalCaloriesDelegate = calculateTotalCalories; //Assign the method to the delegate
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Add an ingredient to the recipe
        public void AddIngredient(Ingredient ingredient)
        {
            Ingredients.Add(ingredient);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Add a step to the recipe
        public void AddStep(string step)
        {
            Steps.Add(step);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Display the recipe's details including ingredients, steps, and total calories
        public void displayRecipe()
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine("--------------------------");
            Console.WriteLine("Recipe Name: {0}", Name);

            Console.WriteLine("\n\nIngredients:");
            Console.WriteLine("------------");
            for (int i = 0; i < Ingredients.Count; i++)
            {
                var ingredient = Ingredients[i];
                Console.WriteLine($"- {ingredient.Name}: {ingredient.Quantities} {ingredient.Measurements}, {ingredient.Calories} calories, {ingredient.FoodGroup}");
            }

            Console.WriteLine("\nSteps:");
            Console.WriteLine("------");
            for (int i = 0; i < Steps.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {Steps[i]}");
            }

            int totalCalories = calculateTotalCaloriesDelegate(); //Use the delegate to calculate total calories
            Console.WriteLine($"\nTotal Calories: {totalCalories}");
            if (totalCalories > 300)
            {
                OnCaloriesExceeded?.Invoke("Warning: Total calories exceed 300!"); //Notify if calories exceed 300
            }
            Console.WriteLine("--------------------------");
            Console.ResetColor();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Calculate the total calories of the recipe
        public int calculateTotalCalories()
        {
            int totalCalories = 0;
            for (int i = 0; i < Ingredients.Count; i++)
            {
                totalCalories += Ingredients[i].Calories;
            }
            return totalCalories;
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Scale the quantities of ingredients by a given factor
        public void scaleRecipe(float factor)
        {
            for (int i = 0; i < Ingredients.Count; i++)
            {
                Ingredients[i].Quantities *= factor;
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Reset the quantities of ingredients by dividing by the scaling factor
        public void resetQuantities(float factor)
        {
            for (int i = 0; i < Ingredients.Count; i++)
            {
                Ingredients[i].Quantities /= factor;
            }
        }

        internal void resetQuantities()
        {
            throw new NotImplementedException();
        }

        internal void ClearRecipe()
        {
            throw new NotImplementedException();
        }
    }
}
//--------------------------------------------------END OF FILE---------------------------------------------------------------------------------//

