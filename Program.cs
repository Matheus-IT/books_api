var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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

app.MapGet("book/", () =>
{
    return books;
});

app.Run();


class Book
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Author { get; set; }
}
