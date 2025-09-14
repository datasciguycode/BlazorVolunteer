using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Volunteer.Models;

[Index("Name", Name = "IX_Interest_Name_Unique", IsUnique = true)]
public partial class Interest
{
    [Key]
    public int InterestId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string Name { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateModified { get; set; }

    [InverseProperty("Interest")]
    public virtual ICollection<UserInterest> UserInterest { get; set; } = new List<UserInterest>();
}
