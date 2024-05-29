using FluentValidation;
using UserModule.Request;

namespace Api.RequestValidator.User
{
    public class UserRegisterValidator : AbstractValidator<UserRegisterReq>
    {
        public UserRegisterValidator()
        {
            //CascadeMode = CascadeMode.Continue;
            RuleFor(x => x.Email)
                .Cascade(CascadeMode.Continue)
                //.MinimumLength(10).WithMessage("panjang minimum Email")
                .NotNull().NotEmpty().WithMessage("Email gak boleh kosong");

            RuleFor(x => x.Fullname)
                .Cascade(CascadeMode.StopOnFirstFailure)
                //.MinimumLength(10).WithMessage("panjang minimum Fullname")
                .NotNull().NotEmpty().WithMessage("User Fullname gak boleh kosong");

        }
    }
}
