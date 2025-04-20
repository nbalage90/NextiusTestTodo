namespace NexiusTestTodo.API.Validation;

public class InputValidator<T, TValidator>
    where TValidator : IValidator<T>
{
    public void Validate(T obj)
    {
        var validator = Activator.CreateInstance<TValidator>();
        var validationResult = validator.Validate(obj);

        if (validationResult.Errors.Count > 0)
        {
            throw new ValidationException(validationResult.Errors);
        }
    }
}
