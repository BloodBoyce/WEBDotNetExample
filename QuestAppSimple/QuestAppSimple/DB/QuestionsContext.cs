using Microsoft.EntityFrameworkCore;

namespace QuestAppSimple.DB;

public class QuestionsContext : DbContext
{
    public QuestionsContext(DbContextOptions<QuestionsContext> options) : base(options)
    {
    }
    public DbSet<Question> Questions { get; set; }
}