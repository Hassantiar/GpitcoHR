using Base.DTOs;
using Base.Entites;
using Base.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serverlibrary.Repository.Contact
{
    public interface IUserAccount
    {
        Task<GeneralRespons> CreateAsync(Reguster user);
        Task<LoginRespons> SingInAsync(Login user);
        Task<LoginRespons> RefrashInfoAsync(RefreshToken Token);
    }
}
