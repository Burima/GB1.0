﻿using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
namespace GB.Membership
{
    public class EmailService : IIdentityMessageService
    {
        #region methods

        public Task SendAsync(IdentityMessage message)
        {
            // Plug in your email service here to send an email.
            return Task.FromResult(0);
        }

        #endregion
    }
}