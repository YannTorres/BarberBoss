namespace BarberBoss.Domain.Repositories.User;
public interface IUserReadOnlyRepository
{
    /// <summary>
    /// Return True if email exist in database and False otherwise.
    /// </summary>
    /// <param name="email"></param>
    /// <returns></returns>
    Task<bool> ExistUserWithThisEmail(string email);

    Task<Entities.User?> GetUserByEmail(string email);
}
