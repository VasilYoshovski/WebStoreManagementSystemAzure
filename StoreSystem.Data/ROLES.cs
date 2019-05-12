using System;
using System.Collections.Generic;
using System.Text;

namespace StoreSystem.Data
{
    public static class ROLES
    {
        public const string Admin = "Admin_R";
        public const string Client = "Client_R";
        public const string OfficeStaff = "OfficeStaff_R";
        public const string SiteObserver = "SiteObserver_R";
        public const string Supplier = "Supplier_R";
        public const string Visitor = "Visitor_R";
        public const string AdminAndOfficeStaff = "Admin_R, OfficeStaff_R";

        public static string[] Roles()
        {
            return new string[] {
                Admin,
                Client,
                OfficeStaff,
                SiteObserver,
                Supplier,
                Visitor
            };
        }
    }
}
