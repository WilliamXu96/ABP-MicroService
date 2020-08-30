using System;
using System.Collections.Generic;
using System.Text;

namespace BaseService.Permissions
{
    public static class BaseServicePermissions
    {
        public const string BaseService = "BaseService";

        public static class AuditLogging
        {
            public const string Default = BaseService + ".AuditLogging";
        }

        public static class DataDictionary
        {
            public const string Default = BaseService + ".DataDictionary";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class DataDictionaryDetail
        {
            public const string Default = BaseService + ".DataDictionaryDetail";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class Organization
        {
            public const string Default = BaseService + ".Organization";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class Job
        {
            public const string Default = BaseService + ".Job";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }

        public static class Employee
        {
            public const string Default = BaseService + ".Employee";
            public const string Delete = Default + ".Delete";
            public const string Update = Default + ".Update";
            public const string Create = Default + ".Create";
        }
    }
}
