using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Intex_2017.Models;
using Intex_2017.Models.ViewModels;
using Intex_2017.DAL;

namespace Intex_2017.App_Start
{
    public class IntexRoleProvider : RoleProvider
    {
        private IntexContext db = new IntexContext();
        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            string[] role = new string[] { };
            var employeeRole = db.Database.SqlQuery<EmployeeRole>(
                "SELECT Role FROM Employee WHERE EmpUsername = \'" + username + "\'"
                ).FirstOrDefault();

            if (employeeRole != null)
            {
                if (employeeRole.Role == "SysAdmin")
                {
                    return new string[] { "SysAdmin" };
                }
                else if (employeeRole.Role == "LabTech")
                {
                    return new string[] { "LabTech" };
                }
                else if (employeeRole.Role == "SalesAgent")
                {
                    return new string[] { "SalesAgent" };
                }
                else if (employeeRole.Role == "Billing")
                {
                    return new string[] { "Billing" };
                }
                else if (employeeRole.Role == "Reports")
                {
                    return new string[] { "Reports" }; 
                }
                else if (employeeRole.Role == "TechDirector")
                {
                    return new string[] { "TechDirector" };
                }
                else if (employeeRole.Role == "Manager")
                {
                    return new string[] { "Manager" };
                }
            }
            return new string[] { "Customer" };
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}