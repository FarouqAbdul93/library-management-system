using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Text.Json;

namespace LibraryManagementSystem
{
    public class Library
    {
        private List<Book> books = new List<Book>();

        public void AddBook(Book book)
        {
            books.Add(book);
        }

        public Book? SearchBook(string title)
        {
            return books.FirstOrDefault(b => string.Equals(b.Title, title, StringComparison.OrdinalIgnoreCase));
        }

        public List<Book> ViewAvailableBooks()
        {
            return books.Where(b => !b.IsBorrowed).ToList();
        }

        public bool BorrowBook(string title)
        {
            var book = SearchBook(title);
            if (book == null)
            {
                throw new KeyNotFoundException($"No book found with title '{title}'.");
            }

            return book.BorrowBook();
        }

        public bool ReturnBook(string title)
        {
            var book = SearchBook(title);
            if (book == null)
            {
                throw new KeyNotFoundException($"No book found with title '{title}'.");
            }

            return book.ReturnBook();
        }

        public List<Book> SearchBooks(string searchTerm)
        {
            return books
                .Where(b => string.Equals(b.Title, searchTerm, StringComparison.OrdinalIgnoreCase)
         || string.Equals(b.Author, searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public void SaveToFile(string filePath)
        {
            string json = JsonSerializer.Serialize(books);
            File.WriteAllText(filePath, json);
        }

        public void LoadFromFile(string filePath)
        {
            string json = File.ReadAllText(filePath);
            var loadedBooks = JsonSerializer.Deserialize<List<Book>>(json);

            books = loadedBooks ?? new List<Book>();
        }
    }
}
