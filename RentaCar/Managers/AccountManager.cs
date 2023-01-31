using RentaCar.Entities;
using RentaCar.Models;

namespace RentaCar.Managers
{
    public interface IAccountManager
    {
        bool CheckMember(LoginViewModel model);
        void RegisterMember(RegisterViewModel model);
        Member GetMember(string username);
    }

    public class AccountManager : IAccountManager
    {
        private DatabaseContext _databaseContext;

        public AccountManager(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public void RegisterMember(RegisterViewModel model)
        {
            Member member = new Member();
            member.Username = model.Username;
            member.Password = model.Password;
            member.Confirmed = false;
            member.Role = "member";

            _databaseContext.Members.Add(member);
            _databaseContext.SaveChanges();
        }

        public bool CheckMember(LoginViewModel model)
        {
            return _databaseContext.Members.Any(
                x => x.Username == model.Username && x.Password == model.Password && x.Confirmed == true);
        }

        public Member GetMember(string username)
        {
            return _databaseContext.Members.Where(x => x.Username == username).FirstOrDefault();
        }
    }
}
