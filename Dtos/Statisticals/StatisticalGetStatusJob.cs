namespace APIWeb1.Dtos.Statisticals
{
    public class StatisticalGetStatusJob
    {
        public int Approval { get; set; } =0;
        public int Pending { get; set; } = 0;
        public int Expired { get; set; } = 0;
    }
}
