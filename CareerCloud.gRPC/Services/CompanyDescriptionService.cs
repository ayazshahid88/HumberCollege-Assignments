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
using static CareerCloud.gRPC.Protos.CompanyDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyDescriptionService : CompanyDescriptionBase
    {
        private readonly CompanyDescriptionLogic _logic;

        public CompanyDescriptionService() 
        {
            _logic = new CompanyDescriptionLogic(new EFGenericRepository<CompanyDescriptionPoco>());
        }

        public override Task<CompanyDescriptionPayLoad> ReadCompanyDescription(IdRequestCompDesc request, ServerCallContext context) 
        {
            CompanyDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<CompanyDescriptionPayLoad>(
                () => new CompanyDescriptionPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    LanguageId = poco.LanguageId,
                    CompanyName = poco.CompanyName,
                    CompanyDescription = poco.CompanyDescription
                }
            );
        }

        public override Task<Empty> CreateCompanyDescription(CompanyDescriptionPayLoad request, ServerCallContext context)
        {
            CompanyDescriptionPoco poco = new CompanyDescriptionPoco()
            {
                Id = new Guid(request.Id),
                Company = new Guid(request.Company),
                LanguageId = request.LanguageId,
                CompanyName = request.CompanyName,
                CompanyDescription = request.CompanyDescription
            };

            _logic.Add(new CompanyDescriptionPoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateCompanyDescription(CompanyDescriptionPayLoad request, ServerCallContext context)
        {
            CompanyDescriptionPoco poco = new CompanyDescriptionPoco()
            {
                Id = new Guid(request.Id),
                Company = new Guid(request.Company),
                LanguageId = request.LanguageId,
                CompanyName = request.CompanyName,
                CompanyDescription = request.CompanyDescription

            };

            _logic.Update(new CompanyDescriptionPoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteCompanyDescription(CompanyDescriptionPayLoad request, ServerCallContext context)
        {
            CompanyDescriptionPoco poco = new CompanyDescriptionPoco()
            {
                Id = new Guid(request.Id),
                Company = new Guid(request.Company),
                LanguageId = request.LanguageId,
                CompanyName = request.CompanyName,
                CompanyDescription = request.CompanyDescription

            };

            _logic.Delete(new CompanyDescriptionPoco[] { poco });


            return null;
        }

    }
}
