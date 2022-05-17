using Blog.Data;
using Blog.Models;
using Blog.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Blog.Controllers
{
    [ApiController]
    [Route("")]
    public class CategoryController : ControllerBase
    {
        [HttpGet("v1/categories")]
        public async Task<IActionResult> GetAsync([FromServices] BlogDataContext context)
        {
            try
            {
                var categories = await context.Categories!.AsNoTracking().ToListAsync();
                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        [HttpGet("v1/categories/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] BlogDataContext context, [FromRoute] int id)
        {
            try
            {
                var category = await context.Categories!.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                if (category == null) return NotFound();
                return Ok(category);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }

        }

        [HttpPost("v1/categories")]
        public async Task<IActionResult> PostAsync([FromServices] BlogDataContext context, [FromBody] EditorCategoryViewModel model)
        {
            try
            {
                var category = new Category()
                {
                    Name = model.Name,
                    Slug = model.Slug
                };

                await context.Categories!.AddAsync(category);
                await context.SaveChangesAsync();

                return Created($"v1/categories/{category.Id}", category);
            }
            catch (System.Exception)
            {

                return BadRequest();
            }
        }

        [HttpPut("v1/categories/{id:int}")]
        public async Task<IActionResult> PutAsync([FromServices] BlogDataContext context, [FromRoute] int id, [FromBody] EditorCategoryViewModel model)
        {
            
                var category = await context.Categories!.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null) return NotFound();
                category.Name = model.Name;
                category.Slug = model.Slug;
                context.Categories!.Update(category);
                await context.SaveChangesAsync();
                return Ok();
            
            

        }

        [HttpDelete("v1/categories/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromServices] BlogDataContext context, [FromRoute] int id)
        {
            try
            {
                var category = await context.Categories!.FirstOrDefaultAsync(x => x.Id == id);
                if (category == null) return NotFound();
                context.Categories!.Remove(category);
                await context.SaveChangesAsync();

                return Ok();
            }
            catch (System.Exception)
            {

                return BadRequest();
            }


        }
    }
}