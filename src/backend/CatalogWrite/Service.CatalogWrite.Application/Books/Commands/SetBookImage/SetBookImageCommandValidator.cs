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

using Service.Catalog.Application.Common.Services;
using Service.Catalog.Application.Common.Validators;

namespace Service.Catalog.Application.Books.Commands.SetBookImage
{
	/// <summary>
	/// Represents the <see cref="SetBookImageCommand"/> validator.
	/// </summary>
	internal sealed class SetBookImageCommandValidator : AbstractValidator<SetBookImageCommand>
	{
		public SetBookImageCommandValidator(IFileManager fileManager)
		{
			var photoFileValidator = new PhotoFileValidator(fileManager, BookErrors.OnlyPhotoFileIsAllowed);

			When(a => a.Icon is not null, () =>
				RuleFor(a => a.Icon).SetValidator(photoFileValidator)
			);

			When(a => a.Cover is not null, () =>
				RuleFor(a => a.Cover).SetValidator(photoFileValidator)
			);

			When(a => a.Previews?.Any() == true, () =>
				RuleForEach(a => a.Previews!).SetValidator(photoFileValidator)
			);
		}
	}
}
