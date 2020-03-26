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
using static CareerCloud.gRPC.Protos.ApplicantWorkHistory;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantWorkHistoryService : ApplicantWorkHistoryBase
    {
        private readonly ApplicantWorkHistoryLogic _logic;

        public ApplicantWorkHistoryService() 
        {
            _logic = new ApplicantWorkHistoryLogic(new EFGenericRepository<ApplicantWorkHistoryPoco>());
        }

        public override Task<ApplicantWorkHistoryPayLoad> ReadApplicantWorkHistory(IdRequestAppWorkHistory request, ServerCallContext context)
        {
            ApplicantWorkHistoryPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<ApplicantWorkHistoryPayLoad>(
                () => new ApplicantWorkHistoryPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    CompanyName = poco.CompanyName,
                    CountryCode = poco.CountryCode,
                    Location = poco.Location,
                    JobTitle = poco.JobTitle,
                    JobDescription = poco.JobDescription,
                    StartMonth = poco.StartMonth,
                    StartYear = poco.StartYear,
                    EndMonth = poco.EndMonth,
                    EndYear = poco.EndYear
                }
            );
        }

        public override Task<Empty> CreateApplicantWorkHistory(ApplicantWorkHistoryPayLoad request, ServerCallContext context)
        {
            ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco()
            {
                Id = new Guid(request.Id),
                Applicant = new Guid(request.Applicant),
                CompanyName = request.CompanyName,
                CountryCode = request.CountryCode,
                Location = request.Location,
                JobTitle = request.JobTitle,
                JobDescription = request.JobDescription,
                StartMonth = (short)request.StartMonth,
                StartYear = request.StartYear,
                EndMonth = (short)request.EndMonth,
                EndYear = request.EndYear
            };

            _logic.Add(new ApplicantWorkHistoryPoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateApplicantWorkHistory(ApplicantWorkHistoryPayLoad request, ServerCallContext context) 
        {
            ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco()
            {
                Id = new Guid(request.Id),
                Applicant = new Guid(request.Applicant),
                CompanyName = request.CompanyName,
                CountryCode = request.CountryCode,
                Location = request.Location,
                JobTitle = request.JobTitle,
                JobDescription = request.JobDescription,
                StartMonth = (short)request.StartMonth,
                StartYear = request.StartYear,
                EndMonth = (short)request.EndMonth,
                EndYear = request.EndYear

            };

            _logic.Update(new ApplicantWorkHistoryPoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteApplicantWorkHistory(ApplicantWorkHistoryPayLoad request, ServerCallContext context)
        {
            ApplicantWorkHistoryPoco poco = new ApplicantWorkHistoryPoco()
            {
                Id = new Guid(request.Id),
                Applicant = new Guid(request.Applicant),
                CompanyName = request.CompanyName,
                CountryCode = request.CountryCode,
                Location = request.Location,
                JobTitle = request.JobTitle,
                JobDescription = request.JobDescription,
                StartMonth = (short)request.StartMonth,
                StartYear = request.StartYear,
                EndMonth = (short)request.EndMonth,
                EndYear = request.EndYear

            };

            _logic.Delete(new ApplicantWorkHistoryPoco[] { poco });


            return null;
        }
    }
}
