using DomainClass;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataLayer.Repository
{
    public class UserRepository
    {
        public UserRepository()
        {
            this.db=new Context();
        }
        public UserRepository(Context _db)
        {
            this.db = _db;
        }
        public Context db;

        public User GetByUserName(string userName)
        {
            return db.Users.SingleOrDefault(u => u.UserName == userName);
        }
        public User GetIdentity(String USerName, String Password)
        {
            return db.Users.SingleOrDefault(u=>u.UserName==USerName&&u.Password==Password);

        }
    }
}
