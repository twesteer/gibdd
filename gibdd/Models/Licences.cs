//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан по шаблону.
//
//     Изменения, вносимые в этот файл вручную, могут привести к непредвиденной работе приложения.
//     Изменения, вносимые в этот файл вручную, будут перезаписаны при повторном создании кода.
// </auto-generated>
//------------------------------------------------------------------------------

namespace gibdd.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Licences
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Licences()
        {
            this.ChangeHistory = new HashSet<ChangeHistory>();
            this.Fines = new HashSet<Fines>();
            this.Categories = new HashSet<Categories>();
        }
    
        public int id { get; set; }
        public System.DateTime LicenceDate { get; set; }
        public Nullable<System.DateTime> ExpireDate { get; set; }
        public string LicenceSeries { get; set; }
        public int idDriver { get; set; }
        public string LicenceNumber { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ChangeHistory> ChangeHistory { get; set; }
        public virtual Drivers Drivers { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Fines> Fines { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Categories> Categories { get; set; }
    }
}
