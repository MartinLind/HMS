//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace HMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class User : Person
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public User()
        {
            this.LocalCase = new HashSet<LocalCase>();
        }
    
        public string password { get; set; }
        public string rolename { get; set; }
        public bool accessright1 { get; set; }
        public bool accessright2 { get; set; }
        public bool accessright3 { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<LocalCase> LocalCase { get; set; }
    }
}
