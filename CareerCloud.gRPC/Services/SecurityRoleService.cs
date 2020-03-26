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
using static CareerCloud.gRPC.Protos.SecurityRole;

namespace CareerCloud.gRPC.Services
{
    public class SecurityRoleService : SecurityRoleBase
    {
        private readonly SecurityRoleLogic _logic;

        public SecurityRoleService() 
        {
            _logic = new SecurityRoleLogic(new EFGenericRepository<SecurityRolePoco>());
        }

        public override Task<SecurityRolePayLoad> ReadSecurityRole(IdRequestSecRole request, ServerCallContext context) 
        {
            SecurityRolePoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<SecurityRolePayLoad>(
                () => new SecurityRolePayLoad()
                {
                    Id = poco.Id.ToString(),
                    Role = poco.Role.ToString(),
                    IsInactive = poco.IsInactive
                }
            );
        }

        public override Task<Empty> CreateSecurityRole(SecurityRolePayLoad request, ServerCallContext context)
        {
            SecurityRolePoco poco = new SecurityRolePoco()
            {
                Id = new Guid(request.Id),
                Role = request.Role,
                IsInactive = request.IsInactive
            };

            _logic.Add(new SecurityRolePoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateSecurityRole(SecurityRolePayLoad request, ServerCallContext context)
        {
            SecurityRolePoco poco = new SecurityRolePoco()
            {

                Id = new Guid(request.Id),
                Role = request.Role,
                IsInactive = request.IsInactive

            };

            _logic.Update(new SecurityRolePoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteSecurityRole(SecurityRolePayLoad request, ServerCallContext context)
        {
            SecurityRolePoco poco = new SecurityRolePoco()
            {

                Id = new Guid(request.Id),
                Role = request.Role,
                IsInactive = request.IsInactive

            };

            _logic.Delete(new SecurityRolePoco[] { poco });


            return null;
        }


    }
}
