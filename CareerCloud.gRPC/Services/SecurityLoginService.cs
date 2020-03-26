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
using static CareerCloud.gRPC.Protos.SecurityLogin;

namespace CareerCloud.gRPC.Services
{
    public class SecurityLoginService : SecurityLoginBase
    {
        private readonly SecurityLoginLogic _logic;

        public SecurityLoginService()
        {
            _logic = new SecurityLoginLogic(new EFGenericRepository<SecurityLoginPoco>());
        }

        public override Task<SecurityLoginPayLoad> ReadSecurityLogin(IdRequestSecLogin request, ServerCallContext context)
        {
            SecurityLoginPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<SecurityLoginPayLoad>(
                () => new SecurityLoginPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login,
                    Password = poco.Password,
                    Created = Timestamp.FromDateTime((DateTime)poco.Created),
                    PasswordUpdate = Timestamp.FromDateTime((DateTime)poco.PasswordUpdate),
                    AgreementAccepted = Timestamp.FromDateTime((DateTime)poco.AgreementAccepted),
                    IsLocked = poco.IsLocked,
                    IsInactive = poco.IsInactive,
                    EmailAddress = poco.EmailAddress,
                    PhoneNumber = poco.PhoneNumber,
                    FullName = poco.FullName,
                    ForceChangePassword = poco.ForceChangePassword,
                    PrefferredLanguage = poco.PrefferredLanguage
                }
            );
        }

        public override Task<Empty> CreateSecurityLogin(SecurityLoginPayLoad request, ServerCallContext context)
        {
            SecurityLoginPoco poco = new SecurityLoginPoco()
            {
                Id = new Guid(request.Id),
                Login = request.Login,
                Password = request.Password,
                Created = request.Created.ToDateTime(),
                PasswordUpdate = request.PasswordUpdate.ToDateTime(),
                AgreementAccepted = request.AgreementAccepted.ToDateTime(),
                IsLocked = request.IsLocked,
                IsInactive = request.IsInactive,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                ForceChangePassword = request.ForceChangePassword,
                PrefferredLanguage = request.PrefferredLanguage

            };

            _logic.Add(new SecurityLoginPoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateSecurityLogin(SecurityLoginPayLoad request, ServerCallContext context)
        {
            SecurityLoginPoco poco = new SecurityLoginPoco()
            {
                Id = new Guid(request.Id),
                Login = request.Login,
                Password = request.Password,
                Created = request.Created.ToDateTime(),
                PasswordUpdate = request.PasswordUpdate.ToDateTime(),
                AgreementAccepted = request.AgreementAccepted.ToDateTime(),
                IsLocked = request.IsLocked,
                IsInactive = request.IsInactive,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                ForceChangePassword = request.ForceChangePassword,
                PrefferredLanguage = request.PrefferredLanguage
            };

            _logic.Update(new SecurityLoginPoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteSecurityLogin(SecurityLoginPayLoad request, ServerCallContext context)
        {
            SecurityLoginPoco poco = new SecurityLoginPoco()
            {
                Id = new Guid(request.Id),
                Login = request.Login,
                Password = request.Password,
                Created = request.Created.ToDateTime(),
                PasswordUpdate = request.PasswordUpdate.ToDateTime(),
                AgreementAccepted = request.AgreementAccepted.ToDateTime(),
                IsLocked = request.IsLocked,
                IsInactive = request.IsInactive,
                EmailAddress = request.EmailAddress,
                PhoneNumber = request.PhoneNumber,
                FullName = request.FullName,
                ForceChangePassword = request.ForceChangePassword,
                PrefferredLanguage = request.PrefferredLanguage

            };

            _logic.Delete(new SecurityLoginPoco[] { poco });


            return null;
        }


    }
}
