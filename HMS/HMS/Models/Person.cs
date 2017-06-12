  using System;			
  using System.Collections.Generic;			
  using System.Linq;				
  using System.Text;				
  using System.Threading.Tasks;		 		
  using System.Text.RegularExpressions;



namespace HMS.Models
{


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
        /// Leerer Konstruktor		
        /// Für den Fall das eine Person ohne Attribute angelegt wird		
        /// Nur theoretisch, werden wir vermutlich nie brauchen, aber ohne den Kontruktor startet die GUI nicht		
        /// </summary>		
        public Person() { }
         public Person(int id, System.DateTime timecreate, System.DateTime timemodify, bool isactive) : base(id, timecreate, timemodify, isactive)
        {

        }

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


        public Person(int id, System.DateTime timecreate, System.DateTime timemodify, bool isactive, string prename, string surname, string phone, string email, string gender, string street, string city, string zip, DateTime dateofbirth) : base(id, timecreate, timemodify, isactive)
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

            if (!((gender == "weiblich") ||  (gender == "männlich")))
            {
                throw checkGenderException;
            }

            // Alternativlösung				
            //if (plz.ToString().Length > 5 || plz.ToString().Length < 5) throw myException;				

            if (!Regex.IsMatch(zip, @"[0 9]{5}")) throw checkZipException;

            if (!Regex.IsMatch(phone, @"^\d{4,5} \d{4,}$")) throw checkPhoneException;

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
    }
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

    /*		
        /// <summary>		
        /// @author: db & yk		
        /// überprüft, ob ein user mit dem vornamen und nachnamen existiert		
        /// WICHTIG!!!   es muss noch angepasst werden mit der ID		
        /// </summary>		
        /// <param name="vorname"></param>		
        /// <param name="nachname"></param>		
        /// <returns></returns>		
    public Boolean checkExistence(String prename, String surname)		
    {		
        foreach (var item in persons)		
        {		
            if (item.getPrename().Equals(prename) && item.getSurname().Equals(surname))		
                return true;		
        }		
        return false;		
    }		      }
    */
    /*		
    /// <summary>		
    /// zeigt den entsprechenden User an		
    /// auch hier wieder  > wichtig: anpassen!		
    /// @author: yk & db 		
    /// </summary>		
    /// <param name="prename"></param>		
    /// <param name="surname"></param>		
    /// <returns></returns>		
public Person showPerson(String prename, String surname)		
{		
    foreach (var item in persons)		
    {		
        if (item.getPrename().Equals(prename) && item.getSurname().Equals(surname))		
            return item;		
    }		
    throw new Exception("Patient existiert nicht! Überprüfe auf Richtigkeit!");		
}		
*/

    /*		
    /// <summary>		
    /// @author yk & db		
    /// Methode zur Bearbeitung des Patienten, allerdings wird die Methode so wie sie hier gerade ist, nicht funktionieren		
    /// Die Methode muss angepasst werden!		
    /// </summary>		
    /// <param name="prename"></param>		
    /// <param name="surname"></param>		
    /// <param name="phone"></param>		
    /// <param name="email"></param>		
    /// <param name="gender"></param>		
    /// <param name="street"></param>		
    /// <param name="city"></param>		
    /// <param name="zip"></param>		
public void changePerson(String prename, String surname, String phone, String email, String gender, String street, String city, String zip)		
{		
    foreach (Person personObject in persons)		
    {		
        if (personObject.getPrename().Equals(prename))		
        {		
            personObject.setSurname(surname);		
            personObject.setPhone(phone);		
            personObject.setEmail(email);		
            personObject.setGender(gender);		
            personObject.setStreet(street);		
            personObject.setCity(city);		
            personObject.setZip(zip);		
            return; //hier ist die Methode zu Ende		
        }		
    }		
    throw new Exception("Person nicht gefunden"); //Exeption wird nur erwischt, wenn return nicht passiert		
}		
*/

