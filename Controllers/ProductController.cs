using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

using Nahang.Data;
using Nahang.Data.Models;
using Nahang.Shared;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nahang.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class ProductController : ControllerBase
    {
        private readonly DataContext dataContext;

        public ProductController(DataContext dataContext)
        {
            this.dataContext = dataContext;
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResult), 200)]
        public async Task<ApiResult> CreateProduct(Product pro)
        {
            dataContext.Add(pro);
            await dataContext.SaveChangesAsync();


            return new ApiResult
            {
                Data = pro,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResult), 200)]
        public async Task<ApiResult> CreateCategory(Category cat)
        {
            dataContext.Add(cat);
            await dataContext.SaveChangesAsync();


            return new ApiResult
            {
                Data = cat,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResult), 200)]
        public async Task<ApiResult> GetProduct(int id)
        {
           var res = dataContext.Products.FirstOrDefault(n=>n.ProductId == id);

            return new ApiResult
            {
                Data = res,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResult), 200)]
        public async Task<ApiResult> GetCategory(int id)
        {
            var res = dataContext.Categories.FirstOrDefault(n => n.CategoryId == id);

            return new ApiResult
            {
                Data = res,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResult), 200)]
        public async Task<ApiResult> GetAllProduct()
        {
            var res = dataContext.Products;

            return new ApiResult
            {
                Data = res,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResult), 200)]
        public async Task<ApiResult> GetAllCategory()
        {
            var res = dataContext.Categories;

            return new ApiResult
            {
                Data = res,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResult), 200)]
        public async Task<ApiResult> DeleteProduct(int id)
        {
            var res = new Product { ProductId = id };
            dataContext.Attach(res);
            dataContext.Remove(res);
            await dataContext.SaveChangesAsync();


            return new ApiResult
            {
                Data = res,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
        [HttpPost("[action]")]
        [ProducesResponseType(typeof(ApiResult), 200)]
        public async Task<ApiResult> DeleteCategory(int id)
        {
            var res = new Category { CategoryId = id };
            dataContext.Attach(res);
            dataContext.Remove(res);
            await dataContext.SaveChangesAsync();
            return new ApiResult
            {
                Data = res,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }
    }
}
