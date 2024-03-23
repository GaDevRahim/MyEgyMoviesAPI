namespace MyEgyMoviesAPI.Models
{
    public class ApplicationDbContext : DbContext 
    {
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            
        }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    base.OnModelCreating(modelBuilder);

        //    modelBuilder.Entity<Genre>().HasMany(m => m.Movies).WithOne(g => g.Genre);
            
        //}
    }
}
