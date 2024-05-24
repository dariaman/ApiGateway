using Api.Request;
using FluentValidation;

namespace Api.RequestValidator.User
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterReq>
    {
        public UserRegisterValidator()
        {
            RuleFor(x => x.Fullname).NotNull().NotEmpty().WithMessage("User Fullname gak boleh kosong");
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email gak boleh kosong");

        }
    }
}
