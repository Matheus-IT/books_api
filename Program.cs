
using BooksApi;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<DataContext>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var books = new List<Book>
{
    new Book{Id=1, Title="harry potter", Author="J. K. Rowling"},
    new Book{Id=2, Title="1984", Author="George Orwel"},
    new Book{Id=3, Title="The martian", Author="Andy"},
};

app.MapGet("book/", async (DataContext context) => await context.Books.ToListAsync());

app.MapGet("book/{id}", async (DataContext context, int id) => {  
    return await context.Books.FindAsync(id) is Book book ?
        Results.Ok(book) : 
        Results.NotFound("The book doesn't exist");
});

app.MapPost("/book", async (DataContext context, Book book) =>
{
    context.Books.Add(book);
    await context.SaveChangesAsync();
    return Results.Ok(await context.Books.ToListAsync());
});

app.MapPut("book/{id}", (Book updatedBook, int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("The book doesn't exist");
    book.Title = updatedBook.Title;
    book.Author = updatedBook.Author;
    return Results.Ok(book);
});

app.MapDelete("book/{id}", (int id) =>
{
    var book = books.Find(b => b.Id == id);
    if (book is null)
        return Results.NotFound("The book doesn't exist");
    books.Remove(book);
    return Results.Ok(book);
});

app.Run();


public class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}
