using System.ComponentModel;

namespace Model.EF
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("User")]
    public partial class User
    {
        public long ID { get; set; }

        [StringLength(50)]
        [DisplayName("Tài khoản")]
        public string UserName { get; set; }

        [StringLength(50)]
        [DisplayName("Mật khẩu")]
        public string Password { get; set; }

        [StringLength(50)]
        [DisplayName("Full Name")]
        public string Name { get; set; }

        [StringLength(50)]
        [DisplayName("Địa chỉ")]
        public string Address { get; set; }

        [StringLength(50)]
        public string Email { get; set; }

        [StringLength(50)]
        public string Phone { get; set; }

        public DateTime? CreateDate { get; set; }

        [StringLength(50)]
        public string CreateBy { get; set; }

        public DateTime? ModifyDate { get; set; }

        [StringLength(50)]
        public string ModifyBy { get; set; }

        [DisplayName("Trạng thái")]
        public bool Status { get; set; }
    }
}
