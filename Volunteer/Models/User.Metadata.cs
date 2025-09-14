using System.ComponentModel.DataAnnotations;

namespace Volunteer.Models
{
    [MetadataType(typeof(UserMetadata))]
    public partial class User
    {
        // This partial class allows us to add validation attributes
        // without modifying the generated User.cs file
    }

    public class UserMetadata
    {
        [Required(ErrorMessage = "First name is required")]
        [StringLength(50, ErrorMessage = "First name cannot exceed 50 characters")]
        public required string FirstName { get; set; }

        [Required(ErrorMessage = "Last name is required")]
        [StringLength(50, ErrorMessage = "Last name cannot exceed 50 characters")]
        public required string LastName { get; set; }

        [Required(ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public required string Email { get; set; }

        [StringLength(10, ErrorMessage = "Phone number cannot exceed 10 characters")]
        [RegularExpression(@"^\d{10}$", ErrorMessage = "Phone number must be exactly 10 digits")]
        public string? Phone { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters")]
        public string? Address { get; set; }

        [StringLength(50, ErrorMessage = "City cannot exceed 50 characters")]
        public string? City { get; set; }

        [StringLength(2, ErrorMessage = "State cannot exceed 2 characters")]
        public string? State { get; set; }

        [StringLength(10, ErrorMessage = "ZIP code cannot exceed 10 characters")]
        public string? Zip { get; set; }
    }
}
