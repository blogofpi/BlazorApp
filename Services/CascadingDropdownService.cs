using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BlazorApp.Data;
using Microsoft.EntityFrameworkCore;

namespace BlazorApp.Services
{
    public interface ICascadingDropdownService
    {
        Task<List<Category>> GetCategories();
        List<Product> GetProducts(int id);
    }
    public class CascadingDropdownService : ICascadingDropdownService
    {
        private readonly ApplicationDbContext _context;

        public CascadingDropdownService(ApplicationDbContext context)
        {
            _context = context;
        }
       
        public async Task<List<Category>> GetCategories()
        {
            return await _context.Categories.ToListAsync();
        }

        public List<Product> GetProducts(int id)
        {
            var products = _context.Products.Where(x => x.CategoryId.Equals(id)).ToList();
            return products;
        }
       
    }
}
