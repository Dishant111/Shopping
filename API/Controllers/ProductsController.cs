using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specification;
using Infrastructure.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{

    public class ProductsController : BaseApiController
    {
        private readonly IGenericRepository<Product> _productRepository;
        private readonly IGenericRepository<ProductBrand> _productBrandRepository;
        private readonly IGenericRepository<ProductType> _productTypeRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productRepository,
                                  IGenericRepository<ProductBrand> productBrandRepository,
                                  IGenericRepository<ProductType> productTypeRepository,
                                  IMapper mapper)
        {
            this._productRepository = productRepository;
            this._productBrandRepository = productBrandRepository;
            this._productTypeRepository = productTypeRepository;
            this._mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<ProductToReturnDto>>> GetProductsAsync()
        {
            var spec = new ProductsWithTypesAndBrandsSpecifiaction();

            var products = await _productRepository.ListAsync(spec);

            return Ok(_mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(products));
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProductsAsync(int id)
        {
            var spec = new ProductsWithTypesAndBrandsSpecifiaction(id);
            var product = await _productRepository.GetEntitywithSpec(spec);

            return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrandsAsync()
        {
            return Ok(await _productBrandRepository.ListAllAsync());
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypesAsync()
        {
            return Ok(await _productTypeRepository.ListAllAsync());
        }
    }
}
