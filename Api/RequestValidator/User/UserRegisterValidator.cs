using Api.Request;
using FluentValidation;

namespace Api.RequestValidator.User
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterReq>
    {
        public UserRegisterValidator()
        {
            CascadeMode = CascadeMode.Continue;
            RuleFor(x => x.Email).NotNull().NotEmpty().WithMessage("Email gak boleh kosong");
            RuleFor(x => x.Email).MinimumLength(10).WithMessage("panjang minimum Email");

            RuleFor(x => x.Fullname).MinimumLength(10).WithMessage("panjang minimum Fullname");
            RuleFor(x => x.Fullname).NotNull().NotEmpty().WithMessage("User Fullname gak boleh kosong");

        }
    }
}
