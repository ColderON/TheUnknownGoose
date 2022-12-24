using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGooseLibrary.DatabaseClasses
{
    public class Dish
    {
        public long? id { get; set; }
        public string? name { get; set; }
        public int? numberOfCalories { get; set; }
        public string? comment { get; set; }
        public long categoryId { get; set; }

        public Dish() { }

        public Dish(IDataReader reader)
        {
            id = reader.GetInt64(reader.GetOrdinal("Id"));
            name = reader.GetString(reader.GetOrdinal("Name"));
            numberOfCalories = reader.GetInt32(reader.GetOrdinal("NumberOfCalories"));
            if (!reader.IsDBNull(reader.GetOrdinal("Comment")))
            {
                comment = reader.GetString(reader.GetOrdinal("Comment"));
            }
            categoryId = reader.GetInt64(reader.GetOrdinal("CategoryId"));
        }

        public override string ToString()
        {
            return $"{name}";
        }
    }
}
