using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TheFunctionAppAzure.Models
{
    public class Product
    {
        public string Id { get; set; } = Guid.NewGuid().ToString();
        public string Name { get; set; }
        public DateTime Created { get; set; }

        public bool Collected { get; set; }
    }


    public class CreateProduct
    {
        public string Name { get; set; }
    }

    public class UpdateProduct
    {
        public string Name { get; set; }
        public bool Collected { get; set; }
    }
}
