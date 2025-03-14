using EcoEarthPOC.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EcoEarthPOC.Components.Services
{
    public interface IAppService
    {
        public Task<string> AuthenticateUser(LoginModel loginModel);

        Task<(bool IsSuccess, string ErrorMessage)> RegisterUser(RegistrationModel registerModel);

    }
}
