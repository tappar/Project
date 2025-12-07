using System;
using System.Collections.Generic;

class Program
{
    static void Main(string[] args)
    {
        LibraryManager manager = new LibraryManager();
        bool running = true;

        while (running)
        {
            Console.WriteLine("\n--- Personal Library Management System (Phase 1) ---");
            Console.WriteLine("1. Add Book");
            Console.WriteLine("2. View All Books");
            Console.WriteLine("3. Update Book");
            Console.WriteLine("4. Delete Book");
            Console.WriteLine("5. Search by Title");
            Console.WriteLine("6. Search by Author");
            Console.WriteLine("7. Exit");
            Console.Write("Choose an option: ");

            string choice = Console.ReadLine();
            Console.WriteLine();

            switch (choice)
            {
                case "1":
                    AddBook(manager);
                    break;
                case "2":
                    ViewBooks(manager);
                    break;
                case "3":
                    UpdateBook(manager);
                    break;
                case "4":
                    DeleteBook(manager);
                    break;
                case "5":
                    SearchTitle(manager);
                    break;
                case "6":
                    SearchAuthor(manager);
                    break;
                case "7":
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid option. Try again.");
                    break;
            }
        }
    }

    static void AddBook(LibraryManager manager)
    {
        Console.Write("Enter title: ");
        string title = Console.ReadLine();

        Console.Write("Enter genre: ");
        string genre = Console.ReadLine();

        Console.Write("Enter publication year: ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Enter ISBN: ");
        string isbn = Console.ReadLine();

        Console.Write("Enter author name: ");
        string authorName = Console.ReadLine();

        Console.Write("Enter author email: ");
        string authorEmail = Console.ReadLine();

        Author author = new Author(authorName, authorEmail);
        Book book = new Book(title, genre, year, isbn, author);

        manager.AddBook(book);
        Console.WriteLine("Book added successfully!");
    }

    static void ViewBooks(LibraryManager manager)
    {
        var books = manager.ViewAllBooks();

        if (books.Count == 0)
        {
            Console.WriteLine("No books found.");
            return;
        }

        for (int i = 0; i < books.Count; i++)
        {
            Console.WriteLine($"\nBook #{i}");
            Console.WriteLine(books[i].ToString());
        }
    }

    static void UpdateBook(LibraryManager manager)
    {
        Console.Write("Enter book index to update: ");
        int index = int.Parse(Console.ReadLine());

        Console.Write("Enter new title: ");
        string title = Console.ReadLine();

        Console.Write("Enter new genre: ");
        string genre = Console.ReadLine();

        Console.Write("Enter new publication year: ");
        int year = int.Parse(Console.ReadLine());

        Console.Write("Enter new ISBN: ");
        string isbn = Console.ReadLine();

        Console.Write("Enter new author name: ");
        string authorName = Console.ReadLine();

        Console.Write("Enter new author email: ");
        string authorEmail = Console.ReadLine();

        Book updatedBook = new Book(title, genre, year, isbn, new Author(authorName, authorEmail));
        manager.UpdateBook(index, updatedBook);

        Console.WriteLine("Book updated.");
    }

    static void DeleteBook(LibraryManager manager)
    {
        Console.Write("Enter book index to delete: ");
        int index = int.Parse(Console.ReadLine());

        manager.DeleteBook(index);
        Console.WriteLine("Book deleted.");
    }

    static void SearchTitle(LibraryManager manager)
    {
        Console.Write("Enter title keyword: ");
        string keyword = Console.ReadLine();

        var results = manager.SearchByTitle(keyword);

        if (results.Count == 0)
        {
            Console.WriteLine("No matching books found.");
            return;
        }

        foreach (var item in results)
        {
            item.DisplayDetails();
            Console.WriteLine();
        }
    }

    static void SearchAuthor(LibraryManager manager)
    {
        Console.Write("Enter author name keyword: ");
        string keyword = Console.ReadLine();

        var results = manager.SearchByAuthor(keyword);

        if (results.Count == 0)
        {
            Console.WriteLine("No matching books found.");
            return;
        }

        foreach (var item in results)
        {
            item.DisplayDetails();
            Console.WriteLine();
        }
    }
}
