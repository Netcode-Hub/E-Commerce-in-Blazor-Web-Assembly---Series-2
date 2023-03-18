using Azure;
using ORMExplained.Shared.DTO;
using ORMExplained.Shared.Models;

namespace ORMExplained.Server.Repositories.CategoryRepositories
{
    public class CategoryRepo : ICategoryRepo
    {
        private readonly AppDbContext appDbContext;
        public CategoryRepo(AppDbContext appDbContext)
        {
            this.appDbContext = appDbContext;
        }

        public async Task<ServiceModel<Category>> AddCategory(Category newCategory)
        {
            var response = new ServiceModel<Category>();
            if(newCategory != null)
            {
                var category = await appDbContext.Categories.FirstOrDefaultAsync(c => c.Name == newCategory.Name);
                if(category != null)
                {
                    response.Message = "Category already created !";
                    response.Success = false;
                    response.CssClass = "info fw-bold";
                }
                else
                {
                    newCategory.Url = newCategory.Name!.ToLower().Replace(" ", "-");
                    appDbContext.Categories.Add(newCategory);
                    await appDbContext.SaveChangesAsync();
                    response.Message = "Category created!";
                    response.Success = true;
                    response.CssClass = "success fw-bold";
                    var list = await GetCategories();
                    response.List = list.List;
                }
            }
            else
            {
                response.Message = "Category object is empty !";
                response.Success = false;
                response.CssClass = "danger fw-bold";
            }
            return response;
        }

        public async Task<ServiceModel<Category>> DeleteCategory(int id)
        {
            var response = new ServiceModel<Category>();
            if (id > 0)
            {
                var category = await GetCategory(id);
                if(category != null)
                {
                    appDbContext.Categories.Remove(category!.Single!);
                    await appDbContext.SaveChangesAsync();
                    response.Message = $"Category with the name {category.Single?.Name} deleted!";
                    response.Success = false;
                    response.CssClass = "danger fw-bold";
                    response.Single = category.Single;
                    var list = await GetCategories();
                    response.List = list.List;
                }
                else
                {
                    response.Message = category!.Message;
                    response.Success = category.Success;
                    response.CssClass = category.CssClass;
                }
            }
            else
            {
                response.Message = "Invalid category!";
                response.Success = false;
                response.CssClass = "danger fw-bold";
            }
            return response;
        }

        public async Task<ServiceModel<Category>> GetCategories()
        {
            var response = new ServiceModel<Category>();
            var result = await appDbContext.Categories.ToListAsync();
            if(result != null)
            {
                response.List = result;
                response.Message = "Categories found!";
                response.CssClass = "success fw-bold";
                response.Success = true;
            }
            else
            {
                response.Message = "Categories not found!";
                response.CssClass = "info fw-bold";
                response.Success = false;
            }
            return response;
        }

        public async Task<ServiceModel<Category>> GetCategory(int id)
        {
            var response = new ServiceModel<Category>();
            if(id > 0)
            {
                var category = await appDbContext.Categories.SingleOrDefaultAsync(c => c.Id == id);
                if(category != null)
                {
                    response.Message = "Category  found!";
                    response.CssClass = "success fw-bold";
                    response.Success = true;
                    response.Single = category;
                }
                else
                {
                    response.Message = "Category not found!";
                    response.CssClass = "danger fw-bold";
                    response.Success = false;
                }
            }
            else
            {
                response.Message = "Invalid category!";
                response.CssClass = "danger fw-bold";
                response.Success = false;
            }

            return response;
        }

        public async Task<ServiceModel<Category>> UpdateCategory(Category newCategory)
        {
            var response = new ServiceModel<Category>();
            if( newCategory != null)
            {
                var category = await GetCategory(newCategory.Id);
                if(category.Single != null)
                {
                    category.Single.Name = newCategory.Name;
                    category.Single.Url = newCategory.Name!.ToLower().Replace(" ", "-");
                    category.Single.Image = newCategory.Image;
                    await appDbContext.SaveChangesAsync();
                    response.Message = "Category updated!";
                    response.CssClass = "success fw-bold";
                    response.Single = category.Single;
                    response.Success = true;
                    var categories = await GetCategories();
                    response.List = categories.List;
                }
                else
                {
                    response.Message = category.Message;
                    response.CssClass = category.CssClass;
                    response.Success = category.Success;
                }
            }
            else
            {
                response.Message = "Category object is empty!";
                response.CssClass = "danger fw-bold";
                response.Success = false;
            }
            return response;
        }
    }
}
