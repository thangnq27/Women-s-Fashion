namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("LeftSidebar")]
    public partial class LeftSidebar
    {
        public int ID { get; set; }

        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string ClassCssATag { get; set; }

        [StringLength(250)]
        public string Href { get; set; }

        [StringLength(250)]
        public string ClassCssITtag { get; set; }

        [StringLength(250)]
        public string ClassCssSpanTag { get; set; }

        public long? ChildMenu { get; set; }
    }
}
