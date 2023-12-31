using KsiegarniaProject;
using KsiegarniaProject.Data;
using KsiegarniaProject.Interfaces;
using KsiegarniaProject.Models;
using KsiegarniaProject.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Moq;
using Moq.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit.Abstractions;

namespace Tests
{
    public class BookTests
    {

        [Fact]
        public void GetAllBooks()
        {
            // Arrange
            var bookContext = new Mock<IDataContext> ();
            bookContext.Setup(x => x.Books)
                .ReturnsDbSet(Seed.GetTestBooks());

            // Act
            BookRepository bookRepository = new(bookContext.Object);
            var books = bookRepository.GetBooks();
            // Assert
            Assert.NotNull(books);
            Assert.Equal(2, books.Count);
        }
    }
}