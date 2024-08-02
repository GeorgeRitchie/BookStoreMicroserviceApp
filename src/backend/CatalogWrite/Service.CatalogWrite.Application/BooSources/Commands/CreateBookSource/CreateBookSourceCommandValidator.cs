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

using Service.Catalog.Domain.BookSources;

namespace Service.Catalog.Application.BooSources.Commands.CreateBookSource
{
    /// <summary>
    /// Represents the <see cref="CreateBookSourceCommand"/> validator.
    /// </summary>
    internal sealed class CreateBookSourceCommandValidator : AbstractValidator<CreateBookSourceCommand>
    {
        public CreateBookSourceCommandValidator()
        {
            RuleFor(i => i.Format)
                .NotNull()
                    .WithError(BookSourceErrors.InvalidBookFormat)
                .Must(BookFormat.Contains)
                    .WithError(BookSourceErrors.InvalidBookFormat);
        }
    }
}
