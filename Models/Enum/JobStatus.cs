using System.ComponentModel;

namespace APIWeb1.Models.Enum
{
    public enum JobStatus
    {
        [Description("Pending")]
        Pending,
        [Description("Approved")]
        Approved,
        [Description("Rejected")]
        Rejected
    }
}
