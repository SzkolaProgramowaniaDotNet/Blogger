using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public static class UserRoles
    {
        public const string Admin = "Admin";
        public const string User = "User";
        public const string AdminOrUser = Admin + "," + User;
    }
}
