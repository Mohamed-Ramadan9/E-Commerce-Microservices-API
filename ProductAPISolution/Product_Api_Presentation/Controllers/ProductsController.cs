using E_Commerce.SharedLibrary.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProductApiApplication.DTOs;
using ProductApiApplication.DTOs.Conversions;
using ProductApiApplication.Interfaces;

namespace Product_Api_Presentation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [AllowAnonymous]
    public class ProductsController(IProduct productRepo) : ControllerBase
    {
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductDTO>>> GetProducts()
        {
            // Get all products from repo
            var products = await productRepo.GetAllAsync();
            if (!products.Any())
                return NotFound("No products avialable");

            //Convert data from entity to DTO and return it
            var (_, list) = ProductConversions.FromEntity(null, products);

            return list.Any() ? Ok(list) : NotFound("No product found");

        }


        [HttpGet("{id:int}")]
        public async Task<ActionResult<ProductDTO>> GetProduct(int id)
        {
            //Get Single product from the Repo
            var product = await productRepo.FindByIdAsync(id);
            if (product is null) return NotFound("Product requested not found");

            // convert from entity to DTO and return
            var (_product, _) = ProductConversions.FromEntity(product, null!);

            return _product is not null ? Ok(_product) : NotFound("Product not found") ;
        }

        [HttpPost]
        [Authorize(Roles ="Admin")]
        public async Task<ActionResult<Response>> CreateProduct(ProductDTO product)
        {
            // check model state is all annotations are passed 
            if(ModelState.IsValid)
            {
                var getEntity = ProductConversions.ToEntity(product);
                var response = await productRepo.CreateAsync(getEntity);
              return  response.Flag is true ? Ok(response) : BadRequest(response);

            }
            return BadRequest(ModelState);

        }

        [HttpPut]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult <Response>>UpdateProduct(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                var getEntity = ProductConversions.ToEntity(product);
                var response = await productRepo.UpdateAsync(getEntity);
                return response.Flag is true ? Ok(response) : BadRequest(response);

            }
            return BadRequest(ModelState);

        }

        [HttpDelete]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<Response>> DeleteProduct(ProductDTO product)
        {   
            var getEntity = ProductConversions.ToEntity(product);
            var response = await productRepo.DeleteAsync(getEntity);
            return response.Flag is true ? Ok(response) : BadRequest(response);
        }

    }

}
