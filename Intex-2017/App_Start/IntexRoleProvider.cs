using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Intex_2017.Models;
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
            Customer customer = db.Customers.Find(username);
            if (customer != null)
            {
                return role;
            }
            else
            {
                Employee employee = db.Employees.Find(username);
                if (employee != null)
                {
                    if (employee.Role == "SysAdmin")
                    {
                        return new string[] { "SysAdmin" };
                    }
                    else if (employee.Role == "LabTech")
                    {
                        return new string[] { "LabTech" };
                    }
                    else if (employee.Role == "SalesAgent")
                    {
                        return new string[] { "SalesAgent" };
                    }
                    else if (employee.Role == "Billing")
                    {
                        return new string[] { "Billing" };
                    }
                    else if (employee.Role == "Reports")
                    {
                        return new string[] { "Reports" }; 
                    }
                }
            }
            return role;
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