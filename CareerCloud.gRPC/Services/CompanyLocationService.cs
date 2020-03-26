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
using static CareerCloud.gRPC.Protos.CompanyLocation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyLocationService : CompanyLocationBase
    {
        private readonly CompanyLocationLogic _logic;

        public CompanyLocationService() 
        {
            _logic = new CompanyLocationLogic(new EFGenericRepository<CompanyLocationPoco>());
        }

        public override Task<CompanyLocationPayLoad> ReadCompanyLocation(IdRequestCompLocation request, ServerCallContext context) 
        {
            CompanyLocationPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<CompanyLocationPayLoad>(
                () => new CompanyLocationPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    CountryCode = poco.CountryCode,
                    Province = poco.Province,
                    Street = poco.Street,
                    City = poco.Street,
                    PostalCode = poco.PostalCode
                }
            );
        }

        public override Task<Empty> CreateCompanyLocation(CompanyLocationPayLoad request, ServerCallContext context)
        {
            CompanyLocationPoco poco = new CompanyLocationPoco()
            {
                Id = new Guid(request.Id),
                Company = new Guid(request.Company),
                CountryCode = request.CountryCode,
                Province = request.Province,
                Street = request.Street,
                City = request.Street,
                PostalCode = request.PostalCode
            };

            _logic.Add(new CompanyLocationPoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateCompanyLocation(CompanyLocationPayLoad request, ServerCallContext context)
        {
            CompanyLocationPoco poco = new CompanyLocationPoco()
            {
                Id = new Guid(request.Id),
                Company = new Guid(request.Company),
                CountryCode = request.CountryCode,
                Province = request.Province,
                Street = request.Street,
                City = request.Street,
                PostalCode = request.PostalCode

            };

            _logic.Update(new CompanyLocationPoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteCompanyLocation(CompanyLocationPayLoad request, ServerCallContext context) 
        {
            CompanyLocationPoco poco = new CompanyLocationPoco()
            {
                Id = new Guid(request.Id),
                Company = new Guid(request.Company),
                CountryCode = request.CountryCode,
                Province = request.Province,
                Street = request.Street,
                City = request.Street,
                PostalCode = request.PostalCode

            };

            _logic.Delete(new CompanyLocationPoco[] { poco });


            return null;
        }


    }
}
