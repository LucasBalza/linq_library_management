using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Linq;
using System.Text.Json;
using System.IO;
using m1il_lucas_balza.Models;

namespace m1il_lucas_balza
{
    class Program
    {
        private static Library _library = null!;

        /// <summary>
        /// Point d'entrée principal de l'application.
        /// </summary>
        /// <param name="args">Arguments de la ligne de commande.</param>
        static void Main(string[] args)
        {
            Initialize();
            Run();
        }

        /// <summary>
        /// Initialise l'application en chargeant les données depuis le fichier XML.
        /// </summary>
        private static void Initialize()
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Initialisation du programme...");
            _library = new Library();

            // Charger les données depuis le fichier XML
            try
            {
                Console.WriteLine("Tentative de chargement des données XML...");
                LoadFromXml();
                Console.WriteLine("Données chargées avec succès depuis le fichier XML.");
            }
            catch (Exception ex)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"Erreur lors du chargement des données : {ex.Message}");
                Console.WriteLine($"Détails de l'erreur : {ex}");
            }
            Console.ResetColor();
            Pause();
        }

        /// <summary>
        /// Démarre l'exécution de l'application en affichant le menu principal.
        /// </summary>
        private static void Run()
        {
            ShowMainMenu();
        }

        /// <summary>
        /// Affiche le menu principal et gère les choix de l'utilisateur.
        /// </summary>
        private static void ShowMainMenu()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("\n=== Menu Principal ===");
                Console.WriteLine("1. Afficher tous les livres");
                Console.WriteLine("2. Rechercher un livre");
                Console.WriteLine("3. Afficher les livres disponibles");
                Console.WriteLine("4. Afficher les livres empruntés");
                Console.WriteLine("5. Afficher les livres en retard");
                Console.WriteLine("6. Afficher les livres par auteur");
                Console.WriteLine("7. Afficher les livres par catégorie");
                Console.WriteLine("8. Afficher les emprunteurs");
                Console.WriteLine("Q. Quitter");
                Console.Write("\nVotre choix : ");

                string? input = Console.ReadLine();
                string choice = input?.ToUpper() ?? string.Empty;

                switch (choice)
                {
                    case "1":
                        Console.Clear();
                        DisplayAllBooks();
                        break;
                    case "2":
                        Console.Clear();
                        SearchBooks();
                        break;
                    case "3":
                        Console.Clear();
                        DisplayAvailableBooks();
                        break;
                    case "4":
                        Console.Clear();
                        DisplayBorrowedBooks();
                        break;
                    case "5":
                        Console.Clear();
                        DisplayOverdueBooks();
                        break;
                    case "6":
                        Console.Clear();
                        DisplayBooksByAuthor();
                        break;
                    case "7":
                        Console.Clear();
                        DisplayBooksByCategory();
                        break;
                    case "8":
                        Console.Clear();
                        DisplayBorrowers();
                        break;
                    case "Q":
                        Console.Clear();
                        Console.WriteLine("Au revoir !");
                        return;
                    default:
                        Console.WriteLine("Choix invalide. Veuillez réessayer.");
                        break;
                }
            }
        }

        /// <summary>
        /// Attend que l'utilisateur appuie sur une touche pour revenir au menu.
        /// </summary>
        private static void Pause()
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("\nAppuyez sur une touche pour vous rendre au menu...");
            Console.ResetColor();
            Console.ReadKey();
            Console.Clear();
        }

        /// <summary>
        /// Affiche le menu d'export pour un ensemble d'éléments.
        /// </summary>
        /// <typeparam name="T">Type des éléments à exporter.</typeparam>
        /// <param name="items">Collection d'éléments à exporter.</param>
        /// <param name="title">Titre de l'export.</param>
        private static void ShowExportMenu<T>(IEnumerable<T> items, string title)
        {
            Console.WriteLine($"\n=== {title} ===");
            Console.WriteLine("1. Exporter en XML");
            Console.WriteLine("2. Exporter en JSON");
            Console.WriteLine("3. Revenir au menu principal");
            Console.Write("\nVotre choix : ");

            string? input = Console.ReadLine();
            string choice = input?.ToUpper() ?? string.Empty;

            switch (choice)
            {
                case "1":
                    ExportToXml(items, title);
                    break;
                case "2":
                    ExportToJson(items, title);
                    break;
                case "3":
                    return;
                default:
                    Console.WriteLine("Choix invalide.");
                    break;
            }
        }

        /// <summary>
        /// Affiche tous les livres de la bibliothèque.
        /// </summary>
        private static void DisplayAllBooks()
        {
            Console.WriteLine("\n=== Liste des livres ===");
            var books = _library.GetAllBooks();

            if (!books.Any())
            {
                Console.WriteLine("Aucun livre trouvé dans la bibliothèque.");
                Pause();
                return;
            }

            foreach (var book in books)
            {
                Console.WriteLine($"\nID: {book.Id}");
                Console.WriteLine($"Titre: {book.Title}");
                Console.WriteLine($"Auteur: {book.Author.Name}");
                Console.WriteLine($"Catégorie: {book.Category}");
                Console.WriteLine($"ISBN: {book.ISBN}");
                Console.WriteLine($"Date de publication: {book.PublicationDate:d}");
                Console.WriteLine($"Nombre de pages: {book.PageCount}");
                Console.WriteLine("-------------------");
            }

            ShowExportMenu(books, "Export des Livres");
        }

        /// <summary>
        /// Recherche des livres selon un mot de recherche.
        /// </summary>
        private static void SearchBooks()
        {
            Console.Write("\nEntrez un terme de recherche : ");
            string searchTerm = Console.ReadLine()?.ToLower() ?? string.Empty;

            var results = _library.SearchBooks(searchTerm);

            Console.WriteLine("\n=== Résultats de la recherche ===");
            if (!results.Any())
            {
                Console.WriteLine("Aucun résultat trouvé.");
                Pause();
                return;
            }

            foreach (var book in results)
            {
                Console.WriteLine($"- {book.Title} par {book.Author.Name} ({book.Category})");
            }

            ShowExportMenu(results, "Export des résultats de recherche");
        }

        /// <summary>
        /// Affiche les livres disponibles dans la bibliothèque.
        /// </summary>
        private static void DisplayAvailableBooks()
        {
            Console.WriteLine("\n=== Livres disponibles ===");
            var availableBooks = _library.GetAvailableBooks();

            if (!availableBooks.Any())
            {
                Console.WriteLine("Aucun livre disponible.");
                Pause();
                return;
            }

            foreach (var book in availableBooks)
            {
                Console.WriteLine($"- {book.Title} par {book.Author.Name}");
            }

            ShowExportMenu(availableBooks, "Export des livres disponibles");
        }

        /// <summary>
        /// Affiche les livres actuellement empruntés.
        /// </summary>
        private static void DisplayBorrowedBooks()
        {
            Console.WriteLine("\n=== Livres empruntés ===");
            var borrowedBooks = _library.GetBorrowedBooks();

            if (!borrowedBooks.Any())
            {
                Console.WriteLine("Aucun livre emprunté.");
                Pause();
                return;
            }

            foreach (var book in borrowedBooks)
            {
                Console.WriteLine($"- {book.Title} par {book.Author.Name}");
            }

            ShowExportMenu(borrowedBooks, "Export des livres empruntés");
        }

        /// <summary>
        /// Affiche les livres en retard de retour.
        /// </summary>
        private static void DisplayOverdueBooks()
        {
            Console.WriteLine("\n=== Livres en retard ===");
            var overdueBooks = _library.GetOverdueBooks();

            if (!overdueBooks.Any())
            {
                Console.WriteLine("Aucun livre en retard.");
                Pause();
                return;
            }

            foreach (var book in overdueBooks)
            {
                Console.WriteLine($"- {book.Title} par {book.Author.Name}");
            }

            ShowExportMenu(overdueBooks, "Export des livres en retard");
        }

        /// <summary>
        /// Affiche les livres groupés par auteur.
        /// </summary>
        private static void DisplayBooksByAuthor()
        {
            Console.WriteLine("\n=== Livres par auteur ===");
            var booksByAuthor = _library.GetAllBooks()
                .GroupBy(b => b.Author.Name)
                .OrderBy(g => g.Key);

            if (!booksByAuthor.Any())
            {
                Console.WriteLine("Aucun livre trouvé.");
                Pause();
                return;
            }

            foreach (var group in booksByAuthor)
            {
                Console.WriteLine($"\n{group.Key}:");
                foreach (var book in group)
                {
                    Console.WriteLine($"  - {book.Title} ({book.Category})");
                }
            }

            ShowExportMenu(booksByAuthor.SelectMany(g => g), "Export des livres par auteur");
        }

        /// <summary>
        /// Affiche les livres groupés par catégorie.
        /// </summary>
        private static void DisplayBooksByCategory()
        {
            Console.WriteLine("\n=== Livres par catégorie ===");
            var booksByCategory = _library.GetAllBooks()
                .GroupBy(b => b.Category)
                .OrderBy(g => g.Key);

            if (!booksByCategory.Any())
            {
                Console.WriteLine("Aucun livre trouvé.");
                Pause();
                return;
            }

            foreach (var group in booksByCategory)
            {
                Console.WriteLine($"\n{group.Key}:");
                foreach (var book in group)
                {
                    Console.WriteLine($"  - {book.Title} par {book.Author.Name}");
                }
            }

            ShowExportMenu(booksByCategory.SelectMany(g => g), "Export des livres par catégorie");
        }

        /// <summary>
        /// Affiche les emprunteurs actifs de la bibliothèque.
        /// </summary>
        private static void DisplayBorrowers()
        {
            Console.WriteLine("\n=== Emprunteurs ===");
            var activeBorrowers = _library.GetAllBorrowers()
                .Where(b => _library.GetLoansByBorrower(b).Any());

            if (!activeBorrowers.Any())
            {
                Console.WriteLine("Aucun emprunteur.");
                Pause();
                return;
            }

            foreach (var borrower in activeBorrowers)
            {
                Console.WriteLine($"- {borrower.Name} ({borrower.Email})");
            }

            ShowExportMenu(activeBorrowers, "Export des emprunteurs");
        }

        /// <summary>
        /// Exporte une collection d'éléments au format XML.
        /// </summary>
        /// <typeparam name="T">Type des éléments à exporter.</typeparam>
        /// <param name="items">Collection d'éléments à exporter.</param>
        /// <param name="title">Titre de l'export.</param>
        private static void ExportToXml<T>(IEnumerable<T> items, string title)
        {
            try
            {
                var doc = new XDocument(
                    new XElement("Data",
                        new XElement("Title", title),
                        new XElement("Items",
                            items.Select(item => new XElement("Item",
                                item.GetType().GetProperties()
                                    .Select(p => new XElement(p.Name, p.GetValue(item)))
                            ))
                        )
                    )
                );

                Directory.CreateDirectory("Exports");
                string filename = $"export_{DateTime.Now:yyyyMMdd_HHmmss}.xml";
                doc.Save(Path.Combine("Exports", filename));
                Console.WriteLine($"Données exportées avec succès en XML dans le fichier {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'export XML : {ex.Message}");
            }
            Pause();
        }

        /// <summary>
        /// Exporte une collection d'éléments au format JSON.
        /// </summary>
        /// <typeparam name="T">Type des éléments à exporter.</typeparam>
        /// <param name="items">Collection d'éléments à exporter.</param>
        /// <param name="title">Titre de l'export.</param>
        private static void ExportToJson<T>(IEnumerable<T> items, string title)
        {
            try
            {
                var data = new
                {
                    Title = title,
                    Items = items
                };

                var options = new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                };

                Directory.CreateDirectory("Exports");
                string filename = $"export_{DateTime.Now:yyyyMMdd_HHmmss}.json";
                File.WriteAllText(Path.Combine("Exports", filename), JsonSerializer.Serialize(data, options));
                Console.WriteLine($"Données exportées avec succès en JSON dans le fichier {filename}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Erreur lors de l'export JSON : {ex.Message}");
            }
            Pause();
        }

        /// <summary>
        /// Charge les données depuis le fichier XML source.
        /// </summary>
        private static void LoadFromXml()
        {
            string xmlPath = Path.Combine("DataSource", "library_data.xml");
            if (!File.Exists(xmlPath))
                throw new FileNotFoundException("Le fichier XML n'existe pas dans le répertoire DataSource.");

            var doc = XDocument.Load(xmlPath);
            _library = new Library();

            LoadAuthorsFromXml(doc);
            LoadBorrowersFromXml(doc);
            LoadBooksFromXml(doc);
            LoadLoansFromXml(doc);
        }

        /// <summary>
        /// Charge les auteurs depuis le document XML.
        /// </summary>
        /// <param name="doc">Document XML source.</param>
        private static void LoadAuthorsFromXml(XDocument doc)
        {
            var authors = doc.Descendants("Author")
                .Select(a => new Author(
                    int.TryParse(a.Element("Id")?.Value, out var authorId) ? authorId : 0,
                    a.Element("Name")?.Value ?? string.Empty,
                    a.Element("Nationality")?.Value ?? string.Empty,
                    DateTime.TryParse(a.Element("BirthDate")?.Value, out var birthDate) ? birthDate : DateTime.Now
                ));

            foreach (var author in authors)
            {
                _library.AddAuthor(author);
            }
        }

        /// <summary>
        /// Charge les emprunteurs depuis le document XML.
        /// </summary>
        /// <param name="doc">Document XML source.</param>
        private static void LoadBorrowersFromXml(XDocument doc)
        {
            var borrowers = doc.Descendants("Borrower")
                .Select(b => new Borrower(
                    int.TryParse(b.Element("Id")?.Value, out var borrowerId) ? borrowerId : 0,
                    b.Element("Name")?.Value ?? string.Empty,
                    b.Element("Email")?.Value ?? string.Empty,
                    b.Element("Phone")?.Value ?? string.Empty
                ));

            foreach (var borrower in borrowers)
            {
                _library.AddBorrower(borrower);
            }
        }

        /// <summary>
        /// Charge les livres depuis le document XML.
        /// </summary>
        /// <param name="doc">Document XML source.</param>
        private static void LoadBooksFromXml(XDocument doc)
        {
            var books = doc.Descendants("Book")
                .Select(b =>
                {
                    var authorId = int.TryParse(b.Element("AuthorId")?.Value, out var id) ? id : 0;
                    var author = _library.GetAllAuthors().FirstOrDefault(a => a.Id == authorId);
                    if (author == null) return null;

                    return new Book(
                        int.TryParse(b.Element("Id")?.Value, out var bookId) ? bookId : 0,
                        b.Element("Title")?.Value ?? string.Empty,
                        author,
                        DateTime.TryParse(b.Element("PublicationDate")?.Value, out var pubDate) ? pubDate : DateTime.Now,
                        b.Element("ISBN")?.Value ?? string.Empty,
                        b.Element("Category")?.Value ?? string.Empty,
                        int.TryParse(b.Element("PageCount")?.Value, out var pageCount) ? pageCount : 0
                    );
                })
                .Where(b => b != null);

            foreach (var book in books)
            {
                if (book != null)
                    _library.AddBook(book);
            }
        }

        /// <summary>
        /// Charge les emprunts depuis le document XML.
        /// </summary>
        /// <param name="doc">Document XML source.</param>
        private static void LoadLoansFromXml(XDocument doc)
        {
            var loans = doc.Descendants("Loan")
                .Select(l =>
                {
                    var bookId = int.TryParse(l.Element("BookId")?.Value, out var bid) ? bid : 0;
                    var borrowerId = int.TryParse(l.Element("BorrowerId")?.Value, out var brid) ? brid : 0;
                    var book = _library.GetAllBooks().FirstOrDefault(b => b.Id == bookId);
                    var borrower = _library.GetAllBorrowers().FirstOrDefault(br => br.Id == borrowerId);

                    if (book == null || borrower == null) return null;

                    var loanDate = DateTime.TryParse(l.Element("LoanDate")?.Value, out var loanDateValue) ? loanDateValue : DateTime.Now;
                    var dueDate = DateTime.TryParse(l.Element("DueDate")?.Value, out var dueDateValue) ? dueDateValue : loanDate.AddDays(14);

                    return new Loan(
                        int.TryParse(l.Element("Id")?.Value, out var loanId) ? loanId : 0,
                        book,
                        borrower,
                        loanDate,
                        (dueDate - loanDate).Days
                    );
                })
                .Where(l => l != null);

            foreach (var loan in loans)
            {
                if (loan != null)
                    _library.AddLoan(loan);
            }
        }
    }

    public class LibraryData
    {
        public List<Author> Authors { get; set; } = new List<Author>();
        public List<Book> Books { get; set; } = new List<Book>();
        public List<Borrower> Borrowers { get; set; } = new List<Borrower>();
        public List<Loan> Loans { get; set; } = new List<Loan>();
    }
}
