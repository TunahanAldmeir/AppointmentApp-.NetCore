using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Abstract
{
    public interface IUserServices
    {
        void AddUser(User  user);
        void DeleteUser(User user);
        void UpdateUser(User user);

        List<User> GetAllUsers();

        User GetUserById(int id);
        User logUser(User user);
    }
}
