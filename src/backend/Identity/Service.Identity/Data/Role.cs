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

using Microsoft.AspNetCore.Identity;

namespace Service.Identity.Data
{
	public class Role : IdentityRole<Guid>
	{
		public const string Admin = "admin";
		public const string User = "user";

		public List<Permission> Permissions = [];

		public Role(Guid id, string roleName) : base(roleName)
		{
			Id = id;
			NormalizedName = roleName.ToUpper();
		}

		public Role() : base()
		{
		}
	}
}
