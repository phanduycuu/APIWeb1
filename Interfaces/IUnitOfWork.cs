namespace APIWeb1.Interfaces
{
    public interface IUnitOfWork
    {
        ICompanyRepository CompanyRepo { get; }
        IJobRepository JobRepo { get; }
        IJobSkillRepository JobSkillRepo { get; }
        ISkillRepository SkillRepo { get; }
        IApplicationRepository ApplicationRepo { get; }
    }
}
