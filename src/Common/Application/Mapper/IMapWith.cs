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

using AutoMapper;

namespace Application.Mapper
{
	/// <summary>
	/// A marker interface for <see cref="AssemblyMappingProfile"/> to find and register mapper classes from assembly.
	/// </summary>
	/// <typeparam name="T">The type to or/and from mapping applied.</typeparam>
	public interface IMapWith<T>
	{
		/// <summary>
		/// Method that applies mapping logic.
		/// </summary>
		/// <param name="profile">The mapping profile.</param>
		void Mapping(Profile profile) => profile.CreateMap(typeof(T), GetType());
	}
}
