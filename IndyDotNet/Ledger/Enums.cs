using System;
namespace IndyDotNet.Ledger
{
    /// <summary>
    /// TODO: eval best approach to converting to string/back
    /// https://bytefish.de/blog/enums_json_net/
    /// </summary>
    public enum NymRoles
    {
        /// <summary>
        /// null string
        /// </summary>
        NA,
        /// <summary>
        /// "STEWARD"
        /// </summary>
        Steward,
        /// <summary>
        /// "TRUSTEE"
        /// </summary>
        Trustee,
        /// <summary>
        /// TRUST_ANCHOR
        /// </summary>
        TrustAnchor
    }

    public static class EnumToStringConverers
    {
        public static string AsString(this NymRoles role)
        {
            switch (role)
            {
                case NymRoles.Steward:
                    return "STEWARD";
                case NymRoles.TrustAnchor:
                    return "TRUST_ANCHOR";
                case NymRoles.Trustee:
                    return "TRUSTEE";
                default:
                    return null;
            }
        }
    }
}
