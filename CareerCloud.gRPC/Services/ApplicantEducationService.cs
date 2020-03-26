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
using static CareerCloud.gRPC.Protos.ApplicantEducation;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantEducationService : ApplicantEducationBase
    {
        private readonly ApplicantEducationLogic _logic;

        public ApplicantEducationService()
        {
            _logic = new ApplicantEducationLogic(new EFGenericRepository<ApplicantEducationPoco>());
        }

        public override Task<ApplicantEducationPayLoad> ReadApplicantEducation(IdRequestAppEdu request, ServerCallContext context)
        {
            ApplicantEducationPoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<ApplicantEducationPayLoad>(
                () => new ApplicantEducationPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Applicant = poco.Applicant.ToString(),
                    CertificateDiploma = poco.CertificateDiploma,
                    CompletionDate = poco.CompletionDate is null ? null : Timestamp.FromDateTime((DateTime) poco.CompletionDate),
                    CompletionPercent = poco.CompletionPercent is null ? 0 : (int)poco.CompletionPercent,
                    Major = poco.Major,
                    StartDate = poco.StartDate is null ? null : Timestamp.FromDateTime((DateTime)poco.StartDate)

                }
            );
        }

        public override Task<Empty> CreateApplicantEducation(ApplicantEducationPayLoad request, ServerCallContext context)
        {
            ApplicantEducationPoco poco = new ApplicantEducationPoco()
            {
                Id = new Guid(request.Id),
                Applicant = new Guid(request.Applicant),
                Major = request.Major,
                CertificateDiploma = request.CertificateDiploma,
                StartDate = request.StartDate.ToDateTime(),
                CompletionDate = request.CompletionDate.ToDateTime(),
                CompletionPercent = (byte)request.CompletionPercent

            };

            _logic.Add(new ApplicantEducationPoco[] { poco });

            return null;
               
        }

        public override Task<Empty> UpdateApplicantEducation(ApplicantEducationPayLoad request, ServerCallContext context)
        {
            ApplicantEducationPoco poco = new ApplicantEducationPoco()
            {
                Id = new Guid(request.Id),
                Applicant = new Guid(request.Applicant),
                Major = request.Major,
                CertificateDiploma = request.CertificateDiploma,
                StartDate = request.StartDate.ToDateTime(),
                CompletionDate = request.CompletionDate.ToDateTime(),
                CompletionPercent = (byte)request.CompletionPercent,

            };

            _logic.Update(new ApplicantEducationPoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteApplicantEducation(ApplicantEducationPayLoad request, ServerCallContext context)
        {
            ApplicantEducationPoco poco = new ApplicantEducationPoco()
            {
                Id = new Guid(request.Id),
                Applicant = new Guid(request.Applicant),
                Major = request.Major,
                CertificateDiploma = request.CertificateDiploma,
                StartDate = request.StartDate.ToDateTime(),
                CompletionDate = request.CompletionDate.ToDateTime(),
                CompletionPercent = (byte)request.CompletionPercent,

            };

            _logic.Delete(new ApplicantEducationPoco[] { poco }); 


            return null;
        }


    }
}
