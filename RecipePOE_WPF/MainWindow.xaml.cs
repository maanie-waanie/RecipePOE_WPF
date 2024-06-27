using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml;
using Recipe_POE;


namespace RecipePOE_WPF
{
    public partial class MainWindow : Window
    {
        private CookBook recipeBook = new CookBook();
        private Recipe currentRecipe = null;

        public MainWindow()
        {
            InitializeComponent();
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //AddRecipeButton click event
        private void ButtonAddRecipe_Click(object sender, RoutedEventArgs e)
        {
            DisplayPanel.Children.Clear();

            // Create UI elements for adding a new recipe
            var nameLabel = new Label { Content = "Enter the recipe name:" };
            var nameTextBox = new TextBox { Name = "RecipeName" };

            var ingredientNoLabel = new Label { Content = "Enter the number of ingredients:" };
            var ingredientNoTextBox = new TextBox { Name = "IngredientNo" };

            var addButton = new Button { Content = "Add Ingredients and Steps" };
            addButton.Click += (s, args) => AddIngredientsAndSteps(nameTextBox, ingredientNoTextBox);

            DisplayPanel.Children.Add(nameLabel);
            DisplayPanel.Children.Add(nameTextBox);
            DisplayPanel.Children.Add(ingredientNoLabel);
            DisplayPanel.Children.Add(ingredientNoTextBox);
            DisplayPanel.Children.Add(addButton);
        }

        //-------------------------------------------------------------------------------------------------------------------------------------//
        // Method to add ingredients and steps
        private void AddIngredientsAndSteps(TextBox nameTextBox, TextBox ingredientNoTextBox)
        {
            DisplayPanel.Children.Clear();
            currentRecipe = new Recipe(nameTextBox.Text);

            if (int.TryParse(ingredientNoTextBox.Text, out int ingredientNo))
            {
                for (int i = 0; i < ingredientNo; i++)
                {
                    var ingredientLabel = new Label { Content = $"Enter the Name of Ingredient {i + 1}:" };
                    var ingredientTextBox = new TextBox { Name = $"IngredientName{i}" };

                    var quantityLabel = new Label { Content = $"Enter the Quantity of Ingredient {i + 1}:" };
                    var quantityTextBox = new TextBox { Name = $"IngredientQuantity{i}" };

                    var measurementLabel = new Label { Content = $"Enter the Unit of Measurement for Ingredient {i + 1} (mls, cups, g, tspn, tbspn):" };
                    var measurementTextBox = new TextBox { Name = $"IngredientMeasurement{i}" };

                    var caloriesLabel = new Label { Content = $"Enter the Number of Calories for Ingredient {i + 1}:" };
                    var caloriesTextBox = new TextBox { Name = $"IngredientCalories{i}" };

                    var foodGroupLabel = new Label { Content = $"Enter the Food Group for Ingredient {i + 1} (Vegetables, Fruits, Grains/Carbs, Protein, Dairy, Oils/Solid Fats, Other):" };
                    var foodGroupTextBox = new TextBox { Name = $"IngredientFoodGroup{i}" };

                    DisplayPanel.Children.Add(ingredientLabel);
                    DisplayPanel.Children.Add(ingredientTextBox);
                    DisplayPanel.Children.Add(quantityLabel);
                    DisplayPanel.Children.Add(quantityTextBox);
                    DisplayPanel.Children.Add(measurementLabel);
                    DisplayPanel.Children.Add(measurementTextBox);
                    DisplayPanel.Children.Add(caloriesLabel);
                    DisplayPanel.Children.Add(caloriesTextBox);
                    DisplayPanel.Children.Add(foodGroupLabel);
                    DisplayPanel.Children.Add(foodGroupTextBox);
                }

                var stepNoLabel = new Label { Content = "Enter the Number of Steps:" };
                var stepNoTextBox = new TextBox { Name = "StepNo" };

                var stepsButton = new Button { Content = "Add Steps" };
                stepsButton.Click += (s, args) => AddSteps(stepNoTextBox, ingredientNo);

                DisplayPanel.Children.Add(stepNoLabel);
                DisplayPanel.Children.Add(stepNoTextBox);
                DisplayPanel.Children.Add(stepsButton);
            }
        }
        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Method to add steps
        private void AddSteps(TextBox stepNoTextBox, int ingredientNo)
        {
            DisplayPanel.Children.Clear();

            if (int.TryParse(stepNoTextBox.Text, out int stepsNo))
            {
                for (int i = 0; i < stepsNo; i++)
                {
                    var stepLabel = new Label { Content = $"Enter step {i + 1}:" };
                    var stepTextBox = new TextBox { Name = $"Step{i}" };

                    DisplayPanel.Children.Add(stepLabel);
                    DisplayPanel.Children.Add(stepTextBox);
                }

                var saveButton = new Button { Content = "Save Recipe" };
                saveButton.Click += (s, args) => SaveRecipe(ingredientNo, stepsNo);

                DisplayPanel.Children.Add(saveButton);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Method to save the recipe
        private void SaveRecipe(int ingredientNo, int stepsNo)
        {
            foreach (var child in DisplayPanel.Children)
            {
                if (child is TextBox textBox)
                {
                    if (textBox.Name.StartsWith("IngredientName"))
                    {
                        var index = int.Parse(textBox.Name.Replace("IngredientName", ""));
                        var name = textBox.Text;
                        var quantity = float.Parse(DisplayPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == $"IngredientQuantity{index}")?.Text);
                        var measurement = DisplayPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == $"IngredientMeasurement{index}")?.Text;
                        var calories = int.Parse(DisplayPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == $"IngredientCalories{index}")?.Text);
                        var foodGroup = DisplayPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == $"IngredientFoodGroup{index}")?.Text;

                        var ingredient = new Ingredient(name, quantity, measurement, calories, foodGroup);
                        currentRecipe.AddIngredient(ingredient);
                    }
                    else if (textBox.Name.StartsWith("Step"))
                    {
                        var step = textBox.Text;
                        currentRecipe.AddStep(step);
                    }
                }
            }

