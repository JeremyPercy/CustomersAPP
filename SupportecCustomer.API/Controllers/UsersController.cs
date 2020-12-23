using System;
using System.Collections.Generic;
using System.Security.Claims;
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
    public class UsersController : ControllerBase
    {
        private readonly ICustomerRepository _repo;
        private readonly IMapper _mapper;
        public UsersController(ICustomerRepository repo, IMapper mapper)
        {
            _mapper = mapper;
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _repo.GetUsers();

            var usersToReturn = _mapper.Map<IEnumerable<UserForListDto>>(users);

            return Ok(usersToReturn);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            var user = await _repo.GetUser(id);

            var userToReturn = _mapper.Map<UserForDetailDto>(user);

            return Ok(userToReturn);
        }

        [HttpPost("company/add")]
        public async Task<IActionResult> AddCompanyToUser(AddCompanyForUserDto addCompanyForUserDto)
        {
            var company = new Company
            {
                Name = addCompanyForUserDto.Name,
                Address = addCompanyForUserDto.Address,
                Zipcode = addCompanyForUserDto.Zipcode,
                City = addCompanyForUserDto.City,
                Email = addCompanyForUserDto.Email,
                BtwCode = addCompanyForUserDto.BtwCode,
                UserId = addCompanyForUserDto.UserId
            };
            _repo.Add(company);
            await _repo.SaveAll();
            
            return StatusCode(201);
        }

        [HttpGet("company/{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            var company = await _repo.GetCompanyFromUser(id);

            return Ok(company);
        }

        [HttpPut("company/update/{id}")]
        public async Task<IActionResult> UpdateCompany(int id, CompanyForUpdateDto companyForUpdateDto)
        {
            var companyFromRepo = await _repo.GetCompanyFromUser(id);

            _mapper.Map(companyForUpdateDto, companyFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating company for user {id} failed on save");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, UserForUpdateDto userForUpdateDto)
        {
            if (id != int.Parse(User.FindFirst(ClaimTypes.NameIdentifier).Value))
                return Unauthorized();

            var userFromRepo = await _repo.GetUser(id);

            _mapper.Map(userForUpdateDto, userFromRepo);

            if (await _repo.SaveAll())
                return NoContent();

            throw new Exception($"Updating user {id} failed on save");
        }
    }
}