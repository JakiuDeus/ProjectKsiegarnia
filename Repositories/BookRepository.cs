﻿using KsiegarniaProject.Data;
using KsiegarniaProject.DTO;
using KsiegarniaProject.Interfaces;
using KsiegarniaProject.Models;

namespace KsiegarniaProject.Repositories
{
	public class BookRepository : IBookRepository
	{
		private readonly DataContext _context;
		public BookRepository(DataContext context)
		{
			_context = context;
		}
		public bool BookExists(int id)
		{

			return _context.Books.Any(x => x.Id == id);
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
			return _context.Books.Select(b => new BookDTO
			{
				Id = b.Id,
				Author = b.Author,
				Price = b.Price,
				Quantity = b.Quantity,
				Title = b.Title,
				Categories = _context.BookCategories.Join(_context.Categories, a => a.CategoryId, b => b.Id, (a,b) => b).ToList()
			}).ToList();
		}

		public bool Save()
		{
			var saved = _context.SaveChanges();
			return saved > 0 ? true : false;
		}
	}
}
