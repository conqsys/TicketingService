﻿using Ticket.Base;
using Ticket.Base.Entities;
using Ticket.Base.Repositories;
using Ticket.DataAccess.Common;
using Ticket.DataAccess.Security;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Ticket.Base.Services;

namespace Ticket.BusinessLogic.Security
{
    public class UserService : IUserService
    {
        private IUserRepository _userRepository;
        private IRoleRepository _roleRepository;

        public UserService(IUserRepository userRepository, 
            IRoleRepository roleRepository)
        {
            this._userRepository = userRepository;
            this._roleRepository = roleRepository;
        }

        private async Task InsertUserPasswordLog(long userId, string password)
        {
            try
            {
                UserPasswordLog userPasswordLog = new UserPasswordLog();
                userPasswordLog.UserId = userId;
                userPasswordLog.DateChanged = DateTime.Now;
                userPasswordLog.PreviousPassword = password;

            }
            catch
            {
                throw;
            }
        }

        public async Task<IPaginationQuery<IUser>> GetAllUsers()
        {
            return await this._userRepository.FindAll(1, 1,10000, false);
        }
    
        public async Task<IUser> AddNewUser(IUser entity)
        {
            try
            {
                var savedEntity = await this._userRepository.AddNew(entity);

                //foreach (var roleID in entity.RoleIds)
                //{
                //    UserRoles userRole = new UserRoles();
                //    userRole.RoleId = roleID;
                //    userRole.UserId = savedEntity.Id;
                //    await this._userRoleRepository.AddNew(userRole);
                //}

               // await this._userActivityLogRepository.AddActivityLog("New User", savedEntity.Id, savedEntity.LabId, UserActivityType.NewUser);
                await InsertUserPasswordLog(savedEntity.Id, savedEntity.Password);
                
                return savedEntity;
            }
            catch (PostgresException ex)
            {
                throw new EntityUpdateException(ex);
            }
            catch
            {
                throw;
            }
        }
        
        public async Task ChangePassword(long userId, string password)
        {
            try
            {
                //if (await this._userPasswordLogRepository.ValidateUserPassword(userId, password))
                //{
                //    var user = await this._userRepository.UpdatePassword(userId, password);

                //    await this.InsertUserPasswordLog(userId, user.Password);

                ////    await this._userActivityLogRepository.AddActivityLog("Password Changed", user.Id, user.LabId, UserActivityType.PasswordChange);
                //}
            }
            catch (PostgresException ex)
            {
                throw new EntityUpdateException(ex);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> ChangeActiveState(long userId, bool isEnabled, long modifiedById)
        {
            try
            {
                var user = await this._userRepository.ChangeActiveState(userId, isEnabled, modifiedById);
                if (user != null)
                {
                    //await this._userActivityLogRepository
                    //        .AddActivityLog(isEnabled == true ? "Enable" : "Disable",
                    //        user.Id,1 , UserActivityType.UserEnable, 1);
                    //       // user.LabId,
                    //        //isEnabled == true ? UserActivityType.UserEnable : UserActivityType.UserDisable, modifiedById);

                    //return true;
                }
                return false;
            }
            catch (PostgresException ex)
            {
                throw new EntityUpdateException(ex);
            }
            catch
            {
                throw;
            }
        }

        public async Task<IUser> CheckUserAuthentication(string userName, string ipAddress)
        {
            try
            {
                IUser user = null;
                var currentUser = await this._userRepository.CheckAuthentication(userName, ipAddress);
                if (currentUser)
                {
                    user = await this._userRepository.FindFirstByName(userName);

                    //var userAuthentication = null;
                    //    //await this._userAuthenticationRepository.ValidateIPAddress(ipAddress, user.Id);

                    //if (userAuthentication)
                    //{
                    //    user = await this.FindUserWithRoles(user.Id);
                    //  //  await this._userActivityLogRepository.AddActivityLog("Authentication", user.Id, user.LabId, UserActivityType.Authentication);
                    //}
                }
                return user;
            }
            catch (PostgresException ex)
            {
                throw new EntityUpdateException(ex);
            }
            catch
            {
                throw;
            }
        }

        public async Task<bool> UpdateUser(IUser entity)
        {
            try
            {
                var user = await this._userRepository.Update(entity);
                return true;
            }
            catch
            {
                throw;
            }
        }
        
        public async Task<bool> LogOutUser(long userId)
        {
            try
            {
                var user = await this._userRepository.LogOut(userId);
                if (user != null)
                {
                    //await this._userActivityLogRepository.AddActivityLog("Log Out", user.Id, user.LabId, UserActivityType.LogOut);
                    return true;
                }
                return false;
            }
            catch
            {
                throw;
            }
        }


        public async Task DeleteRole(long roleId)
        {
            try
            {
               
                await this._roleRepository.Delete(roleId);
            }
            catch
            {
                throw;
            }
        }


    }
}
