using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Crm.Dynamics.Models
{
    public class CrmSession
    {
        public static CrmSession CreateDefault(bool isInternal = false) => new CrmSession()
        {
            ApplicationId = "bece060c-29b8-442c-a3e3-03af050f32d2",
            ClientSecret = "yE81S:sA5-zpgPDo*B7VFceE/DT/=d-9",
            AadInstanceUrl = "https://login.microsoftonline.com",
            TenantId = "37378be5-7862-4470-87b2-eb0edbe89793",
            OrganizationUrl = "https://saxohack.crm11.dynamics.com/",
            IsInternal = isInternal
        };

        public string ApplicationId { get; set; }

        public string ClientSecret { get; set; }

        public string AadInstanceUrl { get; set; }

        public string TenantId { get; set; }

        public string OrganizationUrl { get; set; }

        public AuthenticationResult AuthenticationResult { get; set; }

        public bool IsInternal { get; set; }
    }
}
