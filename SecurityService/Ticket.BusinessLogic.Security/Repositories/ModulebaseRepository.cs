using Ticket.Base.Entities;
using Ticket.BusinessLogic.Common;
using Ticket.DataAccess.Common;
using Ticket.DataAccess.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ticket.BusinessLogic.Security
{
    public class ModuleBaseRepository<T> : BaseRepository<T> where T : class, IEntity
    {
        public ModuleBaseRepository(BaseValidationErrorCodes errorCodes, DatabaseContext dbContext, IUser loggedUser) : base(errorCodes, dbContext, loggedUser)
        {

        }

        public new ValidationErrorCodes ErrorCodes
        {
            get
            {
                return base.ErrorCodes as ValidationErrorCodes;
            }
        }

        public override async Task<bool> Exists(string description)
        {
            return false;
        }

        public override async Task<IEnumerable<IValidationResult>> ValidateEntityToAdd(T entity)
        {
            return await this.ValidateEntity(entity);
        }

        public override async Task<IEnumerable<IValidationResult>> ValidateEntityToUpdate(T entity)
        {
            return await this.ValidateEntity(entity);
        }

        public override async Task<IEnumerable<IValidationResult>> ValidateEntity(T entity)
        {
            return await base.ValidateEntity(entity);
        }
    }
}
