using ComplexDataValidation.Data;
using ComplexDataValidation.Models;
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

        public bool BookFilled(Book book)
        {
            if (book.Information == null)
            {
                return false;
            }

            return true;
        }
    }
}
