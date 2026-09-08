using System.Collections.Generic;
using System.Linq;

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
            return books.FirstOrDefault(b => b.Title == title);
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
    }
}
