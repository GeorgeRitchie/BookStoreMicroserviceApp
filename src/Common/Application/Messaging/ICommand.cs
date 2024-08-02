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

using MediatR;
using Shared.Results;

namespace Application.Messaging
{
	/// <summary>
	/// Represents the command interface.
	/// </summary>
	public interface ICommand : IRequest<Result>
	{
	}

	/// <summary>
	/// Represents the command interface.
	/// </summary>
	/// <typeparam name="TResponse">The command response type.</typeparam>
	public interface ICommand<TResponse> : IRequest<Result<TResponse>>
	{
	}
}
