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
using static CareerCloud.gRPC.Protos.CompanyJobEducation;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobEducationService : CompanyJobEducationBase
    {
        private readonly CompanyJobEducationLogic _logic;

        public CompanyJobEducationService()
        {
            _logic = new CompanyJobEducationLogic(new EFGenericRepository<CompanyJobEducationPoco>());
        }

        public override Task<CompanyJobEducationPayLoad> ReadCompanyJobEducation(IdRequestCompJobEdu request, ServerCallContext context)
        {
            CompanyJobEducationPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<CompanyJobEducationPayLoad>(
                () => new CompanyJobEducationPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Job = poco.Job.ToString(),
                    Major = poco.Major,
                    Importance = poco.Importance
                }
            );
        }

        public override Task<Empty> CreateCompanyJobEducation(CompanyJobEducationPayLoad request, ServerCallContext context)
        {
            CompanyJobEducationPoco poco = new CompanyJobEducationPoco()
            {
                Id = new Guid(request.Id),
                Job = new Guid(request.Job),
                Major = request.Major,
                Importance = (short)request.Importance

            };

            _logic.Add(new CompanyJobEducationPoco[] { poco }); 

            return null;

        }

        public override Task<Empty> UpdateCompanyJobEducation(CompanyJobEducationPayLoad request, ServerCallContext context)
        {
            CompanyJobEducationPoco poco = new CompanyJobEducationPoco()
            {
                Id = new Guid(request.Id),
                Job = new Guid(request.Job),
                Major = request.Major,
                Importance = (short)request.Importance
            };

            _logic.Update(new CompanyJobEducationPoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteCompanyJobEducation(CompanyJobEducationPayLoad request, ServerCallContext context)
        {
            CompanyJobEducationPoco poco = new CompanyJobEducationPoco()
            {
                Id = new Guid(request.Id),
                Job = new Guid(request.Job),
                Major = request.Major,
                Importance = (short)request.Importance

            };

            _logic.Delete(new CompanyJobEducationPoco[] { poco });


            return null;
        }


    }
}
