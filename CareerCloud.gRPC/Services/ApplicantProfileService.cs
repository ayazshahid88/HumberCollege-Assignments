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
using static CareerCloud.gRPC.Protos.ApplicantProfile;

namespace CareerCloud.gRPC.Services
{
    public class ApplicantProfileService : ApplicantProfileBase
    {
        private readonly ApplicantProfileLogic _logic;

        public ApplicantProfileService()
        {
            _logic = new ApplicantProfileLogic(new EFGenericRepository<ApplicantProfilePoco>());
        }

        public override Task<ApplicantProfilePayLoad> ReadApplicantProfile(IdRequestAppProfile request, ServerCallContext context) 
        {
            ApplicantProfilePoco poco = _logic.Get(Guid.Parse(request.Id));

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<ApplicantProfilePayLoad>(
                () => new ApplicantProfilePayLoad()
                {
                    Id = poco.Id.ToString(),
                    Login = poco.Login.ToString(),
                    CurrentSalary =  ((double)poco.CurrentSalary),
                    CurrentRate = ((double)poco.CurrentRate),
                    Currency = poco.Currency,
                    Country = poco.Country,
                    Province = poco.Province,
                    Street = poco.Street,
                    City = poco.City,
                    PostalCode = poco.PostalCode
                }
            );
        }

        public override Task<Empty> CreateApplicantProfile(ApplicantProfilePayLoad request, ServerCallContext context) 
        {
            ApplicantProfilePoco poco = new ApplicantProfilePoco()
            {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                CurrentSalary = (decimal)request.CurrentSalary,
                CurrentRate = (decimal)request.CurrentRate,
                Currency = request.Currency,
                Country = request.Country,
                Province = request.Province,
                Street = request.Street,
                City = request.City, 
                PostalCode = request.PostalCode
            };

            _logic.Add(new ApplicantProfilePoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateApplicantProfile(ApplicantProfilePayLoad request, ServerCallContext context)
        {
            ApplicantProfilePoco poco = new ApplicantProfilePoco()
            {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                CurrentSalary = (decimal)request.CurrentSalary,
                CurrentRate = (decimal)request.CurrentRate,
                Currency = request.Currency,
                Country = request.Country,
                Province = request.Province,
                Street = request.Street,
                City = request.City,
                PostalCode = request.PostalCode

            };

            _logic.Update(new ApplicantProfilePoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteApplicantProfile(ApplicantProfilePayLoad request, ServerCallContext context) 
        {
            ApplicantProfilePoco poco = new ApplicantProfilePoco()
            {
                Id = new Guid(request.Id),
                Login = new Guid(request.Login),
                CurrentSalary = (decimal)request.CurrentSalary,
                CurrentRate = (decimal)request.CurrentRate,
                Currency = request.Currency,
                Country = request.Country,
                Province = request.Province,
                Street = request.Street,
                City = request.City,
                PostalCode = request.PostalCode

            };

            _logic.Delete(new ApplicantProfilePoco[] { poco });


            return null;
        }

    }
}
