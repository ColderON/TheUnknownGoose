using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGooseLibrary.DatabaseClasses
{
    public class Product
    {
        public long? id { get; set; }
        public string? name { get; set; }
        public int? numberOfCalories { get; set; }
        public string? measure { get; set; }
        public long? categoryId { get; set; }
        public string? comment { get; set; }

        public Product() { }

        public Product(IDataReader reader) {

            id = reader.GetInt64(reader.GetOrdinal("Id"));
            name = reader.GetString(reader.GetOrdinal("Name"));           
            numberOfCalories = reader.GetInt32(reader.GetOrdinal("NumberOfCalories"));
            measure = reader.GetString(reader.GetOrdinal("Measure"));
            categoryId = reader.GetInt64(reader.GetOrdinal("CategoryId"));

            if (!reader.IsDBNull(reader.GetOrdinal("Comment")))
            {
                comment = reader.GetString(reader.GetOrdinal("Comment"));
            }
        }

        public override string ToString()
        {
            return $"{name}";
        }
    }
}
