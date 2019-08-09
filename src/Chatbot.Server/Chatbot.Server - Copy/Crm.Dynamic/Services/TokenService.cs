using Crm.Dynamics.Models;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Threading.Tasks;

namespace Crm.Dynamics.Services
{
    public class TokenService
    {
        public AuthenticationResult GetAccessToken(CrmSession session)
        {
            try
            {
                var clientCredential = new ClientCredential(session.ApplicationId, session.ClientSecret);
                var authenticationContext = new AuthenticationContext($"{session.AadInstanceUrl}/{session.TenantId}");
                var authenticationResult = authenticationContext.AcquireTokenAsync(session.OrganizationUrl, clientCredential).Result;
                session.AuthenticationResult = authenticationResult;
                return authenticationResult;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

    }
}
