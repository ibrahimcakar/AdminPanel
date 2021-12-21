using AdminPanel.Data.Model;
using AdminPanelWeb.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminPanelWeb.Mappers
{
    public class AdminMapperConfiguration:Profile
    {
        public AdminMapperConfiguration()
        {
            CreateMap<User, UserDTO>();
            CreateMap<UserDTO, User>();
        }

    }
}
