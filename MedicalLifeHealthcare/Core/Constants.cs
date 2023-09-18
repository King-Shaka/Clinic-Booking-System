namespace MedicalLifeHealthcare.Core
{
    public static class Constants
    {
        public static class Roles
        {
            public const string Administrator = "Admin";
            public const string Doctor = "Doctor";
            public const string Nurse = "Nurse";
            public const string Counsellor = "Counsellor";
            public const string Pathology = "Pathology";
            public const string Walkins = "Walkins";
            public const string User = "User";

        }

        public static class Policies
        {
            public const string RequireAdmin = "RequireAdmin";
            public const string RequireDoctor = "RequireDoctor";
            public const string RequireNurse = "RequireNurse";
            public const string RequireCounsellor = "RequireCounsellor";
            public const string RequirePathology = "RequirePathology";
            public const string RequireWalkins = "RequireWalkins";
        }
    }
}
