using FluentValidation;
using FluentValidation.Results;

namespace Carglass.TechnicalAssessment.Backend.BL;

public static class ValidatorExtensions
{
    public static void ValidateAndShowIfAnyError<TDto>(this IValidator<TDto> validator, TDto dto)
    {
        if (validator.Validate(dto) is ValidationResult result && result.Errors.Any())
        {
            string toShowErrors = string.Join("; ", result.Errors.Select(s => s.ErrorMessage));
            throw new Exception($"No cumple los requisitos de validación. Errores: '{toShowErrors}'");
        }
    }
}