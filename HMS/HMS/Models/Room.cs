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
    
    public partial class Room : Object
    {
        public string number { get; set; }
        public string space { get; set; }
        public string vacancy { get; set; }
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
        }
    
        public virtual LocalCase LocalCase { get; set; }
    }
}
