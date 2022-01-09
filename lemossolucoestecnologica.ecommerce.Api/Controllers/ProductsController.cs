using AutoMapper;
using lemossolucoestecnologia.ecommerce.Domain.Entities;
using lemossolucoestecnologia.ecommerce.Domain.Interfaces;
using lemossolucoestecnologica.ecommerce.Api.ErrorsValidation;
using lemossolucoestecnologica.ecommerce.Api.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System.Security.Claims;

namespace lemossolucoestecnologica.ecommerce.Api.Controllers
{
    [Route("api/v1/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductsServices _productsServices;
        private readonly IMapper _mapper;

        public ProductsController(IProductsServices productsServices, IMapper mapper)
        {
            _productsServices = productsServices;
            _mapper = mapper;
        }

        [SwaggerResponse(statusCode: 201, description: "Produto registrado com sucesso", type: typeof(ProductsViewModel))]
        [SwaggerResponse(statusCode: 401, description: "Não Autorizado", type: typeof(ValidateFieldViewModel))]
        [HttpPost]
        [Route("Register")]
        [Authorize]
        public async Task<IActionResult> Register(ProductsViewModel productViewModel)
        {
           // var UserAuth = User.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            var products = _mapper.Map<Products>(productViewModel);
             
            if(products == null)
            {
                return BadRequest();
            }
            await _productsServices.Register(products);
            await _productsServices.Commit();
             
            return Created("", productViewModel);
        }

        [SwaggerResponse(statusCode: 201, description: "Produto registrado com sucesso", type: typeof(ProductsViewModel))]
        [SwaggerResponse(statusCode: 401, description: "Não Autorizado", type: typeof(ValidateFieldViewModel))]
        [HttpGet]
        [Route("GetAllProducts/{productName}")]
        [Authorize]
        public async Task<IActionResult> GetProducts(Products productName)
        {
            var prod = new  Products();
            var products = _mapper.Map<ProductsViewModel>(prod);

            var result = await _productsServices.GetProductsByName(productName);
           
            if (result == null)
            {
                return BadRequest();
            }
            
            return Ok(result.ToList());
        }
    }
}
