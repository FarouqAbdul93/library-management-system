using NUnit.Framework;
using LibraryManagementSystem;

namespace LibraryManagementSystem.Tests
{
    public class LibraryTests
    {
        [Test]
        public void AddBook_ThenSearchBookByTitle_ReturnsTheAddedBook()
        {
            var library = new Library();
            var book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling");

            library.AddBook(book);
            var result = library.SearchBook("Harry Potter and the Philosopher's Stone");

            Assert.That(result, Is.Not.Null);
            Assert.That(result!.Title, Is.EqualTo("Harry Potter and the Philosopher's Stone"));
        }

        [Test]
        public void SearchBook_WhenTitleDoesNotExist_ReturnsNull()
        {
            var library = new Library();

            var result = library.SearchBook("A Book That Does Not Exist");

            Assert.That(result, Is.Null);
        }

        [Test]
        public void ViewAvailableBooks_ReturnsOnlyBooksThatAreNotBorrowed()
        {
            var library = new Library();
            var book1 = new Book("Harry Potter and the Chamber of Secrets", "J.K. Rowling");
            var book2 = new Book("Harry Potter and the Prisoner of Azkaban", "J.K. Rowling");
            library.AddBook(book1);
            library.AddBook(book2);
            book2.BorrowBook(); // book2 is now unavailable

            var result = library.ViewAvailableBooks();

            Assert.That(result.Count, Is.EqualTo(1));
            Assert.That(result[0].Title, Is.EqualTo("Harry Potter and the Chamber of Secrets"));
        }

        [Test]
        public void BorrowBook_WhenTitleExistsAndAvailable_ReturnsTrue()
        {
            var library = new Library();
            var book = new Book("Harry Potter and the Goblet of Fire", "J.K. Rowling");
            library.AddBook(book);

            bool result = library.BorrowBook("Harry Potter and the Goblet of Fire");

            Assert.That(result, Is.True);
            Assert.That(book.IsBorrowed, Is.True);
        }

        [Test]
        public void BorrowBook_WhenTitleExistsButAlreadyBorrowed_ReturnsFalse()
        {
            var library = new Library();
            var book = new Book("Harry Potter and the Order of the Phoenix", "J.K. Rowling");
            library.AddBook(book);
            library.BorrowBook("Harry Potter and the Order of the Phoenix"); // first borrow

            bool result = library.BorrowBook("Harry Potter and the Order of the Phoenix"); // second attempt

            Assert.That(result, Is.False);
        }

        [Test]
        public void BorrowBook_WhenTitleDoesNotExist_ThrowsKeyNotFoundException()
        {
            var library = new Library();

            Assert.Throws<KeyNotFoundException>(() =>
                library.BorrowBook("A Book That Does Not Exist"));
        }

        [Test]
        public void ReturnBook_WhenTitleExistsAndBorrowed_ReturnsTrue()
        {
            var library = new Library();
            var book = new Book("Harry Potter and the Half-Blood Prince", "J.K. Rowling");
            library.AddBook(book);
            library.BorrowBook("Harry Potter and the Half-Blood Prince");

            bool result = library.ReturnBook("Harry Potter and the Half-Blood Prince");

            Assert.That(result, Is.True);
            Assert.That(book.IsBorrowed, Is.False);
        }

        [Test]
        public void ReturnBook_WhenTitleExistsButNotBorrowed_ReturnsFalse()
        {
            var library = new Library();
            var book = new Book("Harry Potter and the Deathly Hallows", "J.K. Rowling");
            library.AddBook(book);

            bool result = library.ReturnBook("Harry Potter and the Deathly Hallows");

            Assert.That(result, Is.False);
        }

        [Test]
        public void ReturnBook_WhenTitleDoesNotExist_ThrowsKeyNotFoundException()
        {
            var library = new Library();

            Assert.Throws<KeyNotFoundException>(() =>
                library.ReturnBook("A Book That Does Not Exist"));
        }
    }
}