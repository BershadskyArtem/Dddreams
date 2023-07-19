using Dddreams.Application.Common.Exceptions;
using FluentValidation;
using MediatR;
using ApplicationException = System.ApplicationException;

namespace Dddreams.Application.Behaviours;

public class ValidationPipelineBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;
    
    public ValidationPipelineBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var errors = _validators.Select(v => v.Validate(request))
                .SelectMany(validationResult => validationResult.Errors)
                .Where(error => error != null)
                .Select(validationResult =>
                    $"Error code: {validationResult.PropertyName}. Error message: {validationResult.ErrorMessage}")
                .ToArray();

            if (errors.Any())
                throw new BadRequestException(errors);
        }

        try
        {
            return await next();
        }
        catch(ApplicationException)
        {
            throw;
        }
        catch (Exception)
        {
            //Log
            throw new Common.Exceptions.ApplicationException("Internal error.");
        }
        
        
    }
}