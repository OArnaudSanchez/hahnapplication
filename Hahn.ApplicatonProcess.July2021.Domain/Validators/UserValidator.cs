using FluentValidation;
using Hahn.ApplicatonProcess.July2021.Domain.DTOs;

namespace Hahn.ApplicatonProcess.July2021.Domain.Validators
{
    public class UserValidator : AbstractValidator<UserDTO>
    {
        public UserValidator()
        {
            RuleFor(x => x.Age).NotNull().NotEmpty().GreaterThan(18).WithMessage("The user Must be Greater than 18 years");
            RuleFor(x => x.FirstName).NotNull().NotEmpty().MinimumLength(3).WithMessage("FirstName At least 3 characters");
            RuleFor(x => x.LastName).NotNull().NotEmpty().MinimumLength(3).WithMessage("LastName At least 3 characters");
            RuleFor(x => x.AssetName).NotNull().NotEmpty();
            RuleFor(x => x.Email).NotNull().NotEmpty().Matches("[a-z0-9!#$%&'+/=?^_`{|}~-]+(?:\\.[a-z0-9!#$%&'+/=?^_`{|}~-]+)@(?:[a-z0-9](?:[a-z0-9-][a-z0-9])?\\.)+(?:[A-Z]{2}|com|org|net|gov|biz|info|mobi|name|aero|jobs|museum|tv)\\b").WithMessage("Email must be an valid Email With a valid top level domain");
            RuleFor(x => x.Address).NotNull().NotEmpty().Matches("#?(\\d*)\\s?[\\,]?((?:[\\w+\\s*\\-])+)[\\,]\\s?([a-zA-Z]+)\\s?[\\,]?([0-9a-zA-Z]+)-?([0-9]+)")
                .WithMessage("Valid Address: " +
                "House Number, Street, Country Code And Zip Code " +
                "EXAMPLE: #8400, NW 25th Street Suite 100, FL 33198-1534 " +
                "OR: #8400 NW 25th Street Suite 100 FL 33198-1534 " +
                "OR: 8400, NW 25th Street Suite 100, FL 33198-1534 " +
                "OR: 8400 NW 25th Street Suite 100 FL 33198-1534");
        }
    }
}
