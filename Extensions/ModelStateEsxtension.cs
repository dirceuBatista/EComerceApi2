using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace LivrariaApi.Extensions;

public static class ModelStateEsxtension
{
    public static List<string> GetErrors(this ModelStateDictionary modelState)
    {
        var result = new List<string>();
        foreach (var item in modelState.Values)
            result.AddRange(
                item.Errors.Select(error =>error.ErrorMessage ));

        return result;
    }
}