namespace FilmRent.Repositories
{
    using System.Collections.Generic;
    using Models;
    using Abstract;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Data.Entity;
    using System;

    public class AccountRepository
        : Repository<Account>, IAccountRepository
    {
        #region Fields

        #endregion //Feilds

        #region Constructors
        public AccountRepository(FilmRentContext context)
            : base(context)
        {
             
        }
        #endregion //Constuctors

        #region IAccountRepository Members
        public Account GetAccountByNickName(string nickName)
        {
            return _context.Accounts
                .Where(c => c.Nickname == nickName)
                .FirstOrDefault();
        }

        public IEnumerable<Account> GetAccountsByRole(string role)
        {
            return _context.Accounts
                .Include(c => c.AccountRole)
                .Where(c => c.AccountRole.RoleDefenition == role);
        }
        #endregion IAccountRepositoryMethods

        #region Methods

        #endregion //Methods

    }
}
