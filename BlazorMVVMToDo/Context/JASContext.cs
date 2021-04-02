using System.Diagnostics;
using System.Threading.Tasks;
using BlazorMVVMToDo.Models;
using Microsoft.EntityFrameworkCore;

namespace BlazorMVVMToDo.Context
{

  public class JASContext : DbContext
  {

    /// <summary>
    /// Magic string.
    /// </summary>
    public static readonly string RowVersion = nameof(RowVersion);

    /// <summary>
    /// Magic strings.
    /// </summary>
    public static readonly string JASDb = nameof(JASDb).ToLower();

    /// <summary>
    /// Inject options.
    /// </summary>
    /// <param name="options">The <see cref="DbContextOptions{JASDBContext}"/>
    /// for the context
    /// </param>
    public JASContext(DbContextOptions<JASContext> options)
        : base(options)
    {
      Debug.WriteLine($"{ContextId} context created.");
    }

    /// <summary>
    /// List of <see cref="Issue"/>.
    /// </summary>
    public DbSet<Issue> Issues { get; set; }


    /// <summary>
    /// Define the models.
    /// </summary>
    /// <param name="modelBuilder">The <see cref="ModelBuilder"/>.</param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

      // this property isn't on the C# class
      // so we set it up as a "shadow" property and use it for concurrency
      modelBuilder.Entity<Issue>().Property<byte[]>(RowVersion).IsRowVersion();
      // Set key for entity
      modelBuilder.Entity<Issue>().HasKey(p => p.Id);

      base.OnModelCreating(modelBuilder);
    }

    /// <summary>
    /// Dispose pattern.
    /// </summary>
    public override void Dispose()
    {
      Debug.WriteLine($"{ContextId} context disposed.");
      base.Dispose();
    }

    /// <summary>
    /// Dispose pattern.
    /// </summary>
    /// <returns>A <see cref="ValueTask"/></returns>
    public override ValueTask DisposeAsync()
    {
      Debug.WriteLine($"{ContextId} context disposed async.");
      return base.DisposeAsync();
    }
  }
}
