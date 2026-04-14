using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using PustokApp.Data;
using PustokApp.ViewModels;

namespace PustokApp.Controllers
{
    public class BookController (AppDbContext context): Controller
    {
        public IActionResult Details (Guid id)
        {
            var book = context.Books
                .Include(b => b.Author)
                .Include(b => b.Description)
                .Include(b => b.BookImages)
                .Include(b => b.BookTags)
                .ThenInclude(bt => bt.Tag)
                .FirstOrDefault(b => b.Id == id);

            BookVm bookVm = new BookVm
            {
                Book = book,
                RelatedBooks = context.Books
                .Include(b => b.Author)
                .Include(b => b.BookImages)
                .Where(b=>b.AuthorId == book.AuthorId && b.Id != book.Id)
                .Take(4)
                .ToList()
            };
            return View(bookVm);
        }
    }
}
