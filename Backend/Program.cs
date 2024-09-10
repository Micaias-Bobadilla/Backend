using Backend.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
//builder.Services.AddSingleton<IPeopleServices, People2Service>();
builder.Services.AddKeyedSingleton<IPeopleServices, People2Service>("people2Service");
builder.Services.AddKeyedSingleton<IPeopleServices, PeopleService>("peopleService");

builder.Services.AddKeyedSingleton<IRandomService, RandomService>("randomSingleton");
builder.Services.AddKeyedScoped<IRandomService, RandomService>("randomScoped");
builder.Services.AddKeyedTransient<IRandomService, RandomService>("randomTransient");

builder.Services.AddScoped<IPostsService, PostsService>();

builder.Services.AddHttpClient<IPostsService, PostsService>(c =>
{
    c.BaseAddress = new Uri("https://jsonplaceholder.org/posts");
});


builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
