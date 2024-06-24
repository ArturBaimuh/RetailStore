using ReatailStore.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ReatailStore.Domain.SupportData
{
    public class AuthInfo
    {
        public static string CookieName = "AuthCookie";
        public string Key { get; set; } = string.Empty;
        public User User { get; set; } = new User();

    }
}
