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
    }
}