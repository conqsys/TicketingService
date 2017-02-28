using Ticket.Base.Entities;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Ticket.UnitTests
{
    public class Program
    {

        public static void Main(string[] args)
        {
            var loginTests = new LoginTests();
            var labTests = new LabTests();
            var roleTests = new RoleTests();
            var userAuthenticationTests = new UserAuthenticationTests();
            var userTests = new UserTests();
            
            Task.Run(async () =>
            {
                //await loginTests.LoginTest();
                //await loginTests.LoginTestWithValidationErrror();

                //await labTests.PostLab();
                //await labTests.PostLabWithValidationError();
                //await labTests.PostLabWithRequiredValidationError();
                //await labTests.PostLabWithAdminSameUserNameExistValidationError();
                //await labTests.PutLab();

                //await roleTests.PostRole();
                //await roleTests.PostRoleWithValidationError();
                //await roleTests.PostRoleWithRequiredValidationError();
                //await roleTests.GetAllRole();
                //await roleTests.PostRolePermission();
                //await roleTests.PutRolePermission();
                //await roleTests.PostRolePermissionWithValidationError();
                //await roleTests.GetAllRolesPermission();
                //await roleTests.PutRole();
                
                //await userAuthenticationTests.UserAuthenticationWithValidationError();
                //await userAuthenticationTests.UserAuthenticationWithRequiredValidationError();
                //await userAuthenticationTests.UserAuthentication();
                //await userAuthenticationTests.VerifyAuthentication();
                //await userAuthenticationTests.VerifyAuthenticationWithValidationError();

                await userTests.PostUser();
                //await userTests.PostUserWithValidationError();
                //await userTests.PostUserWithRequiredValidationError();
                //await userTests.PutUser();
                //await userTests.GetAllUsers();
                //await userTests.DisableUser();
                //await userTests.EnableUser();
                //await userTests.ChangePasswordWithValidationError();
                //await userTests.ChangePassword();
                //await userTests.ConnectionTest();
                //await userTests.LogOut();

            }).GetAwaiter().GetResult();
        }



    }
}
