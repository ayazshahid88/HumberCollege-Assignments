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
using static CareerCloud.gRPC.Protos.CompanyJobSkill;

namespace CareerCloud.gRPC.Services
{
    public class CompanyJobSkillService : CompanyJobSkillBase
    {
        private readonly CompanyJobSkillLogic _logic;

        public CompanyJobSkillService() 
        {
            _logic = new CompanyJobSkillLogic(new EFGenericRepository<CompanyJobSkillPoco>());
        }

        public override Task<CompanyJobSkillPayLoad> ReadCompanyJobSkill(IdRequestCompJobSkill request, ServerCallContext context)
        {
            CompanyJobSkillPoco poco = _logic.Get(Guid.Parse(request.Id)); 

            if (poco is null)
            {
                throw new ArgumentOutOfRangeException("poco is null");
            }

            return new Task<CompanyJobSkillPayLoad>(
                () => new CompanyJobSkillPayLoad()
                {
                    Id = poco.Id.ToString(),
                    Job = poco.Job.ToString(),
                    Skill = poco.Skill,
                    SkillLevel = poco.SkillLevel,
                    Importance = poco.Importance
                }
            );
        }

        public override Task<Empty> CreateCompanyJobSkill(CompanyJobSkillPayLoad request, ServerCallContext context)
        {
            CompanyJobSkillPoco poco = new CompanyJobSkillPoco()
            {
                Id = new Guid(request.Id),
                Job = new Guid(request.Job),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                Importance = request.Importance

            };

            _logic.Add(new CompanyJobSkillPoco[] { poco });

            return null;

        }

        public override Task<Empty> UpdateCompanyJobSkill(CompanyJobSkillPayLoad request, ServerCallContext context)
        {
            CompanyJobSkillPoco poco = new CompanyJobSkillPoco()
            {
                Id = new Guid(request.Id),
                Job = new Guid(request.Job),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                Importance = request.Importance

            };

            _logic.Update(new CompanyJobSkillPoco[] { poco });

            return null;
        }

        public override Task<Empty> DeleteCompanyJobSkill(CompanyJobSkillPayLoad request, ServerCallContext context)
        {
            CompanyJobSkillPoco poco = new CompanyJobSkillPoco()
            {
                Id = new Guid(request.Id),
                Job = new Guid(request.Job),
                Skill = request.Skill,
                SkillLevel = request.SkillLevel,
                Importance = request.Importance

            };

            _logic.Delete(new CompanyJobSkillPoco[] { poco });


            return null;
        }

    }
}
