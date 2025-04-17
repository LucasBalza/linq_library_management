using System;

namespace m1il_lucas_balza.Models
{
    public class Loan
    {
        public int Id { get; set; }
        public Book Book { get; set; }
        public Borrower Borrower { get; set; }
        public DateTime LoanDate { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? ReturnDate { get; set; }

        public bool IsOverdue => !ReturnDate.HasValue && DueDate < DateTime.Now;

        public Loan()
        {
            Book = null!;
            Borrower = null!;
        }

        public Loan(int id, Book book, Borrower borrower, DateTime loanDate, int durationDays)
        {
            Id = id;
            Book = book;
            Borrower = borrower;
            LoanDate = loanDate;
            DueDate = loanDate.AddDays(durationDays);
        }

        public override string ToString()
        {
            return $"{Book.Title} emprunté par {Borrower.Name} le {LoanDate:d}";
        }
    }
} 