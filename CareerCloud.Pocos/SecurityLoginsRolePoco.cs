﻿using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CareerCloud.Pocos
{
    [Table("Security_Logins_Roles")]
    public class SecurityLoginsRolePoco : IPoco
    {
        [Key]
        public Guid Id { get; set; }
        public Guid Login { get; set; }
        public Guid Role { get; set; }

        [Column("Time_Stamp")]
        [NotMapped]
        public Byte[] TimeStamp { get; set; }

        public virtual SecurityRolePoco SecurityRoles { get; set; }

        public virtual SecurityLoginPoco SecurityLogins { get; set; } 

    }
}
