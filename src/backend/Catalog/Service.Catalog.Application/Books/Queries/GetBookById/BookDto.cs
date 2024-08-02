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

using Service.Catalog.Application.Common.Models;
using Service.Catalog.Domain.Authors;
using Service.Catalog.Domain.Books;
using Service.Catalog.Domain.BookSources;
using Service.Catalog.Domain.Categories;
using Service.Catalog.Domain.Publishers;

namespace Service.Catalog.Application.Books.Queries.GetBookById
{
	/// <summary>
	/// Represents the dto for <see cref="Book"/> entity.
	/// </summary>
	public sealed class BookDto : IMapWith<Book>
	{
		/// <summary>
		/// Book identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Book title.
		/// </summary>
		public string Title { get; set; }

		/// <summary>
		/// Book description.
		/// </summary>
		public string Description { get; set; }

		/// <summary>
		/// Book ISBN if available.
		/// </summary>
		public string? ISBN { get; set; }

		/// <summary>
		/// Book language in ISO 639-1 format (e. g. ru, en, fr).
		/// </summary>
		public string Language { get; set; }

		/// <summary>
		/// Book age rating.
		/// </summary>
		public uint AgeRating { get; set; }

		/// <summary>
		/// Book published date if available.
		/// </summary>
		public DateOnly? PublishedDate { get; set; }

		/// <summary>
		/// Book publisher information if available.
		/// </summary>
		public PublisherDto? Publisher { get; set; }

		/// <summary>
		/// Book images.
		/// </summary>
		public List<ImageSourceDto> Images { get; set; }

		/// <summary>
		/// Book authors information.
		/// </summary>
		public List<AuthorDto> Authors { get; set; }

		/// <summary>
		/// Book categories.
		/// </summary>
		public List<CategoryDto> Categories { get; set; }

		/// <summary>
		/// Book source information.
		/// </summary>
		public List<BookSourceDto> Sources { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<Book, BookDto>()
				.ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id.Value));
	}

	/// <summary>
	/// Represents the dto for <see cref="BookSource"/> entity.
	/// </summary>
	public class BookSourceDto : IMapWith<BookSource>
	{
		/// <summary>
		/// Book source identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Book source format (e. g. paper, pdf, txt).
		/// </summary>
		public string Format { get; set; }

		/// <summary>
		/// Books quantity in storage. (Used only for paper format book sources).
		/// </summary>
		public uint? StockQuantity { get; set; }

		/// <summary>
		/// Book price.
		/// </summary>
		public decimal Price { get; set; }

		/// <summary>
		/// Book's preview source url of if available.
		/// </summary>
		public string? PreviewUrl { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<BookSource, BookSourceDto>()
				.ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id.Value))
				.ForMember(dto => dto.Format, opt => opt.MapFrom(c => c.Format.Name));
	}

	/// <summary>
	/// Represents the dto for <see cref="Category"/> entity.
	/// </summary>
	public class CategoryDto : IMapWith<Category>
	{
		/// <summary>
		/// Category identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Category title.
		/// </summary>
		public string Title { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<Category, CategoryDto>()
				.ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id.Value));
	}

	/// <summary>
	/// Represents the dto for <see cref="Author"/> entity.
	/// </summary>
	public sealed class AuthorDto : IMapWith<Author>
	{
		/// <summary>
		/// Author identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Author first name.
		/// </summary>
		public string FirstName { get; set; }

		/// <summary>
		/// Author last name.
		/// </summary>
		public string LastName { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<Author, AuthorDto>()
				.ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id.Value));
	}

	/// <summary>
	/// Represents the dto for <see cref="Publisher"/> entity.
	/// </summary>
	public sealed class PublisherDto : IMapWith<Publisher>
	{
		/// <summary>
		/// Publisher identifier.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Publisher name.
		/// </summary>
		public string Name { get; set; }

		public void Mapping(Profile profile)
			=> profile.CreateMap<Publisher, PublisherDto>()
				.ForMember(dto => dto.Id, opt => opt.MapFrom(c => c.Id.Value));
	}
}
