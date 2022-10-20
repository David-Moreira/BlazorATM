using ATM.Models;

using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ATM.Security
{

    public interface IUserSession
    {
        Task<bool> Register(RegisterViewModel registerViewModel);

        Task<bool> Login(LoginViewModel loginViewModel);

        Task<User> GetLoggedInUser();

        Task Logout();

    }

    //Very simple dumbed down in memory version...
    public class UserSession : IUserSession
    {
        public UserSession()
        {
        }
        Dictionary<string, User> _userStore = new();
        private User _loggedInUser;

        public Task<bool> Login(LoginViewModel loginViewModel)
        {
            if (_userStore.TryGetValue(loginViewModel.Email, out var user))
            {
                if (user.Password != loginViewModel.Password)
                    return Task.FromResult(false);

                _loggedInUser = user;
                return Task.FromResult(true);
            }
            else
                return Task.FromResult(false);
        }

        public Task<User> GetLoggedInUser()
            => Task.FromResult(_loggedInUser);

        public Task Logout()
        {
            _loggedInUser = null;
            return Task.CompletedTask;
        }

        public Task<bool> Register(RegisterViewModel registerViewModel)
        {
            if (_userStore.ContainsKey(registerViewModel.Email))
                return Task.FromResult(false);

            User user = new User()
            {
                AccountName = registerViewModel.AccountName,
                Email = registerViewModel.Email,
                FirstName = registerViewModel.FirstName,
                LastName = registerViewModel.LastName,
                Password = registerViewModel.Password
            };
            _userStore.Add(registerViewModel.Email, user);

            return Task.FromResult(true);
        }
    }
}
