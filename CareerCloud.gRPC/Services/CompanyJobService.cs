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
using static CareerCloud.gRPC.Protos.CompanyJob;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobService : CompanyJobBase
    {
        private readonly CompanyJobLogic _logic;

        public CompanyJobService()
        {
            _logic = new CompanyJobLogic(new EFGenericRepository<CompanyJobPoco>());
        }

        public override Task<CompanyJobPayLoad> ReadCompanyJob(IdRequestCompJob request, ServerCallContext context)
        {
            CompanyJobPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<CompanyJobPayLoad>(
                () => new CompanyJobPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Company = poco.Company.ToString(),
                    ProfileCreated = Timestamp.FromDateTime((DateTime)poco.ProfileCreated),
                    IsInactive = poco.IsInactive,
                    IsCompanyHidden = poco.IsCompanyHidden
                }
            );
        }

        public override Task<Empty> CreateCompanyJob(CompanyJobPayLoad request, ServerCallContext context) 
        {
            CompanyJobPoco poco = new CompanyJobPoco()
            {
                Id = new Guid(request.Id),
                Company = new Guid(request.Id),
                ProfileCreated = request.ProfileCreated.ToDateTime(),
                IsInactive = request.IsInactive,
                IsCompanyHidden = request.IsCompanyHidden

            };

            _logic.Add(new CompanyJobPoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateCompanyJob(CompanyJobPayLoad request, ServerCallContext context)
        {
            CompanyJobPoco poco = new CompanyJobPoco()
            {
                Id = new Guid(request.Id),
                Company = new Guid(request.Id),
                ProfileCreated = request.ProfileCreated.ToDateTime(),
                IsInactive = request.IsInactive,
                IsCompanyHidden = request.IsCompanyHidden

            };

            _logic.Update(new CompanyJobPoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteCompanyJob(CompanyJobPayLoad request, ServerCallContext context)
        {
            CompanyJobPoco poco = new CompanyJobPoco()
            {
                Id = new Guid(request.Id),
                Company = new Guid(request.Id),
                ProfileCreated = request.ProfileCreated.ToDateTime(),
                IsInactive = request.IsInactive,
                IsCompanyHidden = request.IsCompanyHidden


            };

            _logic.Delete(new CompanyJobPoco[] { poco });


            return null;
        }

    }
}
