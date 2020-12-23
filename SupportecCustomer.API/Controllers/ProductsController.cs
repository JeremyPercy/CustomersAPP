using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SupportecCustomer.API.Data;
using SupportecCustomer.API.Dtos;
using SupportecCustomer.API.Models;

namespace SupportecCustomer.API.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private readonly IProductRepository _repo;
        private readonly IMapper _mapper;
        public ProductsController(IProductRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProduct(int id)
        {
            var product = await _repo.GetProduct(id);

            var productToReturn = _mapper.Map<ProductForDetailDto>(product);

            return Ok(productToReturn);
        }

        [HttpGet("user/{id}")]
        public async Task<IActionResult> GetProducts(int id)
        {
            var products = await _repo.GetProducts(id);

            var productsToReturn = _mapper.Map<IEnumerable<ProductForListDto>>(products);

            return Ok(productsToReturn);
        }


        [HttpGet("items")]
        public async Task<IActionResult> GetItems()
        {
            var items = await _repo.GetItems();

            var itemsToReturn = _mapper.Map<IEnumerable<ItemForProductDto>>(items);

            return Ok(itemsToReturn);
        }

        [HttpPost("add")]
        public async Task<IActionResult> AddProduct(AddProductForUserDto addProductForUserDto)
        {
            var product = new Product
            {
                Name = addProductForUserDto.Name,
                PhotoUrl = addProductForUserDto.PhotoUrl,
                UserId = addProductForUserDto.UserId
            };
            _repo.Add(product);
            await _repo.SaveAll();
            return StatusCode(201);
        }

        [HttpPost("items/add")]
        public async Task<IActionResult> AddItems(AddItemToProductDto addItemToProduct)
        {
            _repo.addItemsToProduct(addItemToProduct.ProductId, addItemToProduct.ItemId);
            await _repo.SaveAll();

            return StatusCode(201);
        }

        [HttpGet("totalprice/{id}")]
        public async Task<IActionResult> GetTotalPrice(int id)
        {
            var items = await _repo.GetItemsFromProduct(id);

            decimal totalPrice = 0;

            foreach (var item in items)
            {
                totalPrice += item.Price;
            }

            return Ok(totalPrice);
        }
    }
}