using ComplexDataValidation.Data;
using ComplexDataValidation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Helpers
{
    public class DocumentControll
    {
        private readonly ApplicationDbContext _context;
        private readonly EntitiesManager _entManager;

        public DocumentControll(ApplicationDbContext contex, EntitiesManager entManager)
        {
            _context = contex;
            _entManager = entManager;
        }

        /// <summary>
        /// Checks if book has all information filled.
        /// </summary>
        public bool BookIsFilled(Book book)
        {
            if (book.Information == null)
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if Book is last in specific person list.
        /// </summary>
        public async Task<bool> BookIsLast(string bookId, string personId)
        {
            var booksQuery = _context.Books
                       .Where(p => p.PersonId == personId)
                       .Select(p => p);
            var books = await booksQuery.ToListAsync();

            foreach (var book in books)
            {
                book.Information = await _context.Information.SingleOrDefaultAsync(x => x.Id == book.Id);
            }

            books.Sort();

            if (bookId == books.Last().Id)
            {
                return true;
            }
            return false;
        }

        /// <summary>
        /// Checks if there are any chapters in that book.
        /// </summary>
        public async Task<bool> BookIsEmpty(Book book)
        {
            var chaptersQuery = from c in _context.Chapters
                                where c.BookId == book.Id
                                orderby c.CreationDate
                                select c;
            if (await chaptersQuery.AnyAsync())
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Checks if date is youngest in specific person considering books and chapters.
        /// </summary>
        public async Task<bool> DateIsYoungest(DateTime date, string personId)
        {
            var booksQuery = _context.Books
                      .Where(p => p.PersonId == personId)
                      .Select(p => p);
            var books = await booksQuery.ToListAsync();

            foreach (var book in books)
            {
                book.Information = await _context.Information.SingleOrDefaultAsync(x => x.Id == book.Id);
            }
            books.Sort();
            var lastBook = books.Last();
            await _entManager.RetrieveChapters(lastBook.Chapters, lastBook);

            if (lastBook.Chapters == null)
            {
                return date < lastBook.Information.CreationDate;
            }
            return date < lastBook.Chapters.Last().CreationDate;
        }
    }
}
