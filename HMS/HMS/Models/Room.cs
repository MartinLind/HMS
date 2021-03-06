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

    public partial class Room : Object
    {
        /// <summary>
        /// @Author Yunus Koc, David Bismor
        /// </summary>
        [Required(ErrorMessage = "Bitte eine Raumnummer eintragen.")]
        [Display(Name = "Raumnummer")]
        public string number { get; set; }

        [Required(ErrorMessage = "Bitte Bettenanzahl angeben.")]
        [Display(Name = "Platz")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Überprüfen Sie Ihre Eingabe!")]
        public string space { get; set; }

        [Required(ErrorMessage = "Bitte verfügbare Betten angeben.")]
        [Display(Name = "Freie Betten")]
        [RegularExpression(@"^\d+$", ErrorMessage = "Überprüfen Sie Ihre Eingabe!")]
        public string vacancy { get; set; }

        [Required(ErrorMessage = "Bitte Raumtypen angeben.")]
        [Display(Name = "Raumtyp")]
        [RegularExpression(@"\bOP\b|\bUntersuchungsraum\b|\bWarteraum\b|\bBehandlungsraum\b|\bPatientenzimmer\b", ErrorMessage = "Bitte tätigen Sie eine Eingabe!")]
        public string type { get; set; }

        //Leerer Konstruktor
        //MW
        public Room()
        {
            this.LocalCase = new HashSet<LocalCase>();

        }


        public Room(int id, System.DateTime timecreate, System.DateTime timemodify, bool isactive, string number, string space, string vacancy, string type) : base(id, timecreate, timemodify, isactive)
        {
            this.LocalCase = new HashSet<LocalCase>();
            this.number = number;
            this.space = space;
            this.vacancy = vacancy;
            this.type = type;

            Exception roomTypeException = new Exception("Ungültiger Raumtyp!");

            if (!((type == "OP") || (type == "Warteraum") || (type == "Behandlungsraum") || (type == "Patientenzimmer") || (type == "Untersuchungsraum")))
            {
                throw roomTypeException;
            }
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalCase> LocalCase { get; set; }
    }
}
