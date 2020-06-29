using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Permissions
{
    public static class BusinessPermissions
    {
        public const string Business = "Business";

        public static class DataDictionary
        {
            public const string Default = Business + ".DataDictionary";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class DataDictionaryDetail
        {
            public const string Default = Business + ".DataDictionaryDetail";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class Organization
        {
            public const string Default = Business + ".Organization";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class Job
        {
            public const string Default = Business + ".Job";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class Employee
        {
            public const string Default = Business + ".Employee";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }
    }
}
