using System;

namespace m1il_lucas_balza.Models
{
    public class Borrower
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }

        public Borrower()
        {
            Name = string.Empty;
            Email = string.Empty;
            Phone = string.Empty;
        }

        public Borrower(int id, string name, string email, string phone)
        {
            Id = id;
            Name = name;
            Email = email;
            Phone = phone;
        }

        public override string ToString()
        {
            return $"{Name} ({Email})";
        }
    }
} 