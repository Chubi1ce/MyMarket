namespace GraphQl
{
    public record Author(int Id, string Name);
    public record Book(int Id,string Title, Author Author);

    public record BookPayLoad(Book? record, string? error = null);
    public record BookInput(string title, int author);
    public record AuthorPayLoad(Author record);
    public record AuthorInput(string name);


    public class Query
    {
        public Task<List<Book>> GetBooks([Service] Repository repository)=>repository.GetBooksAsync();
        public Task<List<Book>> GetBooksByAuthorId([Service] Repository repository, int authorId) => repository.GetBooksAsync(authorId);
    }

    public class Mutation
    {
        public async Task<AuthorPayLoad> AddAuthor(AuthorInput input, [Service] Repository repository)
        {
            var author = new Author(Repository.GerAuthorId(), input.name);
            await repository.AddAuthor(author);
            return new AuthorPayLoad(author);
        }

        public async Task<BookPayLoad> AddBook(BookInput input, [Service] Repository repository)
        {
            var author = await repository.GetAuthor(input.author) ?? throw new Exception("author not found");
            var book = new Book(Repository.GerBookId(), input.title, author);
            await repository.AddBook(book);
            return new BookPayLoad(book);
        }
    }
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services
                .AddSingleton<Repository>()
                .AddGraphQLServer()
                .AddQueryType<Query>()
                .AddMutationType<Mutation>();
            
            var app = builder.Build();

            app.MapGraphQL();

            app.Run();
        }
    }

    public class Repository
    {
        public static int GerAuthorId() => authors.Last().Id + 1;
        public static int GerBookId() => books.Last().Id + 1;

        private static List<Author> authors = new List<Author>()
        {
            new Author(0,"Автор0"),
            new Author(1,"Автор1"),
            new Author(2,"Автор2")
        };

        private static List<Book> books = new List<Book>()
        {
            new Book(0,"Книга1",authors[0]),
            new Book(1,"Книга2",authors[1]),
            new Book(2,"Книга3",authors[2]),
            new Book(2,"Книга4",authors[2])
        };

        public Task<List<Book>> GetBooksAsync()
        {
            return Task.FromResult(books);
        }

        public Task<List<Book>> GetBooksAsync(int authorId)
        {
            return Task.FromResult(books.Where(x=>x.Author.Id==authorId).ToList());
        }
        public Task<Author?> GetAuthor(int authorId)
        {
            return Task.FromResult(authors.FirstOrDefault(a => a.Id == authorId));
        }

        public Task AddAuthor(Author author)
        {
            authors.Add(author);
            return Task.CompletedTask;
        }

        public Task AddBook(Book book)
        {
            books.Add(book);
            return Task.CompletedTask;
        }

    }
}
