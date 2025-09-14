using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Volunteer.Models;

[Index("Name", Name = "IX_Name_Unique", IsUnique = true)]
public partial class Skill
{
    [Key]
    public int SkillId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateModified { get; set; }

    [InverseProperty("Skill")]
    public virtual ICollection<UserSkill> UserSkill { get; set; } = new List<UserSkill>();
}
