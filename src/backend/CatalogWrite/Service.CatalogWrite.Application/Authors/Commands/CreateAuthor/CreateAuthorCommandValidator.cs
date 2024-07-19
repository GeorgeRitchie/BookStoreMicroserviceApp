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

namespace Service.CatalogWrite.Application.Authors.Commands.CreateAuthor
{
    /// <summary>
    /// Represents the <see cref="CreateAuthorCommand"/> validator.
    /// </summary>
    internal sealed class CreateAuthorCommandValidator : AbstractValidator<CreateAuthorCommand>
    {
        public CreateAuthorCommandValidator(IFileManager fileManager)
        {
            RuleFor(a => a.FirstName)
                .NotEmpty()
                .WithError(AuthorErrors.PropertyIsRequired(nameof(CreateAuthorCommand.FirstName)));

            RuleFor(a => a.LastName)
                .NotEmpty()
                .WithError(AuthorErrors.PropertyIsRequired(nameof(CreateAuthorCommand.LastName)));

            RuleFor(c => c.Icon)
                .NotEmpty()
                    .WithError(AuthorErrors.PropertyIsRequired(nameof(CreateAuthorCommand.Icon)))
                .Custom((icon, context) =>
                {
                    if (fileManager.IsPhoto(icon) == false)
                    {
                        var error = AuthorErrors.OnlyPhotoFileIsAllowed(icon.FileName);
                        context.AddFailure(new ValidationFailure(context.PropertyName, error.Message)
                        {
                            ErrorCode = error.Code,
                        });
                    }
                });

            RuleFor(c => c.Photo)
                .NotEmpty()
                    .WithError(AuthorErrors.PropertyIsRequired(nameof(CreateAuthorCommand.Photo)))
                .Custom((photo, context) =>
                {
                    if (fileManager.IsPhoto(photo) == false)
                    {
                        var error = AuthorErrors.OnlyPhotoFileIsAllowed(photo.FileName);
                        context.AddFailure(new ValidationFailure(context.PropertyName, error.Message)
                        {
                            ErrorCode = error.Code,
                        });
                    }
                });
        }
    }
}
