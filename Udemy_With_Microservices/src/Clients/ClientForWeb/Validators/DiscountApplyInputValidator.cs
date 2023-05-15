using ClientForWeb.DTOs;
using FluentValidation;

namespace ClientForWeb.Validators
{
    public class DiscountApplyInputValidator : AbstractValidator<DiscountsApplyInput>
    {
        public DiscountApplyInputValidator()
        {
            RuleFor(x => x.Code).NotEmpty().WithMessage("indirim kupon alanı boş olamaz");
        }
    }
}
