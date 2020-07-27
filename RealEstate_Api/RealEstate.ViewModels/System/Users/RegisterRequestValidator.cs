using FluentValidation;
using System;
using System.Collections.Generic;
using System.Text;

namespace RealEstate.ViewModels.System.Users
{
    public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
    {
        public RegisterRequestValidator()
        {
            RuleFor(x => x.FullName).NotEmpty().WithMessage("Họ tên không được để trống")
                .MaximumLength(200).WithMessage("Họ tên không quá 200 ký tự");

            RuleFor(x => x.UserName).NotEmpty().WithMessage("Email không được để trống")
                .Matches(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$")
                .WithMessage("Email không hợp lệ");
        }
    }
}
