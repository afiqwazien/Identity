// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Linq;
using System.Threading.Tasks;

namespace Microsoft.AspNetCore.Identity.UI.Services
{
    public abstract class ExternalLoginsServices
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
