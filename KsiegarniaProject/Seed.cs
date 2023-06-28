﻿using KsiegarniaProject.Data;
using KsiegarniaProject.Interfaces;
using KsiegarniaProject.Models;
using Microsoft.AspNetCore.Identity;

namespace KsiegarniaProject
{
    public class Seed
    {
        private readonly IDataContext _context;
        public Seed(IDataContext context)
        {
            _context = context;
        }

        public void SeedDataContext()
        {
            if(!_context.Books.Any())
            {
                var books = new List<Book>()
                {
                    new Book()
                    {
                       
                        Title = "Lord of the Rings",
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory
                            {
                                Category = new Category() {Name = "Fantasy" },
                            },
                            new BookCategory
                            {
                                Category = new Category() {Name = "Przygoda" },
                            },
                            new BookCategory
                            {
                                Category = new Category() {Name = "Powieść" },
                            }
                        },
                        Author = new Author {
                            FirstName = "J.R.R",
                            LastName = "Tolkien"
                        },
                        Quantity = 100,
                        Price = 100.99M
                    },
                    new Book()
                    {
                        Title = "Zbrodnia i kara",
                        BookCategories = new List<BookCategory>()
                        {
                            new BookCategory
                            { 
                                Category = new Category() {Name = "Proza psychologiczna" }
                            },
                            new BookCategory
                            {
                                Category = new Category() {Name = "Powieść kryminalna" }
                            }
                        },
                        Author = new Author {
                            FirstName = "Fiodor",
                            LastName = "Dostojewski"
                        },
                        Quantity = 70,
                        Price = 50M
                    }
                };
                _context.Books.AddRange(books);
                _context.SaveChanges();
            }

            if (!_context.AppUsers.Any())
            {
                string ADMIN_ID = "02174cf0–9412–4cfe-afbf-59f706d72cf6";
                string ADMIN_ROLE_ID = "341743f0-asd2–42de-afbf-59kmkkmk72cf6";
                string MANAGER_ROLE_ID = "4f593732-1151–435c-99df-1c6a05b52071";
                string WORKER_ROLE_ID = "9faaa0de-bcc1–437e-9db9-30d6dbe8de2f";

                var users = new List<AppUser>()
                {
                    new AppUser()
                    {
                        Id = ADMIN_ID,
                        FirstName = "Pan",
                        LastName = "Admin",
                        UserName = "Administrator",
                        NormalizedUserName = "ADMINISTRATOR",
                        Email = "admin@test.com",
                        NormalizedEmail = "ADMIN@TEST.COM",
                        EmailConfirmed = true
                    }
                };

                var roles = new List<IdentityRole>()
                {
                    new IdentityRole()
                    {
                        Name = "Administrator",
                        NormalizedName = "ADMINISTRATOR",
                        Id = ADMIN_ROLE_ID,
                        ConcurrencyStamp = ADMIN_ROLE_ID
                    },
                    new IdentityRole()
                    {
                        Name = "Kierownik",
                        NormalizedName = "KIEROWNIK",
                        Id = MANAGER_ROLE_ID,
                        ConcurrencyStamp = MANAGER_ROLE_ID
                    },
                    new IdentityRole()
                    {
                        Name = "Pracownik",
                        NormalizedName = "PRACOWNIK",
                        Id = WORKER_ROLE_ID,
                        ConcurrencyStamp = WORKER_ROLE_ID
                    }
                };

                PasswordHasher<AppUser> hasher = new PasswordHasher<AppUser>();
                users[0].PasswordHash = hasher.HashPassword(users[0], "supersecret");

                var userRole = new IdentityUserRole<string>()
                {
                    RoleId = ADMIN_ROLE_ID,
                    UserId = ADMIN_ID
                };

                _context.AppUsers.AddRange(users);
                _context.AppRoles.AddRange(roles);
                _context.AppUserRoles.Add(userRole);
                _context.SaveChanges();
            }
        }

        public static List<Book> GetTestBooks()
        {
            var books = new List<Book>()
            {
                new Book()
                {

                    Title = "Książka 1",
                    BookCategories = new List<BookCategory>()
                    {
                        new BookCategory
                        {
                            Category = new Category() {Name = "Kategoira 1-1" },
                        },
                        new BookCategory
                        {
                            Category = new Category() {Name = "Kategoria 1-2" },
                        },
                        new BookCategory
                        {
                            Category = new Category() {Name = "Kategoria 1-3" },
                        }
                    },
                    Author = new Author {
                        FirstName = "Imię 1",
                        LastName = "Nazwisko 1"
                    },
                    Quantity = 1000,
                    Price = 111.11M
                },
                new Book()
                {
                    Title = "Książka 2",
                    BookCategories = new List<BookCategory>()
                    {
                        new BookCategory
                        {
                            Category = new Category() {Name = "Kategoria 2-1" }
                        },
                        new BookCategory
                        {
                            Category = new Category() {Name = "Kategoria 2-2" }
                        },
                        new BookCategory
                        {
                            Category = new Category() {Name = "Kategoria 2-3" }
                        }
                    },
                    Author = new Author {
                        FirstName = "Imię 2",
                        LastName = "Nazwisko 2"
                    },
                    Quantity = 2000,
                    Price = 222.22M
                }
            };
            return books;
        }
    }
}