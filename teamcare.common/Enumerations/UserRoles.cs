using System.ComponentModel;

namespace teamcare.common.Enumerations
{
    public enum UserRoles
    {
        [Description("Global Admin")]
        GlobalAdmin = 1,
        [Description("Admin")]
        Admin = 2,
        [Description("Staff Member")]
        StaffMember = 3,
        [Description("Service User")]
        ServiceUser = 4
    }
}

