//------------------------------------------------------------------------------
// <auto-generated>
//     Der Code wurde von einer Vorlage generiert.
//
//     Manuelle Änderungen an dieser Datei führen möglicherweise zu unerwartetem Verhalten der Anwendung.
//     Manuelle Änderungen an dieser Datei werden überschrieben, wenn der Code neu generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Patient : Person
    {
        //Leerer Konstruktor
        //MW
        public Patient() { }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Patient(int id, System.DateTime timecreate, System.DateTime timemodify, bool isactive, string prename, string surname, string phone, string email, string gender, string street, string city, string zip, DateTime dateofbirth) : base(id, timecreate, timemodify, isactive, prename, surname, phone, email, gender, street, city, zip, dateofbirth)
        {
            this.LocalCase = new HashSet<LocalCase>();
        }
    
        public string hcID { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalCase> LocalCase { get; set; }
    }
}
