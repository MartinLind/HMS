  using System;			
  using System.Collections.Generic;			
  using System.Linq;				
  using System.Text;				
  using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;



namespace HMS.Models
{


    public partial class Person : Object
    {
        [Required(ErrorMessage = "Bitte den Vornamen eintragen")]
        [Display(Name = "Vorname")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage ="Bitte den Vornamen auf Fehler überprüfen")]
        public string prename { get; set; }

        [Required(ErrorMessage = "Bitte den Nachnamen eintragen")]
        [Display(Name = "Nachname")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Bitte den Nachnamen auf Fehler überprüfen")]
        public string surname { get; set; }

        [Required(ErrorMessage = "Bitte Haustelefon eingeben")]
        [Display(Name = "Haustelefon")]
        [RegularExpression(@"^\d{4,5}-\d{4,}$", ErrorMessage = "Bitte überprüfen Sie die Nummer auf folgendes Format: Vorwahl-Telefonnummer")]
        public string phone { get; set; }

        [Required(ErrorMessage = "Bitte die E-Mail-Adresse eingeben")]
        [Display(Name = "E-Mail")]
        [RegularExpression(@"^([0-9a-zA-Z]"+ @"([\+\-_\.][0-9a-zA-Z]+)*"+ @")+" + @"@(([0-9a-zA-Z][-\w]*[0-9a-zA-Z]*\.)+[a-zA-Z0-9]{2,17})$", ErrorMessage = "Überprüfen Sie die E-Mail auf ihre Richtigkeit!")]
        public string email { get; set; }

        [Required(ErrorMessage = "Bitte das Geschlecht eintragen")]
        [Display(Name = "Geschlecht")]
        [RegularExpression(@"\bweiblich\b|\bmännlich\b", ErrorMessage = "weiblich oder männlich! Bitte auf Korrektheit überprüfen.")]
        public string gender { get; set; }

        [Required(ErrorMessage = "Bitte die Straße und die Hausnummer eintragen")]
        [RegularExpression(@"^(.+)\s(\S+)$", ErrorMessage = "Bitte überprüfen Sie die Straße und die Hausnummer auf ihre Richtigkeit!")]
        [Display(Name = "Straße")]        
        public string street { get; set; }

        [Required(ErrorMessage = "Bitte die Stadt eintragen")]
        [Display(Name = "Stadt")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Überprüfen Sie die Stadt auf ihre Richtigkeit")]
        public string city { get; set; }

        [Required(ErrorMessage = "Bitte geben Sie eine Postleitzahl ein")]
        [Display(Name = "Postleitzahl")]
        [RegularExpression(@"[0-9]{5}", ErrorMessage = "Ungültige Postleitzahl! Bitte überprüfen Sie ihre Eingabe!")]
        public string zip { get; set; }

        [Required(ErrorMessage = "Bitte tragen Sie das Geburtsdatum ein")]
        [Display(Name = "Geburtstag")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime dateofbirth { get; set; }



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


        public Person(int id, System.DateTime timecreate, System.DateTime timemodify, bool isactive, string prename, string surname, string phone, string email, string gender, string street, string city, string zip, System.DateTime dateofbirth) : base(id, timecreate, timemodify, isactive)
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
    public static Person addPerson(String prename, String surname, String phone, String email, String gender, String street, String city, String zip, string dateofbirth)				
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

