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

        [Required(ErrorMessage = "Bitte eine Raumnummer eintragen.")]
        [Display(Name = "Raumnummer")]
        public string number { get; set; }
        [Required(ErrorMessage = "Bitte Bettenanzahl angeben.")]
        [Display(Name = "Platz")]
        public string space { get; set; }
        [Required(ErrorMessage = "Bitte verfügbare Betten angeben.")]
        [Display(Name = "Freie Betten")]
        public string vacancy { get; set; }
        [Required(ErrorMessage = "Bitte Raumtypen angeben.")]
        [Display(Name = "Raumtyp")]
        [RegularExpression(@"\bOP\b|\bUntersuchungsraum\b|\bWartezimmer\b|\bBehandlungszimmer\b|\bPatientenzimmer\b", ErrorMessage = "Bitte geben Sie eins der foglenden Typen ein: OP, Untersuchungszimmer, Behandlungszimmer, Wartezimmer, Patientenzimmer")]
        public string type { get; set; }

        //Leerer Konstruktor
        //MW
        public Room() { }


        public Room(int id, System.DateTime timecreate, System.DateTime timemodify, bool isactive, string number, string space, string vacancy, string type) : base(id, timecreate, timemodify, isactive)
        {
            this.number = number;
            this.space = space;
            this.vacancy = vacancy;
            this.type = type;

            Exception roomTypeException = new Exception("Ungültiger Raumtyp!");

            if (!((type == "OP") || (type == "Wartezimmer") || (type == "Behandlungszimmer") || (type == "Patientenzimmer") || (type == "Untersuchungszimmer")))
            {
                throw roomTypeException;
            }
        }
    
        public virtual LocalCase LocalCase { get; set; }
    }
}
