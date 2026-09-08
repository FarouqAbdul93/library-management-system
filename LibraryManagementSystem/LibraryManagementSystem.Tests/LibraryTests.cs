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

        [Test]
        public void SaveToFile_ThenLoadFromFile_RestoresBooksAndBorrowedStatus()
        {
            string tempFilePath = Path.GetTempFileName();

            try
            {
                var library = new Library();
                var book1 = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling");
                var book2 = new Book("Harry Potter and the Chamber of Secrets", "J.K. Rowling");
                library.AddBook(book1);
                library.AddBook(book2);
                library.BorrowBook("Harry Potter and the Chamber of Secrets");

                library.SaveToFile(tempFilePath);

                var loadedLibrary = new Library();
                loadedLibrary.LoadFromFile(tempFilePath);

                var restoredBook1 = loadedLibrary.SearchBook("Harry Potter and the Philosopher's Stone");
                var restoredBook2 = loadedLibrary.SearchBook("Harry Potter and the Chamber of Secrets");

                Assert.That(restoredBook1, Is.Not.Null);
                Assert.That(restoredBook1!.IsBorrowed, Is.False);

                Assert.That(restoredBook2, Is.Not.Null);
                Assert.That(restoredBook2!.IsBorrowed, Is.True);
            }
            finally
            {
                if (File.Exists(tempFilePath))
                {
                    File.Delete(tempFilePath);
                }
            }
        }

        [Test]
        public void SearchBooks_WhenSearchTermMatchesTitle_ReturnsThatBook()
        {
            var library = new Library();
            var book = new Book("Harry Potter and the Philosopher's Stone", "J.K. Rowling");
            library.AddBook(book);

            var results = library.SearchBooks("Harry Potter and the Philosopher's Stone");

            Assert.That(results.Count, Is.EqualTo(1));
            Assert.That(results[0].Title, Is.EqualTo("Harry Potter and the Philosopher's Stone"));
        }

        [Test]
        public void SearchBooks_WhenSearchTermMatchesAuthorWithMultipleBooks_ReturnsAllTheirBooks()
        {
            var library = new Library();
            library.AddBook(new Book("Harry Potter and the Chamber of Secrets", "J.K. Rowling"));
            library.AddBook(new Book("Harry Potter and the Prisoner of Azkaban", "J.K. Rowling"));

            var results = library.SearchBooks("J.K. Rowling");

            Assert.That(results.Count, Is.EqualTo(2));
        }

        [Test]
        public void SearchBooks_WhenNoMatch_ReturnsEmptyList()
        {
            var library = new Library();

            var results = library.SearchBooks("Nonexistent");

            Assert.That(results.Count, Is.EqualTo(0));
        }

        [Test]
        public void SearchBook_IsCaseInsensitive()
        {
            var library = new Library();
            var book = new Book("Harry Potter and the Deathly Hallows", "J.K. Rowling");
            library.AddBook(book);

            var result = library.SearchBook("harry potter and the deathly hallows");

            Assert.That(result, Is.Not.Null);
        }

        [Test]
        public void SearchBooks_IsCaseInsensitiveForTitleAndAuthor()
        {
            var library = new Library();
            library.AddBook(new Book("Harry Potter and the Half-Blood Prince", "J.K. Rowling"));

            var byTitle = library.SearchBooks("harry potter and the half-blood prince");
            var byAuthor = library.SearchBooks("j.k. rowling");

            Assert.That(byTitle.Count, Is.EqualTo(1));
            Assert.That(byAuthor.Count, Is.EqualTo(1));
        }

        [Test]
        public void BorrowBook_IsCaseInsensitive()
        {
            var library = new Library();
            var book = new Book("Harry Potter and the Order of the Phoenix", "J.K. Rowling");
            library.AddBook(book);

            bool result = library.BorrowBook("harry potter and the order of the phoenix");

            Assert.That(result, Is.True);
            Assert.That(book.IsBorrowed, Is.True);
        }

        [Test]
        public void GetBooksSortedByTitle_ReturnsBooksInTitleAlphabeticalOrder()
        {
            var library = new Library();
            library.AddBook(new Book("Harry Potter and the Prisoner of Azkaban", "J.K. Rowling"));
            library.AddBook(new Book("Harry Potter and the Chamber of Secrets", "J.K. Rowling"));

            var results = library.GetBooksSortedByTitle();

            Assert.That(results[0].Title, Is.EqualTo("Harry Potter and the Chamber of Secrets"));
            Assert.That(results[1].Title, Is.EqualTo("Harry Potter and the Prisoner of Azkaban"));
        }

        [Test]
        public void GetBooksSortedByAuthor_ReturnsBooksInAuthorAlphabeticalOrder()
        {
            var library = new Library();
            library.AddBook(new Book("The Hobbit", "J.R.R. Tolkien"));
            library.AddBook(new Book("Harry Potter and the Goblet of Fire", "J.K. Rowling"));

            var results = library.GetBooksSortedByAuthor();

            Assert.That(results[0].Author, Is.EqualTo("J.K. Rowling"));
            Assert.That(results[1].Author, Is.EqualTo("J.R.R. Tolkien"));
        }
    }
}