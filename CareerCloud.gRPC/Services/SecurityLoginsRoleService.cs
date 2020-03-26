using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.SecurityLoginsRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsRoleService : SecurityLoginsRoleBase
    {
        private readonly SecurityLoginsRoleLogic _logic;

        public SecurityLoginsRoleService() 
        {
            _logic = new SecurityLoginsRoleLogic(new EFGenericRepository<SecurityLoginsRolePoco>());
        }

        public override Task<SecurityLoginsRolePayLoad> ReadSecurityLoginsRole(IdRequestSecLoginsRole request, ServerCallContext context) 
        {
            SecurityLoginsRolePoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<SecurityLoginsRolePayLoad>(
                () => new SecurityLoginsRolePayLoad()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    Role = poco.Role.ToString()
                }
            );
        }

        public override Task<Empty> CreateSecurityLoginsRole(SecurityLoginsRolePayLoad request, ServerCallContext context)
        {
            SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco()
            {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                Role = new Guid(request.Role)

            };

            _logic.Add(new SecurityLoginsRolePoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateSecurityLoginsRole(SecurityLoginsRolePayLoad request, ServerCallContext context)
        {
            SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco()
            {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                Role = new Guid(request.Role)


            };

            _logic.Update(new SecurityLoginsRolePoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteSecurityLoginsRole(SecurityLoginsRolePayLoad request, ServerCallContext context)
        {
            SecurityLoginsRolePoco poco = new SecurityLoginsRolePoco()
            {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                Role = new Guid(request.Role)

            };

            _logic.Delete(new SecurityLoginsRolePoco[] { poco }); 


            return null;
        }

    }
}
