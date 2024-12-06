﻿using APIWeb1.Dtos.Admin;
using APIWeb1.Dtos.AppUsers;
using APIWeb1.Dtos.Job;
using APIWeb1.Helpers;
using APIWeb1.Models;

namespace APIWeb1.Interfaces
{
    public interface IAccoutAdminRepository
    {
        Task<List<AppUser>> GetAllAsync(string role);
        Task<bool> UpdateAsync(string userId,int status);
        Task<PaginationGetAllAccount> GetAllAccount(AdminAccountQuery query);
        Task<PaginationGetAllAccount> GetUserOrEmployer(AdminAccountQuery query, string role);
        Task<AdminAccountUser> GetById(string Id);
        Task<PaginationGetAllCompany> GetAllCompany(CompanyQueryObj query);
        Task<PaginationGetAllBlog> GetAllBlog(BlogQueryObject query);
        Task<PaginationGetAllJob> GetAllJob(JobQueryObject query);
        Task<PaginationGetAllSkill> GetAllSkill(SkillQuery query);
        Task<JobDto> GetJobById(int JobId);
    }
}
