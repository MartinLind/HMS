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
    
        public virtual LocalCase LocalCase { get; set; }
    }
}
