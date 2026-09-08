using NUnit.Framework;
using LibraryManagementSystem;

namespace LibraryManagementSystem.Tests
{
    public class BookTests
    {
        [Test]
        public void Constructor_SetsTitleAndAuthor_AndBookIsNotBorrowed()
        {
            // Arrange & Act
            var book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling");

            // Assert
            Assert.That(book.Title, Is.EqualTo("Harry Potter and the Philosopher's Stone"));
            Assert.That(book.Author, Is.EqualTo("J.K. Rowling"));
            Assert.That(book.IsBorrowed, Is.False);
        }

        [Test]
        public void BorrowBook_WhenAvailable_ReturnsTrueAndMarksAsBorrowed()
        {
            var book = new Book("Harry Potter and the Chamber of Secrets", "J.K. Rowling");

            bool result = book.BorrowBook();

            Assert.That(result, Is.True);
            Assert.That(book.IsBorrowed, Is.True);
        }

        [Test]
        public void BorrowBook_WhenAlreadyBorrowed_ReturnsFalse()
        {
            var book = new Book("Harry Potter and the Prisoner of Azkaban", "J.K. Rowling");
            book.BorrowBook(); // first borrow succeeds

            bool result = book.BorrowBook(); // second attempt should fail

            Assert.That(result, Is.False);
            Assert.That(book.IsBorrowed, Is.True); // still borrowed
        }

        [Test]
        public void ReturnBook_WhenBorrowed_ReturnsTrueAndMarksAsAvailable()
        {
            var book = new Book("Harry Potter and the Goblet of Fire", "J.K. Rowling");
            book.BorrowBook();

            bool result = book.ReturnBook();

            Assert.That(result, Is.True);
            Assert.That(book.IsBorrowed, Is.False);
        }

        [Test]
        public void ReturnBook_WhenNotBorrowed_ReturnsFalse()
        {
            var book = new Book("Harry Potter and the Order of the Phoenix", "J.K. Rowling");

            bool result = book.ReturnBook();

            Assert.That(result, Is.False);
            Assert.That(book.IsBorrowed, Is.False); 
        }
    }
}