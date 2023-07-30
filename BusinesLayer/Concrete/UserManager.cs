using BusinesLayer.Abstract;
using DataAccessLayer.Abstract;
using DataAccessLayer.EntityFrameWork;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace BusinesLayer.Concrete
{
    public class UserManager : IUserServices
    {
        IUserDal _userdal;
        public UserManager(IUserDal userDal)
        {
            _userdal = userDal;
        }
        public void AddUser(User user)
        {
           _userdal.Insert(user);
        }

        public void DeleteUser(User user)
        {
            _userdal.Delete(user);
        }

        public List<User> GetAllUsers()
        {
            return _userdal.GetAll();
        }

        public User GetUserById(int id)
        {
            return _userdal.GetByID(id);
        }

        public User logUser(User user)
        {
            return _userdal.logUser(user);
        }

        public void UpdateUser(User user)
        {
            _userdal.Update(user);
        }
    }
}
