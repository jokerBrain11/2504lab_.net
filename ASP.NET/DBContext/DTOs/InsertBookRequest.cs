public class InsertBookRequest
{
    public int BookId { get; set; }
    public string Title { get; set; }
    public string ISBN { get; set; }
    public int AuthorId { get; set; }
    public int GenreId { get; set; }
    public int PublisherId { get; set; }
    public int PageCount { get; set; }
    public DateTime PublishDate { get; set; }
    public string Description { get; set; }
    public string Notes { get; set; }
}