using System.Collections.Generic;
using BookApi.Models;

namespace BookApi.Services
{
    public interface ICategoryRepository
    {
        ICollection<Category> GetCategories();
        Category GetCategory(int categoryId);

        ICollection<Category> GetAllCategoriesForABook(int bookId);
        ICollection<Book> GetAllBooksForCategory(int categoryId);
        bool CategoryExists(int categoryId);

        bool IsDuplicateCategory(int categoryId, string categoryName);
    }
}
