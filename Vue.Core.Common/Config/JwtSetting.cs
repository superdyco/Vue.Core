using System;

namespace Vue.Core.Common.Config
{
    public class JwtSetting
    {
        public string Key { get;set; }
        public string Issuer { get;set; }
        public string Audience { get;set; }
        public int Expire { get;set; }
        public int LongExpire { get;set; }
    }
}