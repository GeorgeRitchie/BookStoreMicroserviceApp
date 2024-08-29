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

namespace Service.Carts.WebApi.ServiceInstallers.Grpc
{
	/// <summary>
	/// Represents the GRPC options.
	/// </summary>
	internal sealed class GrpcOptions
	{
		/// <summary>
		/// Gets the GRPC url.
		/// </summary>
		public string Url { get; init; }

		/// <summary>
		/// Gets the retry count.
		/// </summary>
		public int RetryCount { get; init; }
	}
}
