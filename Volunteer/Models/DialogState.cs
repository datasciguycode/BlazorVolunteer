namespace Volunteer.Models
{
    public class DialogState
    {
        public bool IsVisible { get; set; } = false;
        public bool IsEditMode { get; set; } = false;
        public User CurrentUser { get; set; } = new User();
        public string ActiveTabId { get; set; } = "users";
        public string ErrorMessage { get; set; } = string.Empty;

        public void Reset()
        {
            IsVisible = false;
            IsEditMode = false;
            CurrentUser = new User();
            ActiveTabId = "users";
            ErrorMessage = string.Empty;
        }

        public void ShowForAdd()
        {
            IsVisible = true;
            IsEditMode = false;
            CurrentUser = new User { StatusId = 1 }; // Set default status to 1
            ActiveTabId = "users";
            ErrorMessage = string.Empty;
        }

        public void ShowForEdit(User user)
        {
            IsVisible = true;
            IsEditMode = true;
            ActiveTabId = "users";
            ErrorMessage = string.Empty;
            CurrentUser = new User
            {
                UserId = user.UserId,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Phone = user.Phone,
                Email = user.Email,
                Address1 = user.Address1,
                PrecinctNumber = user.PrecinctNumber,
                GroupNumber = user.GroupNumber,
                DistrictNumber = user.DistrictNumber,
                SourceId = user.SourceId,
                StatusId = user.StatusId
            };
        }
    }
}
