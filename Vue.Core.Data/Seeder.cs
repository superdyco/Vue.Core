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
                        Gid = Guid.NewGuid(),Delete = true, Write = true, Read = true, Print = true,
                        AppsId = 3, RolesId = 1, 
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new RolesApps()
                    {
                        Gid = Guid.NewGuid(), Delete = true, Write = true, Read = true, Print = true,
                        AppsId = 4,RolesId = 1,
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new RolesApps()
                    {
                        Gid = Guid.NewGuid(), Delete = true, Write = true, Read = true, Print = true,
                        AppsId = 5,RolesId = 2,
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    }
                };

                List<Apps> apps = new List<Apps>()
                {
                    new Apps()
                    {
                        Gid = Guid.NewGuid(), AppName = "home",RouteName = "home",IconClass = "home",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new Apps()
                    {
                        Gid = Guid.NewGuid(), AppName = "Setting",IconClass = "keyboard_arrow_up",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new Apps()
                    {
                       Gid = Guid.NewGuid(), AppName = "Users", RouteName = "userslist",IconClass = "list",ParentId = 2,
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new Apps()
                    {
                        Gid = Guid.NewGuid(), AppName = "News", RouteName = "newslist", IconClass = "message",ParentId = 2,
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new Apps()
                    {
                        Gid = Guid.NewGuid(), AppName = "about",RouteName = "about",IconClass = "face",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new Apps()
                    {
                        Gid = Guid.NewGuid(), AppName = "logout",RouteName = "logout",IconClass = "close",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    }
                };

                List<AppsApiCollection> appsapicollection = new List<AppsApiCollection>()
                {
                    new AppsApiCollection()
                    {
                        Gid = Guid.NewGuid(), AppsId = 3, RoutePath = "api/Users/GetAll",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new AppsApiCollection()
                    {
                        Gid = Guid.NewGuid(), AppsId = 3, RoutePath = "api/Users/GetOne",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new AppsApiCollection()
                    {
                        Gid = Guid.NewGuid(), AppsId = 3, RoutePath = "api/Users/Update",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new AppsApiCollection()
                    {
                       Gid = Guid.NewGuid(), AppsId = 3, RoutePath = "api/Users/Create",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new AppsApiCollection()
                    {
                        Gid = Guid.NewGuid(), AppsId = 4, RoutePath = "api/News/GetAll",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new AppsApiCollection()
                    {
                       Gid = Guid.NewGuid(), AppsId = 4, RoutePath = "api/News/GetOne",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new AppsApiCollection()
                    {
                        Gid = Guid.NewGuid(), AppsId = 4, RoutePath = "api/News/Update",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    },
                    new AppsApiCollection()
                    {
                        Gid = Guid.NewGuid(), AppsId = 4, RoutePath = "api/News/Create",
                        CreatedAt = DateTime.Now, UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin"
                    }
                };


                List<Roles> roles = new List<Roles>()
                {
                    new Roles()
                    {
                        Gid = Guid.NewGuid(), RoleName = "Admin", CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", Lock = true,
                        IsDeleted = false
                    },
                    new Roles()
                    {
                        Gid = Guid.NewGuid(), RoleName = "Member", CreatedAt = DateTime.Now,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", Lock = true,
                        IsDeleted = false
                    }
                };

                List<UsersRoles> usersroles = new List<UsersRoles>()
                {
                    new UsersRoles()
                    {
                        Gid = Guid.NewGuid(), CreatedAt = DateTime.Now, RolesId = 1, UsersId = 1,
                        UpdatedAt = DateTime.Now, CreatedBy = "Admin", UpdatedBy = "Admin", IsDeleted = false
                    }
                };

                byte[] passwordHash, passwordSalt;
                Security.CreatePasswordHash("p12345", out passwordHash, out passwordSalt);
                List<Users> users = new List<Users>()
                {
                    new Users
                    {
                        Gid = Guid.NewGuid(),
                        LoginName = "admin", PasswordHash = passwordHash, PasswordSalt = passwordSalt,
                        FirstName = "Dyco", LastName = " Demo", Email = "", Gender = Enums.Gender.Male,
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