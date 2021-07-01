﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Identity
{
    public class UserService
    {
        public bool IsUserEmailConfirmed(ApplicationUser applicationUser)
            => applicationUser.EmailConfirmed ? true : false;
    }
}
