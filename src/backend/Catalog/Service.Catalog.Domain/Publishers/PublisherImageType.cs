﻿/* 
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

using Newtonsoft.Json;

namespace Service.Catalog.Domain.Publishers
{
	/// <summary>
	/// Represents the Publisher image type enumeration.
	/// </summary>
	public sealed class PublisherImageType : Enumeration<PublisherImageType>
	{
		public static readonly PublisherImageType Icon = new("PublisherIcon", 0);
		public static readonly PublisherImageType Photo = new("PublisherPhoto", 1);
		public static readonly PublisherImageType Other = new("PublisherOther", 2);

		/// <summary>
		/// Initializes a new instance of the <see cref="PublisherImageType"/> class.
		/// </summary>
		/// <inheritdoc/>
		[JsonConstructor]
		private PublisherImageType(string name, int value) : base(name, value)
		{
		}
	}
}