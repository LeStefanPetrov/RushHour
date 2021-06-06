using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RushHour.Contracts
{
    public class RoleKeys
    {
        public const string UserRole = "User";
        public const string AdministratorRole = "Administrator";
        public const string AdministratorOrUser = UserRole + "," + AdministratorRole;
    }
}
