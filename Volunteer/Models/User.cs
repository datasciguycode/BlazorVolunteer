using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Volunteer.Models;

[Index("Email", Name = "IX_User_Email_Unique", IsUnique = true)]
[Index("Phone", Name = "IX_User_Phone_Unique", IsUnique = true)]
[Index("SourceId", Name = "IX_User_SourceId")]
[Index("StatusId", Name = "IX_User_StatusId")]
[Index("UserId", Name = "IX_User_UserId")]
public partial class User
{
    [Key]
    public int UserId { get; set; }

    public int SourceId { get; set; }

    public int StatusId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string FirstName { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string LastName { get; set; } = null!;

    [StringLength(14)]
    [Unicode(false)]
    public string Phone { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Email { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string Address1 { get; set; } = null!;

    public short PrecinctNumber { get; set; }

    public short GroupNumber { get; set; }

    public short DistrictNumber { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateModified { get; set; }

    [ForeignKey("SourceId")]
    [InverseProperty("User")]
    public virtual Source Source { get; set; } = null!;

    [InverseProperty("User")]
    public virtual ICollection<UserInterest> UserInterest { get; set; } = new List<UserInterest>();

    [InverseProperty("User")]
    public virtual ICollection<UserSkill> UserSkill { get; set; } = new List<UserSkill>();
}
