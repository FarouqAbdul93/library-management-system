using System;
using System.Collections.Generic;
using System.IO;

namespace LibraryManagementSystem
{
    internal class Program
    {
        private const string SaveFilePath = "library.json";
        static void Main(string[] args)
        {
            Library library = new Library();
            if (File.Exists(SaveFilePath))
            {
                library.LoadFromFile(SaveFilePath);
                Console.WriteLine("Loaded existing library data.");
            }
            bool running = true;

            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== Library Management System ===");
                Console.WriteLine("1. Add a new book");
                Console.WriteLine("2. View available books");
                Console.WriteLine("3. Search for a book");
                Console.WriteLine("4. Borrow a book");
                Console.WriteLine("5. Return a book");
                Console.WriteLine("6. View books sorted");
                Console.WriteLine("7. Exit");
                Console.Write("Choose an option: ");

                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook(library);
                        break;

                    case "2":
                        ViewAvailableBooks(library);
                        break;

                    case "3":
                        SearchBook(library);
                        break;

                    case "4":
                        BorrowBook(library);
                        break;

                    case "5":
                        ReturnBook(library);
                        break;

                    case "6":
                        ViewBooksSorted(library);
                        break;

                    case "7":
                        library.SaveToFile(SaveFilePath);
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please choose 1-7.");
                        break;
                }
            }
        }

        static void AddBook(Library library)
        {
            Console.Write("Enter book title: ");
            string? title = Console.ReadLine();

            Console.Write("Enter book author: ");
            string? author = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(title) || string.IsNullOrWhiteSpace(author))
            {
                Console.WriteLine("Title and author cannot be empty.");
                return;
            }

            Book book = new Book(title, author);
            library.AddBook(book);
            Console.WriteLine($"Added: {book}");
        }

        static void ViewAvailableBooks(Library library)
        {
            var availableBooks = library.ViewAvailableBooks();

            if (availableBooks.Count == 0)
            {
                Console.WriteLine("No books are currently available.");
                return;
            }

            Console.WriteLine("Available books:");
            foreach (var book in availableBooks)
            {
                Console.WriteLine($"- {book}");
            }
        }

        static void ViewBooksSorted(Library library)
        {
            Console.Write("Sort by (title/author): ");
            string? sortChoice = Console.ReadLine();

            List<Book> sortedBooks;

            if (string.Equals(sortChoice, "author", StringComparison.OrdinalIgnoreCase))
            {
                sortedBooks = library.GetBooksSortedByAuthor();
            }
            else
            {
                sortedBooks = library.GetBooksSortedByTitle();
            }

            if (sortedBooks.Count == 0)
            {
                Console.WriteLine("No books in the library yet.");
                return;
            }

            foreach (var book in sortedBooks)
            {
                Console.WriteLine($"- {book}");
            }
        }

        static void SearchBook(Library library)
        {
            Console.Write("Enter title or author to search for: ");
            string? searchTerm = Console.ReadLine();

            var results = library.SearchBooks(searchTerm ?? "");

            if (results.Count == 0)
            {
                Console.WriteLine("No books found matching that search.");
            }
            else
            {
                Console.WriteLine("Found:");
                foreach (var book in results)
                {
                    Console.WriteLine($"- {book}");
                }
            }
        }

        static void BorrowBook(Library library)
        {
            Console.Write("Enter title to borrow: ");
            string? title = Console.ReadLine();

            try
            {
                bool success = library.BorrowBook(title ?? "");
                Console.WriteLine(success
                    ? "Book borrowed successfully."
                    : "That book is already borrowed.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static void ReturnBook(Library library)
        {
            Console.Write("Enter title to return: ");
            string? title = Console.ReadLine();

            try
            {
                bool success = library.ReturnBook(title ?? "");
                Console.WriteLine(success
                    ? "Book returned successfully."
                    : "That book wasn't borrowed.");
            }
            catch (KeyNotFoundException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}