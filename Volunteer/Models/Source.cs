using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Volunteer.Models;

[Index("Name", Name = "IX_Source_Name_Unique", IsUnique = true)]
public partial class Source
{
    [Key]
    public int SourceId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateModified { get; set; }

    [InverseProperty("Source")]
    public virtual ICollection<User> User { get; set; } = new List<User>();
}
