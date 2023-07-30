using DataAccessLayer.Abstract;
using DataAccessLayer.Concrete;
using DataAccessLayer.Repositories;
using EntityLayer.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer.EntityFrameWork
{
    public class EfUserRepository:GenericRepository<User>,IUserDal
    {
        public User logUser(User user)  
        {
            using (var c = new Context())
            {
                return c.Users.FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);
            }
        }
    }
}
