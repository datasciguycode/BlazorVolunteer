using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Volunteer.Models;

public partial class UserInterest
{
    [Key]
    public int UserInterestId { get; set; }

    public int UserId { get; set; }

    public int InterestId { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateCreated { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime DateModified { get; set; }

    [ForeignKey("InterestId")]
    [InverseProperty("UserInterest")]
    public virtual Interest Interest { get; set; } = null!;

    [ForeignKey("UserId")]
    [InverseProperty("UserInterest")]
    public virtual User User { get; set; } = null!;
}
