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

namespace Service.Catalog.Domain.ImageSources
{
	/// <summary>
	/// Represents the Image source entity.
	/// </summary>
	/// <typeparam name="TEnum">The enumeration type used to define image type.</typeparam>
	/// <typeparam name="TValue">The enumeration value (<see cref="Enumeration{TEnum, TEnumValue}"/>).</typeparam>
	public sealed class ImageSource<TEnum>
		: Entity<ImageSourceId> where TEnum : Enumeration<TEnum>
	{
		/// <summary>
		/// Gets the image source.
		/// </summary>
		public string Source { get; private set; }

		/// <summary>
		/// Gets the image type.
		/// </summary>
		public TEnum Type { get; private set; }

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageSource{TEnum}"/> class.
		/// </summary>
		/// <remarks>
		/// Required for deserialization.
		/// </remarks>
#pragma warning disable CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		private ImageSource()
#pragma warning restore CS8618 // Поле, не допускающее значения NULL, должно содержать значение, отличное от NULL, при выходе из конструктора. Возможно, стоит объявить поле как допускающее значения NULL.
		{
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="ImageSource{TEnum}"/> class.
		/// </summary>
		/// <param name="id">The image identifier.</param>
		/// <param name="source">The image source. Must not be <see langword="null"/> value.</param>
		/// <param name="type">The image type. Must not be <see langword="null"/> value.</param>
		/// <param name="isDeleted">The image deleted status marker (<see langword="true"/> - deleted, <see langword="false"/> - not deleted).</param>
		/// <exception cref="ArgumentNullException">Thrown if any param is <see langword="null"/>.</exception>
		public ImageSource(ImageSourceId id, string source, TEnum type, bool isDeleted = false) : base(id, isDeleted)
		{
			Source = source ?? throw new ArgumentNullException(nameof(source));
			Type = type ?? throw new ArgumentNullException(nameof(source));
		}

		/// <inheritdoc/>
		public override bool Equals(Entity<ImageSourceId>? other)
		{
			if (other is ImageSource<TEnum> _other)
				return base.Equals(other) || Source == _other.Source && Type == _other.Type;
			else
				return base.Equals(other);
		}

		/// <summary>
		/// Creates new instance of <see cref="ImageSource{TEnum}"/> based on parameters and validation result.
		/// </summary>
		/// <typeparam name="T">The enumeration type used to define image type.</typeparam>
		/// <param name="source">The image source.</param>
		/// <param name="type">The image type.</param>
		/// <returns>The new <see cref="ImageSource{TEnum}"/> instance or <see cref="Result{TValue}"/> with validation errors.</returns>
		public static Result<ImageSource<T>> Create<T>(string source, T type)
			where T : Enumeration<T>
			=> Result.Success()
				.Ensure(() => source != null, ImageSourceErrors.NullImageSource)
				.Ensure(() => type != null, ImageSourceErrors.NullImageType)
				.Map(() => new ImageSource<T>(new ImageSourceId(Guid.NewGuid()), source, type, false));
	}
}
