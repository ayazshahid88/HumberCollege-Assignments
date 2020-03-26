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
using static CareerCloud.gRPC.Protos.CompanyJobDescription;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobDescription : CompanyJobDescriptionBase
    {

        private readonly CompanyJobDescriptionLogic _logic;

        public CompanyJobDescription()
        {
            _logic = new CompanyJobDescriptionLogic(new EFGenericRepository<CompanyJobDescriptionPoco>());
        }

        public override Task<CompanyJobDescriptionPayLoad> ReadCompanyJobDescription(IdRequestCompJobDesc request, ServerCallContext context)
        {
            CompanyJobDescriptionPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<CompanyJobDescriptionPayLoad>(
                () => new CompanyJobDescriptionPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Job = poco.Job.ToString(),
                    JobName = poco.JobName,
                    JobDescriptions = poco.JobDescriptions
                }
            );
        }

        public override Task<Empty> CreateCompanyJobDescription(CompanyJobDescriptionPayLoad request, ServerCallContext context)
        {
            CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco()
            {
                Id = new Guid(request.Id),
                Job = new Guid(request.Job),
                JobName = request.JobName,
                JobDescriptions = request.JobDescriptions
            };

            _logic.Add(new CompanyJobDescriptionPoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateCompanyJobDescription(CompanyJobDescriptionPayLoad request, ServerCallContext context)
        {
            CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco()
            {
                Id = new Guid(request.Id),
                Job = new Guid(request.Job),
                JobName = request.JobName,
                JobDescriptions = request.JobDescriptions

            };

            _logic.Update(new CompanyJobDescriptionPoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteCompanyJobDescription(CompanyJobDescriptionPayLoad request, ServerCallContext context)
        {
            CompanyJobDescriptionPoco poco = new CompanyJobDescriptionPoco()
            {
                Id = new Guid(request.Id),
                Job = new Guid(request.Job),
                JobName = request.JobName,
                JobDescriptions = request.JobDescriptions

            };

            _logic.Delete(new CompanyJobDescriptionPoco[] { poco });


            return null;
        }

    }
}
