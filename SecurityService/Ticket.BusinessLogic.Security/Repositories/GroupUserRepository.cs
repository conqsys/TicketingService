﻿using Ticket.DataAccess.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using Dapper;
using System.Data;
using Microsoft.Extensions.Configuration;
using Ticket.Base.Entities;
using Ticket.DataAccess.Security;
using Ticket.BusinessLogic.Common;
using Ticket.Base.Repositories;
using Npgsql;
using System.Linq.Expressions;
using Ticket.Base;
namespace Ticket.BusinessLogic.Security
{
    public class GroupUserRepository<TGroupUser> : ModuleBaseRepository<TGroupUser>, IGroupUserRepository
        where TGroupUser : class, IGroupUser, new()
    {
        public GroupUserRepository(BaseValidationErrorCodes errorCodes, DatabaseContext dbContext, IUser loggedUser)
            : base(errorCodes, dbContext, loggedUser)
        {
        }


        public async Task<IGroupUser> AddNew(IGroupUser entity)
        {
            try
            {
                this.StartTransaction();
                var savedEntity = await base.AddNew(entity as TGroupUser);
                this.CommitTransaction();

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

        public async Task<int> DeleteGroupUserByUserId(long userId)
        {
            try
            {
                this.StartTransaction();
                int deletedRecords = await this.Connection.DeleteAllAsync<TGroupUser>(i => i.UserId == userId);
                this.CommitTransaction();
                return deletedRecords;
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

        public async Task<IEnumerable<IGroupUser>> FindGroupUsersByUserId(long userId)
        {
            var groupUsers = await this.Connection.SelectAsync<TGroupUser>(i => i.UserId == userId);
            return groupUsers;
        }

    }
}
