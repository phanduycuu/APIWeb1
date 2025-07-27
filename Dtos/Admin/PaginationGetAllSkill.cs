using APIWeb1.Dtos.Job;
using APIWeb1.Dtos.SkillDtos;

namespace APIWeb1.Dtos.Admin
{
    public class PaginationGetAllSkill
    {
        public List<SkillDto> Skills { get; set; }
        public int Total { get; set; }
    }
}
