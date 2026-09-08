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
    }
}