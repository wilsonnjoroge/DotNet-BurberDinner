
using ErrorOr;
using FluentValidation;
using MediatR;

namespace BurberDinner.Application.Common.Behaviors
{
    public class ValidationBehaviour<TRequest,TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : IErrorOr
    {
      private readonly IValidator<TRequest>? _validator =   null;

        public ValidationBehaviour(IValidator<TRequest>? validator)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(
          TRequest request,
          RequestHandlerDelegate<TResponse> next,
          CancellationToken cancellationToken)
        {
          var validationResult = await _validator.ValidateAsync(request, cancellationToken);

          if(validationResult.IsValid)
          {
            return await next();
          }

          var errors = validationResult.Errors.ToList().ConvertAll(
            validationFailure => Error.Validation(
            validationFailure.PropertyName, 
            validationFailure.ErrorMessage)
          );

          return (dynamic)errors;
        }
    }
}