            recipeBook.AddRecipe(currentRecipe);
            DisplayPanel.Children.Clear();
            var successLabel = new Label { Content = "New recipe entered!" };
            DisplayPanel.Children.Add(successLabel);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //DisplayRecipeButton click event
        private void ButtonDisplayRecipe_Click(object sender, RoutedEventArgs e)
        {
            DisplayPanel.Children.Clear();

            var nameLabel = new Label { Content = "Enter the Name of the Recipe to Display:" };
            var nameTextBox = new TextBox { Name = "RecipeDisplayName" };
            var displayButton = new Button { Content = "Display Recipe" };
            displayButton.Click += DisplayRecipe;
           

            DisplayPanel.Children.Add(nameLabel);
            DisplayPanel.Children.Add(nameTextBox);
            DisplayPanel.Children.Add(displayButton);
        }
        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Method to display the recipe
        private void DisplayRecipe(object sender, RoutedEventArgs e)
        {
            var recipeName = DisplayPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "RecipeDisplayName")?.Text;
            currentRecipe = recipeBook.GetRecipeByName(recipeName);

           DisplayPanel.Children.Clear(); // Clear any previous content

            if (currentRecipe != null)
            {
               
                var recipeDetailsLabel = new Label
                {
                    Content = $"Recipe Name: {currentRecipe.Name}",
                    FontWeight = FontWeights.Bold,
                    FontSize = 16,
                    Margin = new Thickness(0, 10, 0, 10)
                };
                DisplayPanel.Children.Add(recipeDetailsLabel);

                var ingredientsLabel = new Label
                {
                    Content = "Ingredients:",
                    FontWeight = FontWeights.Bold,
                    FontSize = 14,
                    Margin = new Thickness(0, 10, 0, 5)
                };
                DisplayPanel.Children.Add(ingredientsLabel) ;

                
                for (int i = 0; i < currentRecipe.Ingredients.Count; i++)
                {
                    
                    var ingredientDetails = new Label
                    {
                        Content = $" {currentRecipe.Ingredients[i]}: {currentRecipe.Ingredients[i].Quantities} {currentRecipe.Ingredients[i].Measurements}, {currentRecipe.Ingredients[i].Calories} calories, {currentRecipe.Ingredients[i].FoodGroup}",
                        Margin = new Thickness(10, 0, 0, 0)
                    };
                    DisplayPanel.Children.Add(ingredientDetails);
                }

                var stepsLabel = new Label
                {
                    Content = "Steps:",
                    FontWeight = FontWeights.Bold,
                    FontSize = 14,
                    Margin = new Thickness(0, 10, 0, 5)
                };
                DisplayPanel.Children.Add(stepsLabel);

                for (int i = 0; i < currentRecipe.Steps.Count; i++)
                {
                    var stepDetails = new Label
                    {
                        Content = $"{i + 1}. {currentRecipe.Steps[i]}",
                        Margin = new Thickness(10, 0, 0, 0)
                    };
                    DisplayPanel.Children.Add(stepDetails);
                }

                int totalCalories = currentRecipe.calculateTotalCaloriesDelegate();
                var totalCaloriesLabel = new Label
                {
                    Content = $"Total Calories: {totalCalories}",
                    FontWeight = FontWeights.Bold,
                    Margin = new Thickness(0, 10, 0, 0)
                };
                DisplayPanel.Children.Add(totalCaloriesLabel);

                if (totalCalories > 300)
                {
                    var warningLabel = new Label
                    {
                        Content = "Warning: Total calories exceed 300!",
                        Foreground = new SolidColorBrush(Colors.Red),
                        FontWeight = FontWeights.Bold,
                        Margin = new Thickness(0, 5, 0, 0)
                    };
                    DisplayPanel.Children.Add(warningLabel);
                }
            }
            else
            {
                var notFoundLabel = new Label { Content = "Recipe not found." };
                DisplayPanel.Children.Add(notFoundLabel);
            }
        }


