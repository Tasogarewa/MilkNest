using FluentValidation;
using MediatR;
using Microsoft.Extensions.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MilkNest.Application.Common.Behaviors
{
    public class LocalizationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    {
        private readonly IStringLocalizer _localizer;

        public LocalizationBehavior(IStringLocalizer localizer)
        {
            _localizer = localizer;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
               
                var response = await next();

                return response;
            }
            catch (ValidationException ex)
            {
                var localizedErrors = ex.Errors.Select(error => _localizer[error.ErrorMessage]);
                throw new ValidationException(localizedErrors.ToString());
            }
            catch (Exception ex)
            {
                
                var localizedMessage = _localizer["An unexpected error occurred"];
                throw new Exception(localizedMessage, ex);
            }
        }
    }
}
