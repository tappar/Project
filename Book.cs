using System;

public class Book : LibraryItem
{
    public string ISBN { get; set; }
    public Author Author { get; set; }

    public Book() { }

    public Book(string title, string genre, int year, string isbn, Author author)
        : base(title, genre, year)
    {
        ISBN = isbn;
        Author = author;
    }

    public override void DisplayDetails()
    {
        Console.WriteLine(ToString());
    }

    public override string ToString()
    {
        return $"Title: {Title}\n" +
               $"Author: {Author}\n" +
               $"Genre: {Genre}\n" +
               $"Publication Year: {PublicationYear}\n" +
               $"ISBN: {ISBN}";
    }
}
