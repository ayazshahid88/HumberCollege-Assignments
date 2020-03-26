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
using static CareerCloud.gRPC.Protos.SystemCountryCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemCountryCodeService : SystemCountryCodeBase
    {
        private readonly SystemCountryCodeLogic _logic;

        public SystemCountryCodeService() 
        {
            _logic = new SystemCountryCodeLogic(new EFGenericRepository<SystemCountryCodePoco>());
        }

        public override Task<SystemCountryCodePayLoad> ReadSystemCountryCode(IdRequestSysCountryCode request, ServerCallContext context)
        {
            SystemCountryCodePoco poco = _logic.Get(request.Id);

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<SystemCountryCodePayLoad>(
                () => new SystemCountryCodePayLoad()
                {
                    Code = poco.Code,
                    Name = poco.Name
                }
            );
        }

        public override Task<Empty> CreateSystemCountryCode(SystemCountryCodePayLoad request, ServerCallContext context)
        {
            SystemCountryCodePoco poco = new SystemCountryCodePoco()
            {
                Code = request.Code,
                Name = request.Name
            };

            _logic.Add(new SystemCountryCodePoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateSystemCountryCode(SystemCountryCodePayLoad request, ServerCallContext context)
        {
            SystemCountryCodePoco poco = new SystemCountryCodePoco()
            {
                Code = request.Code,
                Name = request.Name
            };

            _logic.Update(new SystemCountryCodePoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteSystemCountryCode(SystemCountryCodePayLoad request, ServerCallContext context)
        {
            SystemCountryCodePoco poco = new SystemCountryCodePoco()
            {
                Code = request.Code,
                Name = request.Name

            };

            _logic.Delete(new SystemCountryCodePoco[] { poco });


            return null;
        }


    }
}
