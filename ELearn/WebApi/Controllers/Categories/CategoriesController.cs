using System;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using ELearn.Application.Repositories;
using ELearn.Domain;
using Microsoft.AspNetCore.Mvc;

namespace ELearn.WebApi.Controllers.Categories
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriesController : Controller
    {

        private readonly ICategoriesRepo _repo;


        public CategoriesController(ICategoriesRepo repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IActionResult> Get([Required] Guid idx)
        {
            var c = await _repo.Read(idx);
            if (c == null)
            {
                return NotFound("No such Categories");
            }

            return Ok(c);

        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var l = await _repo.GetAll();

            return Ok(l);
        }

        [HttpPut]
        public async Task<IActionResult> Create([FromBody] CreateCategoryRequest request)
        {
            if (!TryValidateModel(request, nameof(request)))
            {
                return BadRequest("Invalid Data");
            }

            await _repo.Create(new Category(Guid.NewGuid(), request.Name));
            return Ok();
        }

        [HttpPost]
        public async Task<IActionResult> Modify([Required] Guid idx, [FromBody] CreateCategoryRequest request)
        {
            if (!TryValidateModel(request, nameof(request)))
            {
                return BadRequest("Invalid Data");
            }

            await _repo.Update(idx, new Category(idx, request.Name));

            return Ok();

        }

        [HttpDelete]
        public async Task<IActionResult> Delete([Required] Guid idx)
        {
            await _repo.Delete(idx);

            return Ok();
        }
        
    }
}