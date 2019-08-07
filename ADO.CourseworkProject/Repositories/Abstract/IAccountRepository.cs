namespace FilmRent.Repositories.Abstract
{
    using Models;
    using System.Collections.Generic;

    public interface IAccountRepository
        : IRepository<Account>
    {
        Account GetAccountByNickName(string nickName);
        IEnumerable<Account> GetAccountsByRole(string roleName);
    }
}
