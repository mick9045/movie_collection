namespace FilmRent.ViewModels.Abstract
{
    using System.Security;

    interface IHavePassword
    {
        SecureString Password { get; }
    }
}
