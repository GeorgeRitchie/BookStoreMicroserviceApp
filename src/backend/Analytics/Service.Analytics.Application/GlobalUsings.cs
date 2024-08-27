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

global using Application.Extensions;
global using Application.Mapper;
global using Application.Messaging;
global using Application.Models;
global using AutoMapper;
global using FluentValidation;
global using FluentValidation.Results;
global using Microsoft.EntityFrameworkCore;
global using Microsoft.Extensions.Logging;
global using Shared.Errors;
global using Shared.Extensions;
global using Shared.Repositories;
global using Shared.Results;
