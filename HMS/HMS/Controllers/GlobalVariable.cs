using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Controllers
{
    // Author: Ming
    public class GlobalVariable
    {
        public enum Role
        {
            Unknown,

            Admin,
            Arzt,
            Schwester,
            Reinigungspersonal,
            Therapeut
        }

        public static Role currentRole = Role.Unknown;
    }
}