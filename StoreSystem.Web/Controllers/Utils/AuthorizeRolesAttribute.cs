using Microsoft.AspNetCore.Authorization;
using StoreSystem.Data;

namespace StoreSystem.Web.Utils
{
    public class AuthorizeRolesAttribute : AuthorizeAttribute
    {
        public AuthorizeRolesAttribute(params string[] roles) : base()
        {
            Roles = string.Join(",", roles);
        }
    }
    
}
