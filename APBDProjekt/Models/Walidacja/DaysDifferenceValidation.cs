using System.ComponentModel.DataAnnotations;

namespace APBDProjekt.Models.Walidacja;

public class DaysDifferenceValidation(string startDatePropertyName, int minDays = 3, int maxDays = 30)
    : ValidationAttribute
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
            return new ValidationResult("Data końcowa nie może być pusta.");

        var endDate = (DateTime) value;
            
        var property = validationContext.ObjectType.GetProperty(startDatePropertyName);
        if (property == null)
        {
            return new ValidationResult($"Nie znaleziono właściwości {startDatePropertyName}");
        }
        
        var startDate = (DateTime) property.GetValue(validationContext.ObjectInstance);
            
        var daysDifference = (endDate - startDate).TotalDays;

        if (daysDifference < minDays || daysDifference > maxDays)
        {
            return new ValidationResult("Przedział czasowy musi wynosić od " + minDays + " do " + maxDays +" dni.");
        }

        return ValidationResult.Success;

    }

}