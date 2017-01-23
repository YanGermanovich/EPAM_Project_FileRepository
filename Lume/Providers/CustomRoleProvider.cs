using BLL.Services_Interface;
using Lume.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Web.Helpers;
using Lume.Infrastructure;
using Lume.Infrastructure.Mappers;
using BLL.Entities;

namespace Lume.Providers
{
    //провайдер ролей указывает системе на статус пользователя и наделяет 
    //его определенные правами доступа
    public class CustomRoleProvider : RoleProvider
    {
        public IUserService UserRepository
        {
            get { return (IUserService)DependencyResolver.Current.GetService(typeof(IUserService)); }
        }

        public IRoleService RoleRepository
        {
            get { return (IRoleService)DependencyResolver.Current.GetService(typeof(IRoleService)); }
        }

        public override string ApplicationName { get; set; }


        public override bool IsUserInRole(string email, string roleName)
        {

            UserViewModel user = UserRepository.GetAllEntities().FirstOrDefault(u => u.Email == email).ToMvcUser();

            if (user == null) return false;

            Role userRole = (Role)RoleRepository.GetEntitieById((int)user.Role).Id;

            if (userRole.ToString() == roleName)
            {
                return true;
            }

            return false;
        }

        public override string[] GetRolesForUser(string email)
        {

            var roles = new string[] { };
            var user = UserRepository.GetByEmail(email).ToMvcUser();

            if (user == null) return roles;

            var userRole = user.Role;

            roles = new string[] { userRole.ToString() };

            return roles;

        }

        public override void CreateRole(string roleName)
        {
            var newRole = new RoleEntity() { Name = roleName };
            RoleRepository.Create(newRole);
        }

        #region Stabs
        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }
        #endregion
    }
}