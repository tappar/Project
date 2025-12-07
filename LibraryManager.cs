using System;
using System.Collections.Generic;
using System.Linq;

public class LibraryManager : ISearchable
{
    private List<Book> books = new List<Book>();

    public void AddBook(Book book)
    {
        books.Add(book);
    }

    public List<Book> ViewAllBooks()
    {
        return new List<Book>(books);
    }

    public void UpdateBook(int index, Book updatedBook)
    {
        if (index >= 0 && index < books.Count)
        {
            books[index] = updatedBook;
        }
        else
        {
            Console.WriteLine("Invalid book index.");
        }
    }

    public void DeleteBook(int index)
    {
        if (index >= 0 && index < books.Count)
        {
            books.RemoveAt(index);
        }
        else
        {
            Console.WriteLine("Invalid book index.");
        }
    }

    public List<LibraryItem> SearchByTitle(string title)
    {
        return books.Where(b => b.Title.Contains(title, StringComparison.OrdinalIgnoreCase))
                    .Cast<LibraryItem>()
                    .ToList();
    }

    public List<LibraryItem> SearchByAuthor(string authorName)
    {
        return books.Where(b => b.Author.Name.Contains(authorName, StringComparison.OrdinalIgnoreCase))
                    .Cast<LibraryItem>()
                    .ToList();
    }
}
