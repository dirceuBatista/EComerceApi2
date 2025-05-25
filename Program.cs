using FluentValidation;
using LivrariaApi.Data;
using LivrariaApi.Services.ContollerService;
using LivrariaApi.Valideitors;
using LivrariaApi.ViewModels;
using LivrariaApi.Data.Mapper;
using LivrariaApi.ViewModels.InputOrder;
using LivrariaApi.ViewModels.InputViewModel;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddAutoMapper(typeof(OrderProfile).Assembly);


builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<BookService>();
builder.Services.AddScoped<CategoryService>();


builder.Services.AddScoped<IValidator<InputUserCreate>, UserCreateValidation>();
builder.Services.AddScoped<IValidator<InputUserUpdate>, UserUpdateValidation>();

builder.Services.AddScoped<IValidator<BookViewModel>, BookValidator>();
builder.Services.AddScoped<IValidator<InputOrderCreate>, OrderCreateValidator>();
builder.Services.AddScoped<IValidator<InputOrderUpdate>, OrderUpdateValidator>();
builder.Services.AddScoped<IValidator<CategoryViewModel>, CategoryValidator>();


var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}



app.MapControllers();

app.Run();




