using System.Security.Cryptography.X509Certificates;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using TheFunctionAppAzure.Db;
using TheFunctionAppAzure.Interfaces;
using TheFunctionAppAzure.Models;

namespace TheFunctionAppAzure
{
    public class ProductApi
    {
        private readonly ILogger<ProductApi> _logger;
        private readonly IProductRepository _productRepository;
   

        public ProductApi(ILogger<ProductApi> logger, IProductRepository productRepository)
        {
            _logger = logger;
            _productRepository = productRepository;

        }

        //Get ALL Products

        [Function("GetProducts")]
        public async Task< IActionResult> GetProducts([HttpTrigger(AuthorizationLevel.Function, "get", Route="product")] HttpRequest req)
        {
            try
            {
                _logger.LogInformation("Get ALL products");
                var products= await _productRepository.GetAllAsync();
                return new OkObjectResult(products);

            }
            catch (Exception ex)

            {
                _logger.LogError($"There is a problem {ex.Message}");
                throw;
            }
          
           
        }

        //Get Product by ID

        [Function("GetProduct")]
        public async Task<IActionResult> GetProduct([HttpTrigger(AuthorizationLevel.Function, "get", Route = "product/{id}")] HttpRequest req, string id)
        {
            try
            {
                _logger.LogInformation("Get product by ID");
                var product = await _productRepository.GetByIdAsync(id);
                return new OkObjectResult (product);

            }
            catch (Exception ex)

            {
                _logger.LogError($"There is a problem {ex.Message}");
                throw;
            }


        }

        //Create Product

        [Function("CreatProduct")]
        public async Task<IActionResult> CreatProduct([HttpTrigger(AuthorizationLevel.Function, "post", Route = "product")] HttpRequest req)
        {
            try
            {
                _logger.LogInformation("Create product");

                string requestData = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<CreateProduct>(requestData);
                var product = new Product()
                {
                    Name = data.Name
                };

                await _productRepository.AddAsync(product);
                return new OkObjectResult($"A new product was added: {product.Name}");

            }
            catch (Exception ex)

            {
                _logger.LogError($"There is a problem {ex.Message}");
                throw;
            }


        }


        [Function("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct([HttpTrigger(AuthorizationLevel.Function, "put", Route = "product/{id}")] HttpRequest req, string id)
        {
            try
            {
                _logger.LogInformation("Update product");

                string requestData = await new StreamReader(req.Body).ReadToEndAsync();
                var data = JsonConvert.DeserializeObject<UpdateProduct>(requestData);

                var existingProduct = await _productRepository.GetByIdAsync(id);
                if (existingProduct==null)
                {
                    return new NotFoundResult();  
                }

                existingProduct.Name = data.Name;
                existingProduct.Collected = data.Collected;

                await _productRepository.UpdateAsync(existingProduct);
                return new OkObjectResult($"This product was Updated : {existingProduct.Name}");

            }
            catch (Exception ex)

            {
                _logger.LogError($"There is a problem {ex.Message}");
                throw;
            }


        }


        [Function("DeleteProduct")]
        public async Task<IActionResult> DeleteProduct([HttpTrigger(AuthorizationLevel.Function, "delete", Route = "product/{id}")] HttpRequest req, string id)
        {
            try
            {
                _logger.LogInformation("Delete product");

               

                var product = await _productRepository.GetByIdAsync(id);
                if (product == null)
                {
                    return new NotFoundResult();
                }

               

                await _productRepository.DeleteAsync(id);
                return new OkObjectResult($"This product was deleted: {product.Name}");

            }
            catch (Exception ex)

            {
                _logger.LogError($"There is a problem {ex.Message}");
                throw;
            }


        }



    }
}
