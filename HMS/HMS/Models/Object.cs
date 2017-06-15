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
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    public partial class Object
    {
        public int Id { get; set; }
     
        [Required(ErrorMessage = "Bitte geben Sie ein gültiges Datum ein.")]
        [Display(Name = "Erstellungsdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
 
        public System.DateTime timecreate { get; set; }
        
        [Required(ErrorMessage = "Bitte geben Sie ein gültiges Datum ein.")]
        [Display(Name = "Veränderungsdatum")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime timemodify { get; set; }
        
        [Display(Name = "Status")]
        public bool isactive { get; set; }

        public Object()
        {

        }

        public Object(int id, System.DateTime timecreate, System.DateTime timemodify, bool isactive)
        {
            this.Id = id;
            this.timecreate = timecreate;
            this.timemodify = timemodify;
            this.isactive = isactive;
        }
    }


}
