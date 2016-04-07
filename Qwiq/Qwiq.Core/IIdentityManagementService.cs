﻿using System.Collections.Generic;

namespace Microsoft.IE.Qwiq
{
    public interface IIdentityManagementService
    {
        IEnumerable<ITeamFoundationIdentity> ReadIdentities(ICollection<IIdentityDescriptor> descriptors);
        IEnumerable<ITeamFoundationIdentity> ReadIdentities(IdentitySearchFactor searchFactor,
            string[] searchFactorValues);

        IIdentityDescriptor CreateIdentityDescriptor(string identityType, string identifier);
    }
}
