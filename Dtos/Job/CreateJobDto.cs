﻿using APIWeb1.Models.Enum;
using APIWeb1.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace APIWeb1.Dtos.Job
{
    public class CreateJobDto
    {
        public string Title { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public string Requirements { get; set; }
        [Required]
        public string Benefits { get; set; }
        [Required]
        public double Salary { get; set; }
        [Required]
        public DateTime? ExpiredDate { get; set; }
        public DateTime CreateOn { get; set; } = DateTime.Now;
        public DateTime? UpdatedOn { get; set; }= DateTime.Now;
        public JobLevel JobLevel { get; set; }
        public JobType JobType { get; set; }
        public JobStatus JobStatus { get; set; }

        public string Street { get; set; }
        public string Province { get; set; }
        public string Ward { get; set; }
        public string District { get; set; }
        public List<int> SkillIds { get; set; }

    }
}
