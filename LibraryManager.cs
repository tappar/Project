using System;
using System.Collections.Generic;
using Microsoft.Data.Sqlite;

public class LibraryManager : ISearchable
{
    private readonly string connectionString = "Data Source=library.db";

    public LibraryManager()
    {
        CreateTable();
    }

    private void CreateTable()
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        string sql = @"
            CREATE TABLE IF NOT EXISTS Books (
                BookID INTEGER PRIMARY KEY AUTOINCREMENT,
                Title TEXT,
                AuthorName TEXT,
                AuthorEmail TEXT,
                Genre TEXT,
                PublicationYear INTEGER,
                ISBN TEXT
            );";

        using var command = new SqliteCommand(sql, connection);
        command.ExecuteNonQuery();
    }

    // CREATE
    public void AddBook(Book book)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        string sql = @"
            INSERT INTO Books 
            (Title, AuthorName, AuthorEmail, Genre, PublicationYear, ISBN)
            VALUES (@Title, @AuthorName, @AuthorEmail, @Genre, @Year, @ISBN);";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@Title", book.Title);
        command.Parameters.AddWithValue("@AuthorName", book.Author.Name);
        command.Parameters.AddWithValue("@AuthorEmail", book.Author.Email);
        command.Parameters.AddWithValue("@Genre", book.Genre);
        command.Parameters.AddWithValue("@Year", book.PublicationYear);
        command.Parameters.AddWithValue("@ISBN", book.ISBN);

        command.ExecuteNonQuery();
    }

    // READ
    public List<Book> ViewAllBooks()
    {
        var books = new List<Book>();

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        string sql = "SELECT * FROM Books;";
        using var command = new SqliteCommand(sql, connection);
        using var reader = command.ExecuteReader();

        while (reader.Read())
        {
            var author = new Author(
                reader["AuthorName"].ToString(),
                reader["AuthorEmail"].ToString()
            );

            var book = new Book(
                reader["Title"].ToString(),
                reader["Genre"].ToString(),
                Convert.ToInt32(reader["PublicationYear"]),
                reader["ISBN"].ToString(),
                author
            );

            books.Add(book);
        }

        return books;
    }

    // UPDATE
    public void UpdateBook(int bookId, Book updatedBook)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        string sql = @"
            UPDATE Books SET
                Title = @Title,
                AuthorName = @AuthorName,
                AuthorEmail = @AuthorEmail,
                Genre = @Genre,
                PublicationYear = @Year,
                ISBN = @ISBN
            WHERE BookID = @ID;";

        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@Title", updatedBook.Title);
        command.Parameters.AddWithValue("@AuthorName", updatedBook.Author.Name);
        command.Parameters.AddWithValue("@AuthorEmail", updatedBook.Author.Email);
        command.Parameters.AddWithValue("@Genre", updatedBook.Genre);
        command.Parameters.AddWithValue("@Year", updatedBook.PublicationYear);
        command.Parameters.AddWithValue("@ISBN", updatedBook.ISBN);
        command.Parameters.AddWithValue("@ID", bookId);

        command.ExecuteNonQuery();
    }

    // DELETE
    public void DeleteBook(int bookId)
    {
        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        string sql = "DELETE FROM Books WHERE BookID = @ID;";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@ID", bookId);

        command.ExecuteNonQuery();
    }

    // SEARCH
    public List<LibraryItem> SearchByTitle(string title)
    {
        return Search("Title", title);
    }

    public List<LibraryItem> SearchByAuthor(string authorName)
    {
        return Search("AuthorName", authorName);
    }

    private List<LibraryItem> Search(string column, string value)
    {
        var results = new List<LibraryItem>();

        using var connection = new SqliteConnection(connectionString);
        connection.Open();

        string sql = $"SELECT * FROM Books WHERE {column} LIKE @value;";
        using var command = new SqliteCommand(sql, connection);
        command.Parameters.AddWithValue("@value", $"%{value}%");

        using var reader = command.ExecuteReader();
        while (reader.Read())
        {
            var author = new Author(
                reader["AuthorName"].ToString(),
                reader["AuthorEmail"].ToString()
            );

            var book = new Book(
                reader["Title"].ToString(),
                reader["Genre"].ToString(),
                Convert.ToInt32(reader["PublicationYear"]),
                reader["ISBN"].ToString(),
                author
            );

            results.Add(book);
        }

        return results;
    }
}
