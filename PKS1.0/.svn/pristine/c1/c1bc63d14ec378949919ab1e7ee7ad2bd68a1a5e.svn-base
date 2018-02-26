namespace PKS.DbModels
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("USERAUTHSESSIONS")]
    public partial class USERAUTHSESSIONS
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        [Key]
        [Column(Order = 0)]
        [StringLength(32)]
        [Required]
        public string SESSIONKEY { get; set; }

        [Required]
        [Column(Order = 1)]
        [StringLength(32)]
        public string APPKEY { get; set; }

        [StringLength(50)]
        public string USERNAME { get; set; }

        [Required]
        [Column(Order = 2)]
        [StringLength(30)]
        public string IPADDRESS { get; set; }

        [Required]
        [Column(Order = 3)]
        public DateTime INVALIDTIME { get; set; }

        [Required]
        [Column(Order = 4)]
        public DateTime CREATETIME { get; set; }
    }
}
