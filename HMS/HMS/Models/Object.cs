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

    public partial class Object
    {
        public int Id { get; set; }
        public System.DateTime timecreate { get; set; }
        public System.DateTime timemodify { get; set; }
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
