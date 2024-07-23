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

using Service.CatalogWrite.Application.Common.Services;
using Service.CatalogWrite.Application.Common.Validators;

namespace Service.CatalogWrite.Application.Authors.Commands.SetAuthorImage
{
    /// <summary>
    /// Represents the <see cref="SetAuthorImageCommand"/> validator.
    /// </summary>
    internal sealed class SetAuthorImageCommandValidator : AbstractValidator<SetAuthorImageCommand>
	{
		public SetAuthorImageCommandValidator(IFileManager fileManager)
		{
			var photoFileValidator = new PhotoFileValidator(fileManager, AuthorErrors.OnlyPhotoFileIsAllowed);

			When(a => a.Icon is not null, () =>
				RuleFor(a => a.Icon).SetValidator(photoFileValidator)
			);

			When(a => a.Photo is not null, () =>
				RuleFor(a => a.Photo).SetValidator(photoFileValidator)
			);

			When(a => a.Others?.Any() == true, () =>
				RuleForEach(a => a.Others!).SetValidator(photoFileValidator)
			);
		}
	}
}
