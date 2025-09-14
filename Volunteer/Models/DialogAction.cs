namespace Volunteer.Models
{
    public enum DialogActionType
    {
        Save,
        Cancel,
        DeleteSkill,
        DeleteInterest
    }

    public class DialogAction
    {
        public DialogActionType Type { get; set; }
        public object? Data { get; set; }

        public static DialogAction Save() => new() { Type = DialogActionType.Save };

        public static DialogAction Cancel() => new() { Type = DialogActionType.Cancel };

        public static DialogAction DeleteSkill(int userSkillId) => new()
        {
            Type = DialogActionType.DeleteSkill,
            Data = userSkillId
        };

        public static DialogAction DeleteInterest(int userInterestId) => new()
        {
            Type = DialogActionType.DeleteInterest,
            Data = userInterestId
        };
    }
}
