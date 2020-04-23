using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace API.Model
{
    public class LoginVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
    }
    public class RegisterVM
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; }
        public string DeptModelId { get; set; }
    }
    public class RoleVM
    {
        public string Name { get; set; }
        public string Role { get; set; }

    }
}
