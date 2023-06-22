﻿using KsiegarniaProject.Data;
using KsiegarniaProject.DTO;
using KsiegarniaProject.Interfaces;
using KsiegarniaProject.Models;
using Microsoft.EntityFrameworkCore;

namespace KsiegarniaProject.Repositories
{
	public class BookRepository : IBookRepository
	{
        //private int lastID;
		private readonly DataContext _context;
		public BookRepository(DataContext context)
		{
			_context = context;
		}
		public bool BookExists(int id)
		{

			return _context.Books.Any(x => x.Id == id);
		}

        public bool Delete(int id)
        {
            Book temp = _context.Books.Where(b => b.Id == id).FirstOrDefault();
            _context.Books.Remove(temp);
            return Save();
        }

        public BookDTO GetBook(int id)
		{
            return _context.Books.Where(b => b.Id==id).Select(b => new BookDTO
            {
                Id = b.Id,
                Author = b.Author,
                Price = b.Price,
                Quantity = b.Quantity,
                Title = b.Title,
                Categories = _context.BookCategories.Join(_context.Categories, a => a.CategoryId, b => b.Id, (a, b) => b).ToList()
            }).FirstOrDefault();
        }

		public ICollection<BookDTO> GetBooks()
		{
            var books = _context.Books
                .Include(bc => bc.BookCategories)
                .ThenInclude(c => c.Category)
                .Select(b => new BookDTO
                {
                    Id = b.Id,
                    Author = b.Author,
                    Price = b.Price,
                    Quantity = b.Quantity,
                    Title = b.Title,
                    Categories = b.BookCategories.Select(c => c.Category).ToList()
                })
                .ToList();

            return books;
        }

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
        /*private int GetNextId()
        {

            if (_context.Books.Count() != 0)
            {
                lastID = _context.Books.LastOrDefault().Id;
            }

            int newID = lastID + 1;
            return newID;
        }*/
        public void Create(Book p)
        {
            _context.Books.Add(p);
        }
    }
}
