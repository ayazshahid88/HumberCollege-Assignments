using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System;
using System.Collections.Generic;
using System.Text;


namespace CareerCloud.BusinessLogicLayer
{
    public class CompanyProfileLogic : BaseLogic<CompanyProfilePoco>
    {
        public CompanyProfileLogic(IDataRepository<CompanyProfilePoco> repository)
            : base(repository)
        {

        }

        protected override void Verify(CompanyProfilePoco[] pocos)
        {
            List<ValidationException> exceptions = new List<ValidationException>();

            foreach (CompanyProfilePoco poco in pocos)
            {
                if (string.IsNullOrEmpty(poco.CompanyWebsite))
                {
                    exceptions.Add(new ValidationException(600, "Company Website field should not be blank."));
                }

               else if (!poco.CompanyWebsite.EndsWith(".com") && !poco.CompanyWebsite.EndsWith(".ca") && !poco.CompanyWebsite.EndsWith(".biz"))
                {
                    exceptions.Add(new ValidationException(600, "Valid websites must end with the \".ca\", \".com\", \".biz\"."));
                }

                if (string.IsNullOrEmpty(poco.ContactPhone))
                {
                    exceptions.Add(new ValidationException(601, "Phone number must be in a valid format (e.g. 416-555-1234)"));
                }

                else
                {
                    String[] phoneNumberElement = poco.ContactPhone.Split("-");
                    if (phoneNumberElement.Length != 3)
                    {
                        exceptions.Add(new ValidationException(601, "Phone Number must be in a valid format (e.g. 416-555-1234)"));
                    }

                    else if ((phoneNumberElement[0].Length != 3) || (phoneNumberElement[1].Length != 3) || (phoneNumberElement[2].Length != 4))
                    {
                        exceptions.Add(new ValidationException(601, "Phone Number must be in a valid format (e.g. 416-555-1234)"));
                    }
                }
            }
            if (exceptions.Count > 0)
            {
                throw new AggregateException(exceptions);
            }
        }
        public override void Add(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Add(pocos);
        }
        public override void Update(CompanyProfilePoco[] pocos)
        {
            Verify(pocos);
            base.Update(pocos);
        }
    }
}
