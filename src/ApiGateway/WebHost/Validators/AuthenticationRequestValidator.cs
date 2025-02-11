// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.

using FluentValidation;
using WebHost.Dto.IAM;

namespace WebHost.Validators
{
    public class AuthenticationRequestValidator : AbstractValidator<AuthenticationRequestDto>
    {
        public AuthenticationRequestValidator()
        {
            RuleFor(x => x.Email).EmailAddress(FluentValidation.Validators.EmailValidationMode.AspNetCoreCompatible);
            RuleFor(p => p.Password).NotEmpty().WithMessage("Your password cannot be empty");
        }
    }
}
