using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.RegularExpressions;
using System.Web.Mvc;


namespace HMS.Models
{
    using System;
    using System.Collections.Generic;

    public partial class LocalCase : Object
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public LocalCase()
        {
            this.Patient = new HashSet<Patient>();
            this.User = new HashSet<User>();
            this.Room = new HashSet<Room>();
        }

        //Autor Yunus Koc, David Bismor
        [Required(ErrorMessage = "Bitte wählen Sie ein Datum")]
        [Display(Name = "Behandlungsstart")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime timecreate { get; set; }

        [Required(ErrorMessage = "Bitte wählen Sie ein Datum")]
        [Display(Name = "Behandlungsende")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public System.DateTime timeclosed { get; set; }

        [Required(ErrorMessage = "Bitte tätigen Sie eine Eingabe")]
        [Display(Name = "Behandlungsnummer")]
        public string casenr { get; set; }

        [Required(ErrorMessage = "Bitte tätigen Sie eine Eingabe")]
        [Display(Name = "Diagnose")]
        public string diagnosis { get; set; }

        [Required(ErrorMessage = "Bitte tätigen Sie eine Eingabe")]
        [Display(Name = "Medikation")]
        public string medication { get; set; }

        [Required(ErrorMessage = "Bitte tätigen Sie eine Eingabe")]
        [Display(Name = "Therapy")]
        public string therapy { get; set; }

        [Required(ErrorMessage = "Bitte tätigen Sie eine Eingabe")]
        [Display(Name = "Behandlungsdauer")]
        public string expectedtime { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Patient> Patient { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<User> User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Room> Room { get; set; }

    }
}
