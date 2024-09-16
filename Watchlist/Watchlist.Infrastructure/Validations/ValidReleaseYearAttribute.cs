using System.ComponentModel.DataAnnotations;

public class ValidReleaseYearAttribute : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        if (value is int releaseYear)
        {
            int currentYear = DateTime.Now.Year;
            if (releaseYear >= 1980 && releaseYear <= currentYear)
            {
                return ValidationResult.Success;
            }
            else
            {
                return new ValidationResult($"The release year must be between 1980 and {currentYear}.");
            }
        }

        return new ValidationResult("Invalid release year format.");
    }
}
