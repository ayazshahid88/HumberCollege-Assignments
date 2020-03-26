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
using static CareerCloud.gRPC.Protos.SystemLanguageCode;

namespace CareerCloud.gRPC.Services
{
    public class SystemLanguageCodeService : SystemLanguageCodeBase
    {
        private readonly SystemLanguageCodeLogic _logic;

        public SystemLanguageCodeService()
        {
            _logic = new SystemLanguageCodeLogic(new EFGenericRepository<SystemLanguageCodePoco>());
        }

        public override Task<SystemLanguageCodePayLoad> ReadSystemLanguageCode(IdRequestSysLangCode request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = _logic.Get(request.Id);

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<SystemLanguageCodePayLoad>(
                () => new SystemLanguageCodePayLoad()
                {
                    LanguageID = poco.LanguageID,
                    Name = poco.Name,
                    NativeName = poco.NativeName

                }
            );
        }

        public override Task<Empty> CreateSystemLanguageCode(SystemLanguageCodePayLoad request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = new SystemLanguageCodePoco()
            {
                LanguageID = request.LanguageID,
                Name = request.Name,
                NativeName = request.NativeName

            };

            _logic.Add(new SystemLanguageCodePoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateSystemLanguageCode(SystemLanguageCodePayLoad request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = new SystemLanguageCodePoco()
            {
                LanguageID = request.LanguageID,
                Name = request.Name,
                NativeName = request.NativeName

            };

            _logic.Update(new SystemLanguageCodePoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteSystemLanguageCode(SystemLanguageCodePayLoad request, ServerCallContext context)
        {
            SystemLanguageCodePoco poco = new SystemLanguageCodePoco()
            {
                LanguageID = request.LanguageID,
                Name = request.Name,
                NativeName = request.NativeName

            };

            _logic.Delete(new SystemLanguageCodePoco[] { poco });


            return null;
        }
    }
}
