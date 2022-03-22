namespace FileStorage.Permissions
{
    public static class StoragePermissions
    {
        public const string StorageManagement = "StorageManagement";

        public static class File
        {
            public const string Default = StorageManagement + ".File";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        //Code generation...
    }
}
