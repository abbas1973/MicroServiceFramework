﻿using FluentValidation;
using MediatR;
using ValidationException = Application.Exceptions.ValidationException;
namespace Application.Behaviors
{
    /// <summary>
    /// پایپلاین ولیدیشن خودکار با فلوئنت ولیدیشن
    /// </summary>
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : class, IRequest<TResponse>
    {

        private readonly IEnumerable<IValidator<TRequest>> _validators;
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators) => _validators = validators;
        
        
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (!_validators.Any())
                return await next();

            var context = new ValidationContext<TRequest>(request);
            var failures = _validators
               .Select(v => v.Validate(context))
               .SelectMany(result => result.Errors)
               .Where(f => f != null)
               .ToList();

            if (failures.Count != 0)
            {
                throw new ValidationException(failures);
            }

            return await next();
        }

    }
}
