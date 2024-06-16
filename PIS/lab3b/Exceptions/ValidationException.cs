using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace lab3b_vd.Exceptions;

public class ValidationException : Exception
{
    public IEnumerable<string> ValidationErrors { get; init; }


    public ValidationException(ModelStateDictionary modelState) : base()
    {
        ValidationErrors = prepareAnswer(modelState);
    }

    private static IEnumerable<string> prepareAnswer(ModelStateDictionary modelState)
    {
        var values = modelState.Values;
        var list = new List<string>();
        var error = string.Empty;

        foreach (var v in values)
            list.AddRange(v.Errors.Select(e => e.ErrorMessage));

        return list;
    }
}
