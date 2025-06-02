using System.Text;
using FluentValidation;
using LivrariaApi;
using LivrariaApi.Data;
using LivrariaApi.Services.ContollerService;
using LivrariaApi.Valideitors;
using LivrariaApi.ViewModels;
using LivrariaApi.Data.Mapper;
using LivrariaApi.Services;
using LivrariaApi.ViewModels.InputOrder;
using LivrariaApi.ViewModels.InputViewModel;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;


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
builder.Services.AddScoped<AccountService>();


builder.Services.AddScoped<IValidator<InputUserCreate>, UserCreateValidation>();
builder.Services.AddScoped<IValidator<InputUserUpdate>, UserUpdateValidation>();


builder.Services.AddScoped<IValidator<InputOrderCreate>, OrderCreateValidation>();
builder.Services.AddScoped<IValidator<InputOrderUpdate>, OrderUpdateValidator>();

builder.Services.AddScoped<IValidator<CategoryViewModel>, CategoryValidator>();
builder.Services.AddScoped<IValidator<BookViewModel>, BookValidator>();

builder.Services.AddTransient<TokenService>();

var key = Encoding.ASCII.GetBytes(Configuration.JwtKey);
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    x.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(key),
        ValidateIssuer = false,
        ValidateAudience = false
    };
});

var app = builder.Build();
if (app.Environment.IsDevelopment() || app.Environment.IsProduction())
{
    app.UseSwagger();
    app.UseSwaggerUI(); 
}

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();




