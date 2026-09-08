using System;

namespace LibraryManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Library library = new Library();
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
                Console.WriteLine("6. Exit");
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
                        running = false;
                        Console.WriteLine("Goodbye!");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Please choose 1-6.");
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

        static void SearchBook(Library library)
        {
            Console.Write("Enter title to search for: ");
            string? title = Console.ReadLine();

            var book = library.SearchBook(title ?? "");

            if (book == null)
            {
                Console.WriteLine("No book found with that title.");
            }
            else
            {
                Console.WriteLine($"Found: {book}");
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