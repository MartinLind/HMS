using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HMS.Controllers
{
    public class GlobalVariable
    {
        public enum Role
        {
            Admin,
            Arzt,
            Schwester,
            Reinigungspersonal
            //Therapeut
        }

        public static Role currentRole = Role.Admin;
    }
}