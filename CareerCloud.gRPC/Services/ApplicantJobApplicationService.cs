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
using static CareerCloud.gRPC.Protos.ApplicantJobApplication; 

namespace CareerCloud.gRPC.Services
{
    public class ApplicantJobApplicationService : ApplicantJobApplicationBase
    {
        private readonly ApplicantJobApplicationLogic _logic;

        public ApplicantJobApplicationService()
        {
            _logic = new ApplicantJobApplicationLogic(new EFGenericRepository<ApplicantJobApplicationPoco>());
        }

        public override Task<ApplicantJobApplicationPayLoad> ReadApplicantJobApplication(IdRequestAppJobApp request, ServerCallContext context)
        {
            ApplicantJobApplicationPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<ApplicantJobApplicationPayLoad>(
                () => new ApplicantJobApplicationPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    Job = poco.Job.ToString(),
                    ApplicationDate = Timestamp.FromDateTime((DateTime)poco.ApplicationDate)
                }
            );
        }

        public override Task<Empty> CreateApplicantJobApplication(ApplicantJobApplicationPayLoad request, ServerCallContext context)
        {
            ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco()
            {
                Id = new Guid(request.Id),
                Applicant = new Guid(request.Applicant),
                Job = new Guid(request.Job),
                ApplicationDate = request.ApplicationDate.ToDateTime()
            };

            _logic.Add(new ApplicantJobApplicationPoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateApplicantJobApplication(ApplicantJobApplicationPayLoad request, ServerCallContext context)
        {
            ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco()
            {
                Id = new Guid(request.Id),
                Applicant = new Guid(request.Applicant),
                Job = new Guid(request.Job),
                ApplicationDate = request.ApplicationDate.ToDateTime()

            };

            _logic.Update(new ApplicantJobApplicationPoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteApplicantJobApplication(ApplicantJobApplicationPayLoad request, ServerCallContext context)  
        {
            ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco()
            {
                Id = new Guid(request.Id),
                Applicant = new Guid(request.Applicant),
                Job = new Guid(request.Job),
                ApplicationDate = request.ApplicationDate.ToDateTime()
            };

            _logic.Delete(new ApplicantJobApplicationPoco[] { poco }); 


            return null;
        }
    }
}
