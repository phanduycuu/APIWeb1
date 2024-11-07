using APIWeb1.Dtos.Company;
using APIWeb1.Dtos.SkillDto;
using APIWeb1.Models;

namespace APIWeb1.Mappers
{
    public static class SkillMapper
    {
        public static SkillDto ToSkillDto(this Skill skillModel)
        {
            return new SkillDto
            {
                Id = skillModel.Id,
                Name = skillModel.Name
            };
        }
    }
}