        //--------------------------------------------------------------------------------------------------------------------------------------//
        //DisplayAllRecipesButton click event
        private void ButtonDisplayAll_Click(object sender, RoutedEventArgs e)
        {
            DisplayPanel.Children.Clear();

            var allRecipes = recipeBook.ListRecipes();
            foreach (var recipeName in allRecipes)
            {
                var recipeLabel = new Label { Content = recipeName };
                DisplayPanel.Children.Add(recipeLabel);
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //ScaleRecipeButton click event
        private void ButtonScaleRecipe_Click(object sender, RoutedEventArgs e)
        {
            DisplayPanel.Children.Clear();

            var nameLabel = new Label { Content = "Enter the Name of the Recipe to Scale:" };
            var nameTextBox = new TextBox { Name = "RecipeScaleName" };

            var scaleLabel = new Label { Content = "Enter the Scale Factor (0.5, 2, or 3):" };
            var scaleTextBox = new TextBox { Name = "RecipeScaleFactor" };

            var scaleButton = new Button { Content = "Scale Recipe" };
            scaleButton.Click += ScaleRecipe;

            DisplayPanel.Children.Add(nameLabel);
            DisplayPanel.Children.Add(nameTextBox);
            DisplayPanel.Children.Add(scaleLabel);
            DisplayPanel.Children.Add(scaleTextBox);
            DisplayPanel.Children.Add(scaleButton);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Method to Scale a Recipe
        private void ScaleRecipe(object sender, RoutedEventArgs e)
        {
            var recipeName = DisplayPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "RecipeScaleName")?.Text;
            var scaleFactorText = DisplayPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "RecipeScaleFactor")?.Text;

            if (float.TryParse(scaleFactorText, out float scaleFactor))
            {
                currentRecipe = recipeBook.GetRecipeByName(recipeName);
                if (currentRecipe != null)
                {
                    currentRecipe.scaleRecipe(scaleFactor);
                    var scaledRecipeDetails = currentRecipe.ToString();
                    var scaledRecipeDetailsLabel = new Label { Content = scaledRecipeDetails, MaxWidth = 500,  };
                    DisplayPanel.Children.Add(scaledRecipeDetailsLabel);
                }
                else
                {
                    var notFoundLabel = new Label { Content = "Recipe not found." };
                    DisplayPanel.Children.Add(notFoundLabel);
                }
            }
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //ResetQuantitiesButton click event
        private void ButtonResetQuantities_Click(object sender, RoutedEventArgs e)
        {
            DisplayPanel.Children.Clear();

            var nameLabel = new Label { Content = "Enter the name of the recipe to reset quantities:" };
            var nameTextBox = new TextBox { Name = "RecipeResetName" };
            var resetButton = new Button { Content = "Reset Quantities" };
            resetButton.Click += resetQuantities;

            DisplayPanel.Children.Add(nameLabel);
            DisplayPanel.Children.Add(nameTextBox);
            DisplayPanel.Children.Add(resetButton);
        }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Method to Reset Quantities back to the Original ones
        private void resetQuantities(object sender, RoutedEventArgs e)
        {
           
            if (currentRecipe != null)
            {
                currentRecipe.resetQuantities();
                var resetRecipeDetails = currentRecipe.ToString();
                var resetRecipeDetailsLabel = new Label { Content = resetRecipeDetails, MaxWidth = 500, };
                DisplayPanel.Children.Add(resetRecipeDetailsLabel);
            }
            else
            {
                var notFoundLabel = new Label { Content = "Recipe not found." };
                DisplayPanel.Children.Add(notFoundLabel);
            }
        }



        //--------------------------------------------------------------------------------------------------------------------------------------//
        //FilterRecipesButton click event
        private void ButtonFilter_Click(object sender, RoutedEventArgs e)
        {
            DisplayPanel.Children.Clear();

            var calorieLabel = new Label { Content = "Enter the Maximum Number of Calories:" };
            var calorieTextBox = new TextBox { Name = "MaxCalories" };
            var filterButton = new Button { Content = "Filter Recipes" };
            filterButton.Click += FilterRecipes;

            DisplayPanel.Children.Add(calorieLabel);
            DisplayPanel.Children.Add(calorieTextBox);
            DisplayPanel.Children.Add(filterButton);
        }


        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Method to filter recipes by either ingredients, foodgroups or max calories
        private void FilterRecipes(object sender, RoutedEventArgs e)
        {
            var maxCaloriesText = DisplayPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "MaxCalories")?.Text;

            if (int.TryParse(maxCaloriesText, out int maxCalories))
            {
                var filteredRecipes = recipeBook.FilterByMaxCalories(maxCalories);
                foreach (var recipe in filteredRecipes)
                {
                    var recipeLabel = new Label { Content = recipe.Name };
                    DisplayPanel.Children.Add(recipeLabel);
                }
            }
        }


        //--------------------------------------------------------------------------------------------------------------------------------------//
        //ClearRecipeButton click event
        private void ButtonClearRecipe_Click(object sender, RoutedEventArgs e)
        {
            DisplayPanel.Children.Clear();

            var nameLabel = new Label { Content = "Enter the Name of the Recipe to Clear:" };
            var nameTextBox = new TextBox { Name = "RecipeClearName" };
            var clearButton = new Button { Content = "Clear Recipe" };
            clearButton.Click += ClearRecipe;

            DisplayPanel.Children.Add(nameLabel);
            DisplayPanel.Children.Add(nameTextBox);
            DisplayPanel.Children.Add(clearButton);
        }


        //--------------------------------------------------------------------------------------------------------------------------------------//
        //Method to Clear Recipes
        private void ClearRecipe(object sender, RoutedEventArgs e)
        {
            var recipeName = DisplayPanel.Children.OfType<TextBox>().FirstOrDefault(tb => tb.Name == "RecipeClearName")?.Text;
            currentRecipe = recipeBook.GetRecipeByName(recipeName);
            if (currentRecipe != null)
            {
                currentRecipe.ClearRecipe();
                var clearRecipeDetails = currentRecipe.ToString();
                var clearRecipeDetailsLabel = new Label { Content = clearRecipeDetails, MaxWidth = 500,  };
                DisplayPanel.Children.Add(clearRecipeDetailsLabel);
            }
            else
            {
                var notFoundLabel = new Label { Content = "Recipe not found." };
                DisplayPanel.Children.Add(notFoundLabel);
            }
        }
    }
}
//--------------------------------------------------END OF FILE---------------------------------------------------------------------------------//
