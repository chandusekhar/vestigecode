using System;

namespace WSS.User.Service
{
    // ReSharper disable once InconsistentNaming
    public class LDAPUser
    {
        //SystemGUID for this LDAPUser stored in ObjectGUID
        public Guid Guid { get; set; }

        //Email Address is stored in UId
        public string UId { get; set; }
    }
}