using System.ComponentModel.DataAnnotations;
using System.Linq;

public static class ValidationHelper
{
    public static Dictionary<string, List<string>> Validate<T>(T model)
    {
        var results = new List<ValidationResult>();
        var context = new ValidationContext(model);

        Validator.TryValidateObject(model, context, results, true);

        var errors = new Dictionary<string, List<string>>();

        foreach (var validationResult in results)
        {
            foreach (var member in validationResult.MemberNames)
            {
                if (!errors.ContainsKey(member))
                {
                    errors[member] = new List<string>();
                }

                errors[member].Add(validationResult.ErrorMessage ?? "Validation error");
            }
        }

        return errors;
    }
}