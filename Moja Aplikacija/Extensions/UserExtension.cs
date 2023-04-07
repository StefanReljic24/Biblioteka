using Moja_Aplikacija.Entity;
using Moja_Aplikacija.Models;

namespace Moja_Aplikacija.Extensions
{
    public  static class UserExtension
    {
        public static UserModel ToModel(this User model)
        {
            return new UserModel
            {
                UserId = model.UserId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
                UserName = model.UserName,
                Password = model.Password,
                Active = model.Active,
                Roles = model.UserRole.Select(p => new RoleModel
                {
                    RoleId = p.RoleId,
                    RoleName = p.Role.Name
                }).ToList()
            };
        }
        public static IEnumerable<UserModel> ToModel(this IEnumerable<User> model)
        {
            return model.Select(p => p.ToModel());
        }
    }
}
