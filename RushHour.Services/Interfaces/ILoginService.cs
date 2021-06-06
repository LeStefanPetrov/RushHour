using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RushHour.Services.Interfaces
{
    public interface ILoginService
    {
        Task<string> GenerateJwtTokenAsync(string email, string password);
    }
}
