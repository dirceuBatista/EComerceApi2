using FluentValidation;
using LivrariaApi.Data;
using LivrariaApi.Services.ContollerService;
using LivrariaApi.Valideitors;
using LivrariaApi.ViewModels;
using LivrariaApi.Data.Mapper;


var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDbContext>();

builder.Services.AddEndpointsApiExplorer(); 
builder.Services.AddSwaggerGen(); 
builder.Services.AddControllers();
builder.Services.AddAutoMapper(typeof(UserProfile).Assembly);
builder.Services.AddAutoMapper(typeof(OrderProfile).Assembly);


builder.Services.AddScoped<UserService>();
builder.Services.AddScoped<OrderService>();

builder.Services.AddScoped<IValidator<UserViewModel>, UserValidation>();
builder.Services.AddScoped<IValidator<OrderViewModel>, OrderValidation>();


var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}



app.MapControllers();

app.Run();



