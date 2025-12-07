using System.Collections.Generic;

public interface ISearchable
{
    List<LibraryItem> SearchByTitle(string title);
    List<LibraryItem> SearchByAuthor(string authorName);
}
