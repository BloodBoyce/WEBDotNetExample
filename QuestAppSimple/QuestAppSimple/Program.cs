using Microsoft.Data.Sqlite;
using Microsoft.EntityFrameworkCore;
using QuestAppSimple.DB;

var builder = WebApplication.CreateBuilder(args);
// Add swagger services
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// config for entity framework
var connectionString = new SqliteConnectionStringBuilder() { DataSource = "Production.db" }.ToString();
builder.Services.AddDbContext<QuestionsContext>(x => x.UseSqlite(connectionString));

var app = builder.Build();
using (var scope = app.Services.CreateScope())
    scope.ServiceProvider.GetRequiredService<QuestionsContext>().Database.EnsureCreated();

// Activate static files serving
app.UseDefaultFiles();
app.UseStaticFiles();

// API Routes
app.MapGet("api/questions", async (QuestionsContext context)
    => await context.Questions.OrderByDescending(q => q.Votes).ToListAsync());


// Post Route to add a new question
app.MapPost("api/questions/", async (QuestionsContext context, string content) =>
{
    if (string.IsNullOrWhiteSpace(content))
        return Results.BadRequest("The Question Content can not be empty");

    context.Questions.Add(new Question { Content = content });
    await context.SaveChangesAsync();
    return Results.Ok();
});

// Post Route to vote for a question
app.MapPost("api/questions/{id:int}/vote", async (QuestionsContext context, int id) =>
{
    var question = await context.Questions.FirstOrDefaultAsync(q => q.Id == id);
    if (question is null)
        return Results.BadRequest("Invalid Question Id");
    if (question.Votes >= 10)
        return Results.BadRequest("You cannot vote more than 10 votes");

    question.Votes++;
    await context.SaveChangesAsync();
    return Results.Ok();
});

// Enable swagger and swaggerUI in DevMode:
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Run();