namespace m1il_lucas_balza.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
        public DateTime PublicationDate { get; set; }
        public string ISBN { get; set; }
        public string Category { get; set; }
        public int PageCount { get; set; }

        public Book()
        {
            Title = string.Empty;
            Author = null!;
            ISBN = string.Empty;
            Category = string.Empty;
        }

        public Book(int id, string title, Author author, DateTime publicationDate, string isbn, string category, int pageCount)
        {
            Id = id;
            Title = title;
            Author = author;
            PublicationDate = publicationDate;
            ISBN = isbn;
            Category = category;
            PageCount = pageCount;
        }

        public override string ToString()
        {
            return $"{Title} par {Author.Name} ({Category})";
        }
    }
} 