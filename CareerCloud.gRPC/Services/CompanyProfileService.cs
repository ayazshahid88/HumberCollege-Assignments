using CareerCloud.BusinessLogicLayer;
using CareerCloud.EntityFrameworkDataAccess;
using CareerCloud.gRPC.Protos;
using CareerCloud.Pocos;
using Google.Protobuf.WellKnownTypes;
using Grpc.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static CareerCloud.gRPC.Protos.CompanyProfile;

namespace CareerCloud.gRPC.Services
{
    public class CompanyProfileService : CompanyProfileBase
    {
        private readonly CompanyProfileLogic _logic;

        public CompanyProfileService()
        {
            _logic = new CompanyProfileLogic(new EFGenericRepository<CompanyProfilePoco>());
        }

        public override Task<CompanyProfilePayLoad> ReadCompanyProfilePayLoad(IdRequestCompProfile request, ServerCallContext context)
        {
            CompanyProfilePoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<CompanyProfilePayLoad>(
                () => new CompanyProfilePayLoad()
                {
                    Id = poco.Id.ToString(),
                    RegistrationDate = Timestamp.FromDateTime((DateTime)poco.RegistrationDate),
                    CompanyWebsite = poco.CompanyWebsite,
                    ContactPhone = poco.ContactPhone,
                    ContactName = poco.ContactName,
                    CompanyLogo = poco.CompanyLogo.ToString()
                }
            );
        }

        public override Task<Empty> CreateCompanyProfilePayLoad(CompanyProfilePayLoad request, ServerCallContext context)
        {
            CompanyProfilePoco poco = new CompanyProfilePoco()
            {
                Id = new Guid(request.Id),
                RegistrationDate = request.RegistrationDate.ToDateTime(),
                CompanyWebsite = request.CompanyWebsite,
                ContactPhone = request.ContactPhone,
                ContactName = request.ContactName,
                CompanyLogo = Encoding.ASCII.GetBytes(request.CompanyLogo)
        };

            _logic.Add(new CompanyProfilePoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateCompanyProfilePayLoad(CompanyProfilePayLoad request, ServerCallContext context)
        {
            CompanyProfilePoco poco = new CompanyProfilePoco()
            {
                Id = new Guid(request.Id),
                RegistrationDate = request.RegistrationDate.ToDateTime(),
                CompanyWebsite = request.CompanyWebsite,
                ContactPhone = request.ContactPhone,
                ContactName = request.ContactName,
                CompanyLogo = Encoding.ASCII.GetBytes(request.CompanyLogo)

            };

            _logic.Update(new CompanyProfilePoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteCompanyProfilePayLoad(CompanyProfilePayLoad request, ServerCallContext context)
        {
            CompanyProfilePoco poco = new CompanyProfilePoco()
            {
                Id = new Guid(request.Id),
                RegistrationDate = request.RegistrationDate.ToDateTime(),
                CompanyWebsite = request.CompanyWebsite,
                ContactPhone = request.ContactPhone,
                ContactName = request.ContactName,
                CompanyLogo = Encoding.ASCII.GetBytes(request.CompanyLogo)


            };

            _logic.Delete(new CompanyProfilePoco[] { poco });


            return null;
        }
    }
}
