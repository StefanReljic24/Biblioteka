namespace Moja_Aplikacija.Entity
{
    public class Role
    {
        public int RoleId { get; set; }
        public string Name { get; set; } = "";
        public bool Active { get; set; }
        public IEnumerable<UserRole> UserRole { get; set; } = new List<UserRole>();

    }
}
