using System.ComponentModel;

namespace APIWeb1.Models.Enum
{
    public enum JobType
    {
        [Description("In Office")]
        InOffice,
        [Description("Remote")]
        Remote,
        [Description("Hybrid")]
        Hybrid,
        [Description("Oversea")]
        Oversea
    }
}
