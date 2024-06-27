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
    public class Ingredient
    {
        public string Name { get; set; }
        public float Quantities { get; set; }
        public string Measurements { get; set; }
        public int Calories { get; set; }
        public string FoodGroup { get; set; }

        //--------------------------------------------------------------------------------------------------------------------------------------//
        public Ingredient(string name, float quantities, string measurements, int calories, string foodGroup)
        {
            Name = name;
            Quantities = quantities;
            Measurements = measurements;
            Calories = calories;
            FoodGroup = foodGroup;
        }
    }
}
//--------------------------------------------------END OF FILE---------------------------------------------------------------------------------//


