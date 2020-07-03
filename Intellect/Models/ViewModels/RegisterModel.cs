using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Intellect.Models.ViewModels
{
    public class RegisterModel
    {
        public int Id { get; set; }

        [Required]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [MinLength(8)]
        public string ConfirmPassword { get; set; }

        public TestTaker TestTaker { get; set; }

        public static implicit operator ExamUser(RegisterModel registerModel)
        {
            return new ExamUser
            {
                Email = registerModel.Email,
                UserName = registerModel.UserName,
                TestTakerId = registerModel.TestTaker.Id                
            };
        }
    }
}
