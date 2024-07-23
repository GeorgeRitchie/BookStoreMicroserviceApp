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

namespace Service.CatalogWrite.Application.Common.Validators
{
    /// <summary>
    /// Represents valid image file validation rule for classes implementing <see cref="IFile"/>.
    /// </summary>
    public sealed class PhotoFileValidator : AbstractValidator<IFile>
    {
        public PhotoFileValidator(IFileManager fileManager, Func<string, Error>? invalidFileErrorFactory)
        {
            RuleFor(file => file)
                .Custom((file, context) =>
                {
                    if (fileManager.IsPhoto(file) == false)
                    {
                        var error = invalidFileErrorFactory(file.FileName);
                        context.AddFailure(new ValidationFailure(context.PropertyPath, error.Message)
                        {
                            ErrorCode = error.Code,
                        });
                    }
                });
        }
    }
}
