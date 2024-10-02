using LibraryManagment.InterFace.IRepository.IAurthorRepo;
using LibraryManagment.InterFace.IRepository.IBookRepository;
using LibraryManagment.InterFace.IRepository.IBookTransactionRepo;
using LibraryManagment.InterFace.IRepository.IGenreRepository;
using LibraryManagment.InterFace.IRepository.IPublicationRepository;
using LibraryManagment.InterFace.IRepository.IUserRepo;
using LibraryManagment.InterFace.IService.IAurthorServ;
using LibraryManagment.InterFace.IService.IBookService;
using LibraryManagment.InterFace.IService.IBookTransactionService;
using LibraryManagment.InterFace.IService.IGenreService;
using LibraryManagment.InterFace.IService.iPublicationService;
using LibraryManagment.InterFace.IService.IUserServ;
using LibraryManagment.Repository.AurthorRepository;
using LibraryManagment.Repository.BookRepository;
using LibraryManagment.Repository.BookTransaction;
using LibraryManagment.Repository.GenreRepository;
using LibraryManagment.Repository.PublicationRepository;
using LibraryManagment.Repository.UserRepository;
using LibraryManagment.Service.AurthorService;
using LibraryManagment.Service.BookService;
using LibraryManagment.Service.BookTransactionService;
using LibraryManagment.Service.GenreService;
using LibraryManagment.Service.PublicationService;
using LibraryManagment.Service.UserService;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

builder.Services.AddSingleton<IAurthorRepository>(provider => new AurthorRepository(connectionString));
builder.Services.AddScoped<IAurthorService, AurthorService>();

builder.Services.AddSingleton<IUserRepository>(provider => new UserRepository(connectionString));
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddSingleton<IPublicationRepository>(provider => new PublicationRepository(connectionString));
builder.Services.AddScoped<IPublicationService, PublicationService>();

builder.Services.AddSingleton<IGenreRepository>(provider => new GenreRepository(connectionString));
builder.Services.AddScoped<IGenreService, GenreService>();

builder.Services.AddSingleton<IBookRepository>(provider => new BookRepository(connectionString));
builder.Services.AddScoped<IBookService, BookService>();


builder.Services.AddSingleton<IBookTransactionRepository>(provider => new BookTransactionRepository(connectionString));
builder.Services.AddScoped<IBookTransactionService, BookTransactionService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseStaticFiles();//img
app.UseRouting();//img

app.UseHttpsRedirection();
app.UseCors(policy => policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

app.UseAuthorization();

app.MapControllers();

app.Run();
