using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Vue.Core.Common;
using Vue.Core.Data.Entities;

namespace Vue.Core.Data
{
    public class Seeder
    {
        private readonly IServiceProvider _serviceProvider;

        public Seeder(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public void Init()
        {
            using (var scope = _serviceProvider.CreateScope())
            {
                var services = scope.ServiceProvider;
                var db = services.GetRequiredService<ApplicationDbContext>();
                if (db.Users.Any()) return;

                List<RolesApps> rolesapps = new List<RolesApps>()
                {
                    new RolesApps()
                    {
                        Id = 1, Gid = Guid.NewGuid(), AppsId = 1, RolesId = 1,
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new RolesApps()
                    {
                        Id = 2, Gid = Guid.NewGuid(), Delete = true, Write = true, Read = true, Print = true,
                        AppsId = 2,
                        RolesId = 1,
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    }
                };

                List<Apps> apps = new List<Apps>()
                {
                    new Apps()
                    {
                        Id = 1, Gid = Guid.NewGuid(), AppName = "Users",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new Apps()
                    {
                        Id = 2, Gid = Guid.NewGuid(), AppName = "UsersList", ParentId = 1,
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    }
                };

                List<AppsApiCollection> appsapicollection = new List<AppsApiCollection>()
                {
                    new AppsApiCollection()
                    {
                        Id = 1, Gid = Guid.NewGuid(), AppsId = 2, RoutePath = "api/Users/GetAll",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                };


                List<Roles> roles = new List<Roles>()
                {
                    new Roles()
                    {
                        Id = 1, Gid = Guid.NewGuid(), RoleName = "Admin", CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", Lock = true,
                        IsDeleted = false
                    }
                };

                List<UsersRoles> usersroles = new List<UsersRoles>()
                {
                    new UsersRoles()
                    {
                        Id = 1, Gid = Guid.NewGuid(), CreatedAt = DateTime.Now, RolesId = 1, UsersId = 1,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    }
                };

                byte[] passwordHash, passwordSalt;
                Security.CreatePasswordHash("p12345", out passwordHash, out passwordSalt);
                List<Users> users = new List<Users>()
                {
                    new Users
                    {
                        Id = 1, Gid = Guid.NewGuid(),
                        LoginName = "admin", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Dyco", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
                        DateOfBirth = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    },
                    new Users
                    {
                        Id = 2, Gid = Guid.NewGuid(),
                        LoginName = "Test2", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Test2", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
                        DateOfBirth = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    },
                    new Users
                    {
                        Id = 3, Gid = Guid.NewGuid(),
                        LoginName = "Test3", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Test3", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
                        DateOfBirth = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    },
                    new Users
                    {
                        Id = 4, Gid = Guid.NewGuid(),
                        LoginName = "Test4", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Test4", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
                        DateOfBirth = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    },
                    new Users
                    {
                        Id = 5, Gid = Guid.NewGuid(),
                        LoginName = "Test5", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Test5", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
                        DateOfBirth = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    },
                    new Users
                    {
                        Id = 6, Gid = Guid.NewGuid(),
                        LoginName = "Test6", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Test6", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
                        DateOfBirth = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    },
                    new Users
                    {
                        Id = 7, Gid = Guid.NewGuid(),
                        LoginName = "Test7", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Test7", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
                        DateOfBirth = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    },
                    new Users
                    {
                        Id = 8, Gid = Guid.NewGuid(),
                        LoginName = "Test8", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Test8", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
                        DateOfBirth = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    },
                    new Users
                    {
                        Id = 9, Gid = Guid.NewGuid(),
                        LoginName = "Test9", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Test9", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
                        DateOfBirth = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    },
                    new Users
                    {
                        Id = 10, Gid = Guid.NewGuid(),
                        LoginName = "Test10", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Test10", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
                        DateOfBirth = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    },
                    new Users
                    {
                        Id = 11, Gid = Guid.NewGuid(),
                        LoginName = "Test11", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Test11", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
                        DateOfBirth = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    },
                    new Users
                    {
                        Id = 12, Gid = Guid.NewGuid(),
                        LoginName = "Test12", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Test12", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
                        DateOfBirth = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    }
                };
                //build 
                db.Apps.AddRange(apps);
                db.AppsApiCollection.AddRange(appsapicollection);
                db.RolesApps.AddRange(rolesapps);
                db.Roles.AddRange(roles);
                db.UsersRoles.AddRange(usersroles);
                db.Users.AddRange(users);
                db.SaveChanges();
            }
        }
    }
}