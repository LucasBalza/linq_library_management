using System;
using System.Collections.Generic;
using System.Linq;

namespace m1il_lucas_balza.Models
{
    /// <summary>
    /// Classe représentant une bibliothèque avec des livres, des auteurs, des emprunteurs et des emprunts.
    /// </summary>
    public class Library
    {
        private List<Author> _authors;
        private List<Book> _books;
        private List<Borrower> _borrowers;
        private List<Loan> _loans;

        /// <summary>
        /// Initialise une nouvelle instance de la classe Library.
        /// </summary>
        public Library()
        {
            _authors = new List<Author>();
            _books = new List<Book>();
            _borrowers = new List<Borrower>();
            _loans = new List<Loan>();
        }


        // Méthodes d'ajout
        /// <summary>
        /// Ajoute un auteur à la bibliothèque.
        /// </summary>
        /// <param name="author">L'auteur à ajouter.</param>
        public void AddAuthor(Author author)
        {
            var query = from a in _authors
                       where a.Id == author.Id
                       select a;
            if (!query.Any())
            {
                _authors.Add(author);
            }
        }

        /// <summary>
        /// Ajoute un livre à la bibliothèque.
        /// </summary>
        /// <param name="book">Le livre à ajouter.</param>
        public void AddBook(Book book)
        {
            var query = from b in _books
                       where b.Id == book.Id
                       select b;
            if (!query.Any())
            {
                _books.Add(book);
            }
        }

        /// <summary>
        /// Ajoute un emprunteur à la bibliothèque.
        /// </summary>
        /// <param name="borrower">L'emprunteur à ajouter.</param>
        public void AddBorrower(Borrower borrower)
        {
            var query = from b in _borrowers
                       where b.Id == borrower.Id
                       select b;
            if (!query.Any())
            {
                _borrowers.Add(borrower);
            }
        }

        /// <summary>
        /// Ajoute un emprunt à la bibliothèque.
        /// </summary>
        /// <param name="loan">L'emprunt à ajouter.</param>
        public void AddLoan(Loan loan)
        {
            var query = from l in _loans
                       where l.Id == loan.Id
                       select l;
            if (!query.Any())
            {
                _loans.Add(loan);
            }
        }

        /// <summary>
        /// Récupère tous les livres de la bibliothèque.
        /// </summary>
        /// <returns>Une collection de tous les livres.</returns>
        public IEnumerable<Book> GetAllBooks()
        {
            var query = from book in _books
                       select book;
            return query;
        }

        /// <summary>
        /// Récupère tous les auteurs de la bibliothèque.
        /// </summary>
        /// <returns>Une collection de tous les auteurs.</returns>
        public IEnumerable<Author> GetAllAuthors()
        {
            var query = from author in _authors
                       select author;
            return query;
        }

        /// <summary>
        /// Récupère tous les emprunteurs de la bibliothèque.
        /// </summary>
        /// <returns>Une collection de tous les emprunteurs.</returns>
        public IEnumerable<Borrower> GetAllBorrowers()
        {
            var query = from borrower in _borrowers
                       select borrower;
            return query;
        }

        /// <summary>
        /// Récupère tous les emprunts de la bibliothèque.
        /// </summary>
        /// <returns>Une collection de tous les emprunts.</returns>
        public IEnumerable<Loan> GetAllLoans()
        {
            var query = from loan in _loans
                       select loan;
            return query;
        }

        /// <summary>
        /// Récupère tous les livres disponibles (non empruntés) de la bibliothèque.
        /// </summary>
        /// <returns>Une collection de livres disponibles.</returns>
        public IEnumerable<Book> GetAvailableBooks()
        {
            var query = from book in _books
                       where !_loans.Any(loan => loan.Book.Id == book.Id)
                       select book;
            return query;
        }

        /// <summary>
        /// Récupère tous les livres en retard de retour.
        /// </summary>
        /// <returns>Une collection de livres en retard.</returns>
        public IEnumerable<Book> GetOverdueBooks()
        {
            var query = from loan in _loans
                       where loan.DueDate < DateTime.Now
                       select loan.Book;
            return query;
        }

        /// <summary>
        /// Recherche des livres par terme de recherche.
        /// </summary>
        /// <param name="searchTerm">Le terme à rechercher dans les titres, auteurs et catégories.</param>
        /// <returns>Une collection de livres correspondant au terme de recherche.</returns>
        public IEnumerable<Book> SearchBooks(string searchTerm)
        {
            var query = from book in _books
                       where book.Title.ToLower().Contains(searchTerm.ToLower()) ||
                             book.Author.Name.ToLower().Contains(searchTerm.ToLower()) ||
                             book.Category.ToLower().Contains(searchTerm.ToLower())
                       select book;
            return query;
        }

        /// <summary>
        /// Récupère tous les livres actuellement empruntés.
        /// </summary>
        /// <returns>Une collection de livres empruntés.</returns>
        public IEnumerable<Book> GetBorrowedBooks()
        {
            var query = from book in _books
                       where _loans.Any(loan => loan.Book.Id == book.Id)
                       select book;
            return query;
        }

        /// <summary>
        /// Récupère les emprunts d'un emprunteur spécifique.
        /// </summary>
        /// <param name="borrower">L'emprunteur à rechercher.</param>
        /// <returns>Une collection d'emprunts de l'emprunteur spécifié.</returns>
        public IEnumerable<Loan> GetLoansByBorrower(Borrower borrower)
        {
            var query = from loan in _loans
                       where loan.Borrower.Id == borrower.Id
                       select loan;
            return query;
        }

    }
} 