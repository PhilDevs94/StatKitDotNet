using Microsoft.AspNetCore.Identity;
using System.ComponentModel;

namespace DotNetCoreStartKit.Core.Model
{
    public class ApplicationUser : IdentityUser
    {
        [DefaultValue(false)]
        public bool IsBanned { get; set; }
    }
}
