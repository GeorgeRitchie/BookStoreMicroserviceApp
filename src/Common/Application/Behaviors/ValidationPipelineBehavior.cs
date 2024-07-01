/* 
	BookStore
	Copyright (c) 2024, Sharifjon Abdulloev.

	This program is free software: you can redistribute it and/or modify
	it under the terms of the GNU General Public License, version 3.0, 
	as published by the Free Software Foundation.

	This program is distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the
	GNU General Public License, version 3.0, for more details.

	You should have received a copy of the GNU General Public License
	along with this program.  If not, see <http://www.gnu.org/licenses/>.
*/

using Application.Infrastructure;
using FluentValidation;
using MediatR;
using Shared.Errors;
using Shared.Results;

namespace Application.Behaviors
{
	/// <summary>
	/// Represents the validation pipeline behavior.
	/// </summary>
	/// <typeparam name="TRequest">The request type.</typeparam>
	/// <typeparam name="TResponse">The response type.</typeparam>
	/// <remarks>
	/// Initializes a new instance of the <see cref="ValidationPipelineBehavior{TRequest,TResponse}"/> class.
	/// </remarks>
	/// <param name="validators">The validators for the given request.</param>
	public sealed class ValidationPipelineBehavior<TRequest, TResponse>(IEnumerable<IValidator<TRequest>> validators)
		: IPipelineBehavior<TRequest, TResponse>
			where TRequest : IRequest<TResponse>
			where TResponse : Result
	{

		/// <inheritdoc />
		public async Task<TResponse> Handle(TRequest request,
											RequestHandlerDelegate<TResponse> next,
											CancellationToken cancellationToken)
		{
			if (validators.Any())
			{
				Error[] errors = await ValidateAsync(new ValidationContext<TRequest>(request), cancellationToken);

				if (errors.Length != 0)
					return ValidationResultFactory.Create<TResponse>(errors);
			}

			return await next();
		}

		private async Task<Error[]> ValidateAsync(IValidationContext validationContext,
													CancellationToken cancellationToken = default)
			=> (await Task.WhenAll(validators.Select(validator
														=> validator.ValidateAsync(validationContext, cancellationToken))))
				.SelectMany(validationResult => validationResult.Errors)
				.Where(validationFailure => validationFailure is not null)
				.Select(validationFailure => new Error(validationFailure.ErrorCode, validationFailure.ErrorMessage))
				.Distinct()
				.ToArray();
	}
}
