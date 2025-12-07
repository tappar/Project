using System;

public abstract class LibraryItem
{
    public string Title { get; set; }
    public string Genre { get; set; }
    public int PublicationYear { get; set; }

    public LibraryItem() { }

    public LibraryItem(string title, string genre, int year)
    {
        Title = title;
        Genre = genre;
        PublicationYear = year;
    }

    public abstract void DisplayDetails();
}
