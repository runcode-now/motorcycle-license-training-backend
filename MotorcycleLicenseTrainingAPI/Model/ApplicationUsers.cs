﻿using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MotorcycleLicenseTrainingAPI.Model
{
    public class ApplicationUsers : IdentityUser
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public virtual ICollection<PracticeHistories> PracticeHistories { get; set; }
        public virtual ICollection<MockExams> MockExams { get; set; }
    }
}
