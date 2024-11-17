using APIWeb1.Dtos.Companys;
using APIWeb1.Dtos.SkillDtos;
using APIWeb1.Models;

namespace APIWeb1.Mappers
{
    public static class SkillMapper
    {
        public static SkillDto ToSkillDto(this Skill skillModel)
        {
            return new SkillDto
            {
                Name = skillModel.Name
            };
        }
    }
}
