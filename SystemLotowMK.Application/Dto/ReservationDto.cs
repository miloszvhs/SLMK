using FluentValidation;

namespace SystemLotowMK.Application.Dto;

public class ReservationDto
{
    
}

public class ReservationDtoValidator : AbstractValidator<ReservationDto>
{
    public ReservationDtoValidator()
    {
        RuleFor(x => x).NotNull();
    }
}