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
using static CareerCloud.gRPC.Protos.SecurityLoginsLog;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginsLogService : SecurityLoginsLogBase
    {
        private readonly SecurityLoginsLogLogic _logic;

        public SecurityLoginsLogService() 
        {
            _logic = new SecurityLoginsLogLogic(new EFGenericRepository<SecurityLoginsLogPoco>());
        }

        public override Task<SecurityLoginsLogPayLoad> ReadSecurityLoginsLog(IdRequestSecLoginsLog request, ServerCallContext context)
        { 
            SecurityLoginsLogPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<SecurityLoginsLogPayLoad>(
                () => new SecurityLoginsLogPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    SourceIP = poco.SourceIP,
                    LogonDate = Timestamp.FromDateTime((DateTime)poco.LogonDate),
                    IsSuccesful = poco.IsSuccesful
                }
            );
        }

        public override Task<Empty> CreateSecurityLoginsLog(SecurityLoginsLogPayLoad request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco()
            {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                SourceIP = request.SourceIP,
                LogonDate = request.LogonDate.ToDateTime(),
                IsSuccesful = request.IsSuccesful

            };
            
            _logic.Add(new SecurityLoginsLogPoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateSecurityLoginsLog(SecurityLoginsLogPayLoad request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco()
            {

                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                SourceIP = request.SourceIP,
                LogonDate = request.LogonDate.ToDateTime(),
                IsSuccesful = request.IsSuccesful

            };

            _logic.Update(new SecurityLoginsLogPoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteSecurityLoginsLog(SecurityLoginsLogPayLoad request, ServerCallContext context)
        {
            SecurityLoginsLogPoco poco = new SecurityLoginsLogPoco()
            {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                SourceIP = request.SourceIP,
                LogonDate = request.LogonDate.ToDateTime(),
                IsSuccesful = request.IsSuccesful


            };

            _logic.Delete(new SecurityLoginsLogPoco[] { poco });


            return null;
        }

    }
}
