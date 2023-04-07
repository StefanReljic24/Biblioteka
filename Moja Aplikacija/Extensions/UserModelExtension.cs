using Moja_Aplikacija.Entity;
using Moja_Aplikacija.Models;

namespace Moja_Aplikacija.Extensions
{
    public static class UserModelExtension
    {
        public static User ToModel(this UserModel model)
        {
            return new User
            {
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                Active = model.Active,
                Password = model.Password,
                UserRole = model.Roles.Select(p => new UserRole
                {
                    RoleId = p.RoleId,
                    UserId = model.UserId
                }).ToList()

            };
        }
    }
}
