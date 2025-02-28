using Microsoft.EntityFrameworkCore;

namespace sjukhus_journal.Models;

public class JournalDbContext : DbContext
{
    public DbSet<Journal> Journaler { get; set; }

    public JournalDbContext(DbContextOptions<JournalDbContext> options) : base(options) { }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlite("Data Source=journal.db");
        }
    }
}