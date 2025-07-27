using System.ComponentModel;

namespace APIWeb1.Models.Enum
{
    public enum JobLevel
    {
        [Description("Intern")]
        Intern,
        [Description("Fresher")]
        Fresher,
        [Description("Junior")]
        Junior,
        [Description("Middle")]
        Middle,
        [Description("Senior")]
        Senior,
    }
}
