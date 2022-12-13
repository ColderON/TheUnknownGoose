using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyGooseLibrary.DatabaseClasses
{
    public class CategoryOfDish
    {
        public long? id { get; set; }
        public string? name { get; set; }

        public CategoryOfDish() { }

        public CategoryOfDish(IDataReader reader)
        {
            id = reader.GetInt64(reader.GetOrdinal("Id"));
            name = reader.GetString(reader.GetOrdinal("Name"));
        }

        public override string ToString()
        {
            return name;
        }
    }
}
