using Microsoft.EntityFrameworkCore;

namespace BlazorMVVMToDo.Context
{
  /// <summary>
  /// Factory to create new instances of <see cref="JASDBContext"/>.
  /// </summary>
  /// <typeparam name="TContext">The type of <seealso cref="JASContext"/> to create.</typeparam>
  public interface IJASContextFactory<TContext> where TContext : DbContext
  {
    /// <summary>
    /// Generate a new <see cref="DbContext"/>.
    /// </summary>
    /// <returns>A new instance of <see cref="TContext"/>.</returns>
    TContext CreateDbContext();
  }
}
