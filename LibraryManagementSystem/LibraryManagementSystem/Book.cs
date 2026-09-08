namespace LibraryManagementSystem
{
    public class Book
    {
        public string Title { get; }
        public string Author { get; }
        public bool IsBorrowed { get; private set; }

        public Book(string title, string author)
        {
            Title = title;
            Author = author;
            IsBorrowed = false;
        }

        public bool BorrowBook()
        {
            if (IsBorrowed)
            {
                return false;
            }

            IsBorrowed = true;
            return true;
        }

        public bool ReturnBook()
        {
            if (!IsBorrowed)
            {
                return false;
            }

            IsBorrowed = false;
            return true;
        }

        public override string ToString()
        {
            string status;
            if (IsBorrowed)
            {
                status = "Borrowed";
            }
            else
            {
                status = "Available";
            }
            return $"{Title} by {Author} ({status})";
        }
    }
}