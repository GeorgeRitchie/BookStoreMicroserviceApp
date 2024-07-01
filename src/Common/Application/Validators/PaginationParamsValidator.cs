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

using Application.Models;
using FluentValidation;

namespace Application.Validators
{
	/// <summary>
	/// Represents the <see cref="PaginationParams"/> validator.
	/// </summary>
	public class PaginationParamsValidator : AbstractValidator<PaginationParams>
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="PaginationParamsValidator"/> class.
		/// </summary>
		public PaginationParamsValidator()
		{
			RuleFor(i => i.PageNumber).Custom((number, context) =>
			{
				if (number < 1)
					context.InstanceToValidate.PageNumber = 1;
				else
				{
					var temp = (number - 1) * context.InstanceToValidate.PageSize;
					if (temp >= int.MaxValue || temp < 0)
					{
						context.InstanceToValidate.PageNumber = 1;
						context.InstanceToValidate.PageSize = 10;
					}
				}
			});

			RuleFor(i => i.PageSize).Custom((size, context) =>
			{
				if (size < 1)
					context.InstanceToValidate.PageSize = 10;
				else
				{
					var temp = size * (context.InstanceToValidate.PageNumber - 1);
					if (temp >= int.MaxValue || temp < 0)
					{
						context.InstanceToValidate.PageNumber = 1;
						context.InstanceToValidate.PageSize = 10;
					}
				}
			});
		}
	}
}
