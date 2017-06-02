
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace HMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Person : Object
    {
        public string prename { get; set; }
        public string surname { get; set; }
        public string phone { get; set; }
        public string email { get; set; }
        public string gender { get; set; }
        public string street { get; set; }
        public string city { get; set; }
        public string zip { get; set; }
        public System.DateTime dateofbirth { get; set; }


        /// <summary>
        /// Konstruktor für Person inkl. Validierung der Attribute
        /// @Author: Yunus
        /// </summary>
        /// <param name="prename"></param>
        /// <param name="surname"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="gender"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="zip"></param>
        /// <param name="dateofbirth"></param>
        public Person(string prename, string surname, string phone, string email, string gender, string street, string city, string zip, DateTime dateofbirth)
        {

            Exception checkNameException = new Exception("ungültiger Name");
            Exception checkZipException = new Exception("ungültige Postleitzahl");
            Exception checkGenderException = new Exception("ungültiges Geschlecht");
            Exception checkPhoneException = new Exception("ungültige Telefonnummer");
            Exception checkStreetException = new Exception("ungültige Adresse");
            Exception checkCityException = new Exception("ungültige Stadt");
            Exception checkEmailException = new Exception("ungültige Mail");

            if (!Regex.IsMatch(prename, @"^[\p{L}]+$")) throw checkNameException;

            if (!Regex.IsMatch(surname, @"^[\p{L}]+$")) throw checkNameException;

            if (!((gender == "weiblich") || (gender == "w") || (gender == "m") || (gender == "männlich")))
            {
                throw checkGenderException;
            }

            // Alternativlösung
            //if (plz.ToString().Length > 5 || plz.ToString().Length < 5) throw myException;

            if (!Regex.IsMatch(zip, @"[0-9]{5}")) throw checkZipException;

            if (!Regex.IsMatch(phone, @"^\d{4,5}-\d{4,}$")) throw checkPhoneException;

            if (!Regex.IsMatch(street, @"^(.+)\s(\S+)$")) throw checkStreetException;

            if (!Regex.IsMatch(city, @"^[\p{L}]+$")) throw checkCityException;

            //email validierung folgt noch

            this.prename = prename;
            this.surname = surname;
            this.phone = phone;
            this.email = email;
            this.gender = gender;
            this.street = street;
            this.city = city;
            this.zip = zip;
            this.dateofbirth = dateofbirth;
        }

        /**

        /// <summary>
        /// Erzeuge eine neue Person
        /// </summary>
        /// <param name="prename"></param>
        /// <param name="surname"></param>
        /// <param name="phone"></param>
        /// <param name="email"></param>
        /// <param name="gender"></param>
        /// <param name="street"></param>
        /// <param name="city"></param>
        /// <param name="zip"></param>
        /// <param name="dateofbirth"></param>
        /// <returns></returns>
        public static Person addPerson(String prename, String surname, String phone, String email, String gender, String street, String city, String zip, DateTime dateofbirth)
        {
            return new Person(prename, surname, phone, email, gender, street, city, zip, dateofbirth);
        }
    **/

    }
}
