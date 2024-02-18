using FluentValidation;

namespace SystemLotowMK.Application.Dto;

public class FlightDto
{
    
}

public class FlightDtoValidator : AbstractValidator<FlightDto>
{
    public FlightDtoValidator()
    {
        RuleFor(x => x).NotNull();
    }
}