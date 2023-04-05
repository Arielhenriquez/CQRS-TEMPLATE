using FluentValidation;
using RBACV2.Application.UsersEntity.Commands;
using System.Text.RegularExpressions;

namespace RBACV2.Application.UsersEntity.Validators
{
    public class UpdateUserValidator : AbstractValidator<UpdateUserCommand>
    {
        const string expression = "[\\d$-/:-?{-~!\"^_`\\[\\]@#]+";
        readonly TimeSpan regexTimeout = TimeSpan.FromMinutes(5);
        public UpdateUserValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .NotNull()
                .Length(3, 50)
                .Custom((name, context) =>
                {
                    bool match = false;
                    try
                    {
                        match = Regex.IsMatch(name, expression, RegexOptions.None, regexTimeout);
                    }
                    catch (RegexMatchTimeoutException)
                    {
                        context.AddFailure("Regular expression timed out.");
                        return;
                    }

                    if (match) context.AddFailure("Invalid characters");
                });
        }
    }
}
