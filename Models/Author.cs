namespace m1il_lucas_balza.Models
{
    public class Author
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Nationality { get; set; }
        public DateTime BirthDate { get; set; }

        public Author()
        {
            Name = string.Empty;
            Nationality = string.Empty;
        }

        public Author(int id, string name, string nationality, DateTime birthDate)
        {
            Id = id;
            Name = name;
            Nationality = nationality;
            BirthDate = birthDate;
        }

        public override string ToString()
        {
            return $"{Name} ({Nationality})";
        }
    }
} 