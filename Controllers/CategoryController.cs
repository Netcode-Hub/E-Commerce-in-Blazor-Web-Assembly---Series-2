using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ORMExplained.Server.Repositories.CategoryRepositories;
using ORMExplained.Shared.DTO;
using ORMExplained.Shared.Models;

namespace ORMExplained.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly ICategoryRepo categoryRepo;
        public CategoryController(ICategoryRepo categoryRepo)
        {
            this.categoryRepo = categoryRepo;
        }

        [HttpGet]
        public async Task<ActionResult<ServiceModel<Category>>> GetCategories()
        {
            return Ok(await categoryRepo.GetCategories());
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<ServiceModel<Category>>> GetCategory(int id)
        {
            return Ok(await categoryRepo.GetCategory(id));
        }

        [HttpPost]
        public async Task<ActionResult<ServiceModel<Category>>> AddCategory(Category newCategory)
        {
            return Ok(await categoryRepo.AddCategory(newCategory));
        }

        [HttpDelete("{id:int}")]
        public async Task<ActionResult<ServiceModel<Category>>> DeleteCategory(int id)
        {
            return Ok(await categoryRepo.DeleteCategory(id));
        }

        [HttpPut]
        public async Task<ActionResult<ServiceModel<Category>>> UpdateCategory(Category newCategory)
        {
            return Ok(await categoryRepo.UpdateCategory(newCategory));
        }
    }
}
