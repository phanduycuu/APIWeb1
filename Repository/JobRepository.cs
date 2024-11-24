using APIWeb1.Data;
using APIWeb1.Dtos.AppUsers;
using APIWeb1.Dtos.Companys;
using APIWeb1.Dtos.Job;
using APIWeb1.Dtos.SkillDtos;
using APIWeb1.Helpers;
using APIWeb1.Interfaces;
using APIWeb1.Models;
using APIWeb1.Models.Enum;
using Microsoft.EntityFrameworkCore;
using APIWeb1.Mappers;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace APIWeb1.Repository
{
    public class JobRepository : Repository<Job>, IJobRepository
    {
        private readonly ApplicationDBContext _context;
        public JobRepository(ApplicationDBContext context) : base(context)
        {
            _context = context;
        }

        public async Task<Job> AdminGetJobById(int jobId)
        {
            return await _context.Jobs.Where(u=>u.Id== jobId).FirstOrDefaultAsync();
        }

        public async Task<Job> CreateAsync(Job job)
        {
            await _context.Jobs.AddAsync(job);
            await _context.SaveChangesAsync();
            return job;
        }

        public async Task<List<JobAdminDto>> GetAdminJob()
        {
            var job = _context.Jobs.Include(a => a.Employer).ThenInclude(b => b.Company).AsQueryable();

            return await job
                .Select(job => new JobAdminDto
                {
                    Id = job.Id,
                    Title = job.Title,
                    EmployerName = job.Employer.Fullname,
                    JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                    JobType = EnumHelper.GetEnumDescription(job.JobType),
                    JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                    
                })
            .ToListAsync();
        }

        public async Task<List<GetAllJobDto>> GetAllAsync(JobQueryObject query)
        {
            var status = EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");
            var job =  _context.Jobs.Include(a => a.Employer).ThenInclude(b => b.Company).Include(job => job.JobSkills)
            .ThenInclude(jobSkill => jobSkill.Skill).Where(u=> u.JobStatus == status && u.ExpiredDate< DateTime.Now).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                job = job.Where(s => s.Title.Contains(query.Title));
            }

            if (!string.IsNullOrWhiteSpace(query.Location))
            {
                job = job.Where(s => (s.Address.Street + " " +
                          s.Address.Province + " " +
                          s.Address.Ward + " " +
                          s.Address.District)
                          .Contains(query.Location));
            }
            if(!string.IsNullOrWhiteSpace(query.JobLevel))
            {
                var level = EnumHelper.GetEnumValueFromDescription<JobLevel>(query.JobLevel);
                job = job.Where(s => s.JobLevel== level);
            }
            if (!string.IsNullOrWhiteSpace(query.JobStatus))
            {
                var Status = EnumHelper.GetEnumValueFromDescription<JobStatus>(query.JobStatus);
                job = job.Where(s => s.JobStatus == Status);
            }
            if (!string.IsNullOrWhiteSpace(query.JobType))
            {
                var Type = EnumHelper.GetEnumValueFromDescription<JobType>(query.JobType);
                job = job.Where(s => s.JobType == Type);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("CreateOn", StringComparison.OrdinalIgnoreCase))
                {
                    job = query.IsDecsending ? job.OrderByDescending(s => s.CreateOn) : job.OrderBy(s => s.CreateOn);
                }
            }
            if(query.PageSize == 0 && query.PageNumber==0) return await job
            .Select(job => new GetAllJobDto
            {
                Id = job.Id,
                Title = job.Title,
                Salary = job.Salary,
                CreateOn = job.CreateOn,
                Employer = new GetEmployer
                {
                    Id = job.Employer.Id,
                    Company = new GetCompany
                    {
                        Name = job.Employer.Company.Name,
                        logo = job.Employer.Company.Logo
                    },
                },
                JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                JobType = EnumHelper.GetEnumDescription(job.JobType),
                JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                LocationShort = job.Address.Province + ", " + job.Address.District,
                Skills = job.JobSkills.Select(js => new SkillDto
                {
                    Id = js.Skill.Id,
                    Name = js.Skill.Name
                    // Include other properties of Skill as needed

                }).ToList()
            })
        .ToListAsync();

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await job.Skip(skipNumber).Take(query.PageSize)
            .Select(job => new GetAllJobDto
            {
                Id = job.Id,
                Title = job.Title,
                Salary = job.Salary,
                CreateOn = job.CreateOn,
                Employer = new GetEmployer
                {
                    Id = job.Employer.Id,
                    Company = new GetCompany 
                    {
                        Name= job.Employer.Company.Name,
                        logo= job.Employer.Company.Logo
                    },
                },
                JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                JobType = EnumHelper.GetEnumDescription(job.JobType),
                JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),                
                LocationShort = job.Address.Province + ", " + job.Address.District,
                Skills = job.JobSkills.Select(js => new SkillDto
                {
                    Id=js.Skill.Id,
                    Name = js.Skill.Name
                    // Include other properties of Skill as needed

                }).ToList()
            })
        .ToListAsync(); 
        }

        public async Task<List<GetAllJobDto>> GetEmployerJob(AppUser user, JobQueryObject query)
        {
            var job = _context.Jobs
                        .Where(job => job.EmployerId == user.Id).Include(job => job.JobSkills)
                        .ThenInclude(jobSkill => jobSkill.Skill).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                job = job.Where(s => s.Title.Contains(query.Title));
            }

            //if (!string.IsNullOrWhiteSpace(query.Location))
            //{
            //    job = job.Where(s => (s.Address.Street + " " +
            //              s.Address.Province + " " +
            //              s.Address.Ward + " " +
            //              s.Address.District)
            //              .Contains(query.Location));
            //}
            //if (!string.IsNullOrWhiteSpace(query.JobLevel))
            //{
            //    var level = EnumHelper.GetEnumValueFromDescription<JobLevel>(query.JobLevel);
            //    job = job.Where(s => s.JobLevel == level);
            //}
            //if (!string.IsNullOrWhiteSpace(query.JobStatus))
            //{
            //    var Status = EnumHelper.GetEnumValueFromDescription<JobStatus>(query.JobStatus);
            //    job = job.Where(s => s.JobStatus == Status);
            //}
            //if (!string.IsNullOrWhiteSpace(query.JobType))
            //{
            //    var Type = EnumHelper.GetEnumValueFromDescription<JobType>(query.JobType);
            //    job = job.Where(s => s.JobType == Type);
            //}

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("CreateOn", StringComparison.OrdinalIgnoreCase))
                {
                    job = query.IsDecsending ? job.OrderByDescending(s => s.CreateOn) : job.OrderBy(s => s.CreateOn);
                }
            }

            if (query.PageSize == 0 && query.PageNumber == 0) return await job
            .Select(job => new GetAllJobDto
            {
                Id = job.Id,
                Title = job.Title,
                Salary = job.Salary,
                CreateOn = job.CreateOn,
                Employer = new GetEmployer
                {
                    Id = job.Employer.Id,
                    Company = new GetCompany
                    {
                        Name = job.Employer.Company.Name,
                        logo = job.Employer.Company.Logo
                    },
                },
                JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                JobType = EnumHelper.GetEnumDescription(job.JobType),
                JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                LocationShort = job.Address.Province + ", " + job.Address.District,
                Skills = job.JobSkills.Select(js => new SkillDto
                {
                    Id = js.Skill.Id,
                    Name = js.Skill.Name
                    // Include other properties of Skill as needed

                }).ToList()
            })
        .ToListAsync();

            var skipNumber = (query.PageNumber - 1) * query.PageSize;
            return await job.Skip(skipNumber).Take(query.PageSize)
            .Select(job => new GetAllJobDto
            {
                Id = job.Id,
                Title = job.Title,
                Salary = job.Salary,
                CreateOn = job.CreateOn,
                Employer = new GetEmployer
                {
                    Id = job.Employer.Id,
                    Company = new GetCompany
                    {
                        Name = job.Employer.Company.Name,
                        logo = job.Employer.Company.Logo
                    },
                },
                JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                JobType = EnumHelper.GetEnumDescription(job.JobType),
                JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                LocationShort = job.Address.Province + ", " + job.Address.District,
                Skills = job.JobSkills.Select(js => new SkillDto
                {
                    Id = js.Skill.Id,
                    Name = js.Skill.Name
                    // Include other properties of Skill as needed

                }).ToList()
            })
        .ToListAsync();
        }


        public async Task<int> GetTotalForEmployer(AppUser user, JobQueryObject query)
        {
            var job = _context.Jobs
                        .Where(job => job.EmployerId == user.Id && job.ExpiredDate < DateTime.Now).AsQueryable();
            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                job = job.Where(s => s.Title.Contains(query.Title));
            }


            return await job.CountAsync();
        }
        //public async Task<List<GetAllJobDto>> GetEmployerJob(AppUser user, JobQueryObject query)
        //{
        //    // Truy vấn các công việc của nhà tuyển dụng có ứng viên đã ứng tuyển
        //    var jobQuery = _context.Jobs
        //        .Where(job => job.EmployerId == user.Id) // Lọc công việc của nhà tuyển dụng
        //        .Include(job => job.JobSkills)
        //            .ThenInclude(jobSkill => jobSkill.Skill) // Bao gồm JobSkills và Skill
        //        .Include(job => job.Applications) // Bao gồm Applications
        //            .ThenInclude(application => application.User) // Bao gồm User trong Application
        //        .Include(job => job.Address) // Bao gồm Address để sử dụng trong LocationShort
        //        .AsQueryable();

        //    // Điều kiện tìm kiếm theo tiêu đề công việc
        //    if (!string.IsNullOrWhiteSpace(query.Title))
        //    {
        //        jobQuery = jobQuery.Where(s => s.Title.Contains(query.Title));
        //    }

        //    // Điều kiện sắp xếp theo ngày tạo
        //    if (!string.IsNullOrWhiteSpace(query.SortBy))
        //    {
        //        if (query.SortBy.Equals("CreateOn", StringComparison.OrdinalIgnoreCase))
        //        {
        //            jobQuery = query.IsDecsending
        //                ? jobQuery.OrderByDescending(s => s.CreateOn)
        //                : jobQuery.OrderBy(s => s.CreateOn);
        //        }
        //    }

        //    // Lọc các công việc đã có ứng viên ứng tuyển
        //    var jobListWithApplications = await jobQuery
        //        .Where(job => job.Applications.Any(application => application.UserId != null)) // Lọc các công việc có ứng viên ứng tuyển
        //        .Select(job => new GetAllJobDto
        //        {
        //            Id = job.Id,
        //            Title = job.Title,
        //            Salary = job.Salary,
        //            CreateOn = job.CreateOn,
        //            Employer = new GetEmployer
        //            {
        //                Id = job.Employer.Id,
        //                Company = new GetCompany
        //                {
        //                    Name = job.Employer.Company.Name,
        //                    logo = job.Employer.Company.Logo
        //                },
        //            },
        //            JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
        //            JobType = EnumHelper.GetEnumDescription(job.JobType),
        //            JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
        //            LocationShort = job.Address != null
        //                ? job.Address.Province + ", " + job.Address.District
        //                : "N/A", // Kiểm tra null để tránh lỗi nếu Address là null
        //            Skills = job.JobSkills.Select(js => new SkillDto
        //            {
        //                Id = js.Skill.Id,
        //                Name = js.Skill.Name
        //            }).ToList(),
        //            IsShow = job.Applications.Any(application => application.Isshow) // Kiểm tra xem có ứng viên nào được hiển thị không
        //        })
        //        .ToListAsync();

        //    return jobListWithApplications;
        //}

        //public async Task<int> GetTotalforEmployerAsync(AppUser user)
        //{
        //    var status = EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");
        //    var totalJobs = await _context.Jobs
        //                          .Where(u => u.JobStatus == status && u.EmployerId== user.Id)
        //                          .CountAsync();

        //    return totalJobs;

        //}
        public Task<List<GetJobByIdDto>> GetJobById(int JobId, string EmployerId)
        {
            var jobModel = _context.Jobs.Include(job => job.JobSkills)
            .ThenInclude(jobSkill => jobSkill.Skill).Where(u => u.Id == JobId && u.EmployerId==EmployerId);

            return jobModel.Select(job => new GetJobByIdDto
            {
                Id = job.Id,
                Title = job.Title,
                Description = job.Description,
                Requirements = job.Requirements,
                Benefits = job.Benefits,
                Salary = job.Salary,
                ExpiredDate = job.ExpiredDate,
                CreateOn = job.CreateOn,
                UpdatedOn = job.UpdatedOn,
                Users = job.Applications.Where(u=>u.Status !=0 && u.Isshow == true).Select(user => new AppUserDto
                {
                    Id=user.UserId,
                    FullName = user.User.Fullname,
                    Email = user.User.Email,
                    CV = user.Cv,
                    Status = user.Status
                    // Include other properties of Skill as needed

                }).ToList(),
                JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                JobType = EnumHelper.GetEnumDescription(job.JobType),
                JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                Location = job.Address.Street + " " +
                              job.Address.Province + " " +
                              job.Address.Ward + " " +
                              job.Address.District,
                Skills = job.JobSkills.Select(js => new Skill
                {


                    Id = js.Skill.Id,
                    Name = js.Skill.Name
                    // Include other properties of Skill as needed

                }).ToList()
            }).ToListAsync();
        }

        public async Task<JobDto> GetJobByIdForAll(int JobId)
        {
            var status = EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");

            // Query to retrieve the job details
            var jobModel = await _context.Jobs
                .Include(a => a.Employer)
                    .ThenInclude(b => b.Company)
                .Include(job => job.JobSkills)
                    .ThenInclude(jobSkill => jobSkill.Skill)
                    .Include(j=>j.Address)
                .Where(u => u.JobStatus == status && u.Id == JobId)
                .Select(job => new JobDto
                {
                    Id = job.Id,
                    Title = job.Title,
                    Description = job.Description,
                    Requirements = job.Requirements,
                    Benefits = job.Benefits,
                    Salary = job.Salary,
                    ExpiredDate = job.ExpiredDate,
                    CreateOn = job.CreateOn,
                    UpdatedOn = job.UpdatedOn,
                    Employer = new GetEmployerDto
                    {
                        Id = job.Employer.Id,
                        Company = job.Employer.Company.ToCompanyDto(),

                    },
                    JobLevel = EnumHelper.GetEnumDescription(job.JobLevel),
                    JobType = EnumHelper.GetEnumDescription(job.JobType),
                    JobStatus = EnumHelper.GetEnumDescription(job.JobStatus),
                    Location = 
                               job.Address.Province + ", " +
                               job.Address.District + ", " +
                               job.Address.Ward,
                    LocationShort = job.Address.Street,
                    Skills = job.JobSkills.Select(js => new SkillDto
                    {
                        Id = js.Skill.Id,
                        Name = js.Skill.Name
                        // Include other properties of Skill as needed
                    }).ToList()
                })
                .FirstOrDefaultAsync();

            return jobModel;
        }

        public async Task<int> GetTotalAsync()
        {
            var status = EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");
            var totalJobs = await _context.Jobs
                                  .Where(u => u.JobStatus == status && u.ExpiredDate < DateTime.Now)
                                  .CountAsync();

            return totalJobs;

        }

        public async Task<int> GetTotalWithConditionsAsync(JobQueryObject query)
        {
            var status = EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");
            var jobQuery = _context.Jobs.Where(u => u.JobStatus == status && u.ExpiredDate < DateTime.Now).AsQueryable();

            if (!string.IsNullOrWhiteSpace(query.Title))
            {
                jobQuery = jobQuery.Where(s => s.Title.Contains(query.Title));
            }

            if (!string.IsNullOrWhiteSpace(query.Location))
            {
                jobQuery = jobQuery.Where(s => (s.Address.Street + " " +
                                                 s.Address.Province + " " +
                                                 s.Address.Ward + " " +
                                                 s.Address.District)
                                                 .Contains(query.Location));
            }

            if (!string.IsNullOrWhiteSpace(query.JobLevel))
            {
                var level = EnumHelper.GetEnumValueFromDescription<JobLevel>(query.JobLevel);
                jobQuery = jobQuery.Where(s => s.JobLevel == level);
            }

            if (!string.IsNullOrWhiteSpace(query.JobStatus))
            {
                var Status = EnumHelper.GetEnumValueFromDescription<JobStatus>(query.JobStatus);
                jobQuery = jobQuery.Where(s => s.JobStatus == Status);
            }

            if (!string.IsNullOrWhiteSpace(query.JobType))
            {
                var Type = EnumHelper.GetEnumValueFromDescription<JobType>(query.JobType);
                jobQuery = jobQuery.Where(s => s.JobType == Type);
            }

            if (!string.IsNullOrWhiteSpace(query.SortBy))
            {
                if (query.SortBy.Equals("CreateOn", StringComparison.OrdinalIgnoreCase))
                {
                    jobQuery = query.IsDecsending ? jobQuery.OrderByDescending(s => s.CreateOn) : jobQuery.OrderBy(s => s.CreateOn);
                }
            }

            // Tính tổng số lượng công việc phù hợp với các điều kiện
            return await jobQuery.CountAsync();
        }


        public void UpdateStatusJob(int JobId, JobStatus Status)
        {   
            Job job = _context.Jobs.Where(u=> u.Id== JobId).FirstOrDefault();
            job.JobStatus= Status;
            var status = EnumHelper.GetEnumValueFromDescription<JobStatus>("Approved");
            if (Status== status)
                job.CreateOn=DateTime.Now;
            _context.Jobs.Update(job);
            _context.SaveChanges();

        }
    }

}
