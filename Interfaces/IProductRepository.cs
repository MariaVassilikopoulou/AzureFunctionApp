﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheFunctionAppAzure.Models;

namespace TheFunctionAppAzure.Interfaces
{
    public interface IProductRepository : IRepository<Product>
    {
    }
}
