using ComplexDataValidation.Data;
using ComplexDataValidation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComplexDataValidation.Helpers
{
    public class EntitiesManager
    {
        private readonly ApplicationDbContext _context;

        public EntitiesManager(ApplicationDbContext context)
        {
            _context = context;
        }

        #region Retrieve

        public async Task RetrievePerson(Person person)
        {
            if (person == null)
            {
                return;
            }

            person.Credentials = await _context.Credentials.Where(x => x.Id == person.Id).FirstOrDefaultAsync();
            person.Pet = await _context.Pets.Where(x => x.Id == person.Id).FirstOrDefaultAsync();
            var booksQuery = _context.Books
                       .Where(p => p.PersonId == person.Id)
                       .Select(p => p);
            person.Books = await booksQuery.ToListAsync();
            foreach (var book in person.Books)
            {
                await RetrieveBook(book);
            }
            person.Books.OrderBy(b => b.Information.CreationDate).ThenBy(b => b.Id);
        }

        public async Task RetrieveBook(Book book)
        {
            if (book == null)
            {
                return;
            }

            book.Information = await _context.Information.Where(x => x.Id == book.Id).FirstOrDefaultAsync();

            var chaptersQuery = from c in _context.Chapters
                                where c.BookId == book.Id
                                orderby c.CreationDate
                                select c;
            book.Chapters = await chaptersQuery.ToListAsync();
        }

        #endregion

        #region Create

        public async Task CreatePerson(Person person)
        {
            var myId = Guid.NewGuid().ToString("N");
            while (await _context.People.Where(x => x.Id == myId).AnyAsync())
            {
                myId = Guid.NewGuid().ToString("N");
            }
            person.Id = myId;
            _context.Add(person);
        }

        public async Task CreateBook(Book book, string personId)
        {
            var myId = Guid.NewGuid().ToString("N");
            while (await _context.Books.Where(x => x.Id == myId).AnyAsync())
            {
                myId = Guid.NewGuid().ToString("N");
            }
            book.Id = myId;
            book.PersonId = personId;
            _context.Add(book);
        }

        public async Task CreateChapter(Chapter chapter, string bookId)
        {
            var myId = Guid.NewGuid().ToString("N");
            while (await _context.Chapters.Where(x => x.Id == myId).AnyAsync())
            {
                myId = Guid.NewGuid().ToString("N");
            }
            chapter.Id = myId;
            chapter.BookId = bookId;
            _context.Add(chapter);
        }

        #endregion

        #region Delete

        public async Task DeletePerson(Person person)
        {
            if (person == null)
            {
                return;
            }
            await DeleteCredentails(person.Credentials);
            await DeletePet(person.Pet);
            foreach (var book in person.Books)
            {
                await DeleteBook(book);
            }
            _context.Remove(person);
        }

        public async Task DeleteCredentails(Credentials credentials)
        {
            if (credentials == null)
            {
                return;
            }
            _context.Remove(credentials);
        }

        public async Task DeletePet(Pet pet)
        {
            if (pet == null)
            {
                return;
            }
            _context.Remove(pet);
        }

        public async Task DeleteBook(Book book)
        {
            if (book == null)
            {
                return;
            }
            await DeleteInformation(book.Information);
            foreach (var chapter in book.Chapters)
            {
                await DeleteChapter(chapter);
            }
            _context.Remove(book);
        }

        public async Task DeleteInformation(Information information)
        {
            if (information == null)
            {
                return;
            }
            _context.Remove(information);
        }

        public async Task DeleteChapter(Chapter chapter)
        {
            if (chapter == null)
            {
                return;
            }
            _context.Remove(chapter);
        }

        #endregion
    }
}
