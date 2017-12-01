namespace Digital_Planner.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Category
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Category()
        {
            Events = new HashSet<Event>();
        }

        //PK
        [Key]
        public int CategoryID { get; set; }

        //Attributes
        [Required]
        public string Description { get; set; }

        //[StringLength(128)]
        //FK
        [ForeignKey("User")]
        public string UserID { get; set; }

        //Navigation Properties
        public virtual ApplicationUser User { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Event> Events { get; set; }
    }
}
