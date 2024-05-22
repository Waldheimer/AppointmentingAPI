using Appointmenting.API.Domain.ValueObjects;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Appointmenting.API.Domain.Entities
{
    public class User : IdentityUser
    {
        public FirstName? FirstName { get; set; }
        public LastName? LastName { get; set; }
    }
}
