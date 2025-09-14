using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Volunteer.Models;

public partial class UserSkill
{
    [Key]
    public int UserSkillId { get; set; }

    public int UserId { get; set; }

    public int SkillId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateModified { get; set; }

    [ForeignKey("SkillId")]
    [InverseProperty("UserSkill")]
    public virtual Skill Skill { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserSkill")]
    public virtual User User { get; set; } = null!;
}
