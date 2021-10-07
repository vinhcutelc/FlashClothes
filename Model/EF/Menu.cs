namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Menu")]
    public partial class Menu
    {
        public int ID { get; set; }

        [StringLength(50)]
        public string Text { get; set; }

        [Column(TypeName = "ntext")]
        public string Link { get; set; }

        public int? DisplayOrder { get; set; }

        [Column(TypeName = "ntext")]
        public string Target { get; set; }

        public int? Status { get; set; }

        public int? TypeID { get; set; }
    }
}
