using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;

namespace Microsoft.AspNetCore.Identity.UI.Services
{
    internal abstract class ExternalLoginsServices
    {
        public abstract Task<bool> HasExternalLoginsAsync();
    }

    internal class ExternalLoginServices<TUser> : ExternalLoginsServices where TUser: class
    {
        private readonly SignInManager<TUser> _signInManager;
        private bool? _hasExternalLogins;

        public ExternalLoginServices(SignInManager<TUser> signInManager)
        {
            _signInManager = signInManager;
        }

        public override async Task<bool> HasExternalLoginsAsync()
        {
            if(_hasExternalLogins.HasValue)
            {
                return _hasExternalLogins.Value;
            }
            var schemes = await _signInManager.GetExternalAuthenticationSchemesAsync();
            _hasExternalLogins = schemes.Any();

            return _hasExternalLogins.Value;
        }
    }
}
