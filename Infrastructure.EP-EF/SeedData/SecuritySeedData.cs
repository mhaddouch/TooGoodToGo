using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

namespace Infrastructure.EP_EF.SeedData
{
    public class SecuritySeedData 
    {
        private readonly UserManager<IdentityUser> _userManager;

        public SecuritySeedData(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

        }
        public async Task EnsurePopulated()
        {

        }

    }
}
