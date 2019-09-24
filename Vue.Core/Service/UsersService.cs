using System.Collections.Generic;
using System.Linq;
using Vue.Core.Common;
using Vue.Core.Data;
using Vue.Core.Data.Entities;
using Vue.Core.Service.Interface;

namespace Vue.Core.Service
{
    public class UsersService : IUsersService<Users>
    {
        private ApplicationDbContext _db;
        public UsersService(ApplicationDbContext db) => _db = db;

        public IEnumerable<Users> GetAll()
        {
            return _db.Users;
        }

        public Users GetById(int id)
        {
            return _db.Users.Find(id);
        }

        public Users Create(Users users)
        {
            // validation
            if (string.IsNullOrWhiteSpace(users.Password))
                throw new AppException("Password is required");

            if (_db.Users.Any(x => x.LoginName == users.LoginName))
                throw new AppException("Username \"" + users.LoginName + "\" is already taken");

            byte[] passwordHash, passwordSalt;
            Security.CreatePasswordHash(users.Password, out passwordHash, out passwordSalt);

            users.PasswordHash = passwordHash;
            users.PasswordSalt = passwordSalt;

            _db.Users.Add(users);
            _db.SaveChanges();

            return users;
        }

        public void Update(Users t)
        {
            throw new System.NotImplementedException();
        }

        public void Delete(int it)
        {
            throw new System.NotImplementedException();
        }
        
        public Users Authenticate(string loginname, string password)
        {
            if (string.IsNullOrEmpty(loginname) || string.IsNullOrEmpty(password))
                return null;

            var user = _db.Users.SingleOrDefault(x => x.LoginName == loginname);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!Security.VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return user;
        }
    }
}