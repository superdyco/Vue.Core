using System.Collections.Generic;
using AutoMapper;
using Vue.Core.Data.Entities;

namespace Vue.Core.Model
{
    public class MappingProfile:Profile
    {
        public MappingProfile() {
            AllowNullCollections = true;
            CreateMap<Users, UsersList>();
        }
    }
}