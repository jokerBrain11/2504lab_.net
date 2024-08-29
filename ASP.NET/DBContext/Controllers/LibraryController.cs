using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;

namespace DBContext.Controllers;

[ApiController]
[Route("[controller]")]
public class LibraryController : ControllerBase
{
    private readonly CodeFirstContext _codeContext;
    public LibraryController(CodeFirstContext codeContext)
    {
        _codeContext = codeContext;
    }

    [HttpGet("GetAuthors")]
    public IEnumerable<Author> GetAuthors()
    {
        var authors = _codeContext.Authors.Select(a => new Author
        {
            Id = a.Id,
            FirstName = a.FirstName,
            LastName = a.LastName,
            Email = a.Email,
            Phone = a.Phone,
            Address = a.Address,
            City = a.City,
            State = a.State,
            Zip = a.Zip,
            Notes = a.Notes,
            Books = _codeContext.Books.Where(b => b.AuthorId == a.Id).ToList()
        }).ToList();
        return authors;
    }

    [HttpPost("InsertAuthor")]
    public Author InsertAuthor(InsertAuthorRequest author)
    {
        var newAuthor = new Author
        {
            FirstName = author.FirstName,
            LastName = author.LastName,
            Email = author.Email,
            Phone = author.Phone,
            Address = author.Address,
            City = author.City,
            State = author.State,
            Zip = author.Zip,
            Notes = author.Notes
        };
        _codeContext.Authors.Add(newAuthor);
        _codeContext.SaveChanges();
        return newAuthor;
    }

    [HttpPost("DeleteAuthors")]
    public Author DeleteAuthors(int authorId)
    {
        var author = _codeContext.Authors.FirstOrDefault(a => a.Id == authorId);
        if (author != null)
        {
            _codeContext.Authors.Remove(author);
            _codeContext.SaveChanges();
        }
        return author;
    }

    [HttpGet("GetBooks")]
    public IEnumerable<Book> GetBooks()
    {
        return _codeContext.Books.ToList();
    }

    [HttpPost("InsertBook")]
    public Book InsertBook(InsertBookRequest book)
    {
        var newBook = new Book
        {
            Title = book.Title,
            ISBN = book.ISBN,
            AuthorId = book.AuthorId,
            GenreId = book.GenreId,
            PublisherId = book.PublisherId,
            PageCount = book.PageCount,
            PublishDate = DateTime.Today,
            Description = book.Description,
            Notes = book.Notes
        };
        _codeContext.Books.Add(newBook);
        _codeContext.SaveChanges();
        return newBook;
    }

    [HttpPost("DeleteBooks")]
    public Book DeleteBooks(string bookName)
    {
        var book = _codeContext.Books.FirstOrDefault(b => b.Title == bookName);
        if (book != null)
        {
            _codeContext.Books.Remove(book);
            _codeContext.SaveChanges();
        }
        return book;
    }
}
