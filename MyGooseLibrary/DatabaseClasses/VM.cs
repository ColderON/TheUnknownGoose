using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheUnknownGoose.MyGooseLibrary.DatabaseClasses
{
    public class VM
    {
        public long? id { get; set; }
        public string? name { get; set; }
        public int? numberOfCalories { get; set; }
        public string? measure { get; set; }
        public long? categoryId { get; set; }
        public string? comment { get; set; }
        public int? countOfitems { get; set; }
       

        public override string ToString()
        {
            if(measure == "per 355 ml")
            {
                return $"{name} - {numberOfCalories} kcal per {countOfitems} can(s)";
            }
            else if(measure == "stk")
            {
                return $"{name} - {numberOfCalories} kcal per {countOfitems} piece(s)";
            }
            else if( measure == "per 100 grams")
            {
                return $"{name} - {numberOfCalories} kcal per {countOfitems} gramms";
            }
            else
            {
                return $"{name} - {numberOfCalories} kcal per {countOfitems} portion(s)";
            }
        }
    }
}
