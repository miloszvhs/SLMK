using FluentValidation;

namespace SystemLotowMK.Application.Dto;

public class PaymentDto
{
    
}

public class PaymentDtoValidator : AbstractValidator<PaymentDto>
{
    public PaymentDtoValidator()
    {
        RuleFor(x => x).NotNull();
    }
}