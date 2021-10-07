namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Order")]
    public partial class Order
    {
        public long ID { get; set; }

        public DateTime? CreatedDate { get; set; }
        [StringLength(50)]
        public string Customer { get; set; }

        [StringLength(50)]
        public string ShipName { get; set; }

        [StringLength(10)]
        public string ShipPhone { get; set; }

        [StringLength(250)]
        public string ShipAddress { get; set; }

        [StringLength(50)]
        public string ShipEmail { get; set; }

        public int? Status { get; set; }
    }
}
