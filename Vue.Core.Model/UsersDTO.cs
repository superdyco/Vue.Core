using System;
using Vue.Core.Data;


namespace Vue.Core.Model
{
    public class UsersList
    {
        public Guid Gid { get; set; }
        public string LoginName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Enums.Gender Gender { get; set; }
        public DateTime? DateOfBirth { get; set; }
        public DateTime CreatedAt{get;set;}
    }
    
    public class LoginModel
    {
        public string Loginname { get; set; }
        public string Password { get; set; }
    }
}