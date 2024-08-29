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

using Serilog;

namespace Service.Shipments.WebApi.Utility
{
	/// <summary>
	/// Contains utility methods for logging.
	/// </summary>
	internal static class LoggingUtility
	{
		/// <summary>
		/// Wraps the provided startup action with a try-catch-finally block and provides logging.
		/// </summary>
		/// <param name="startupAction">The startup action.</param>
		internal static void Run(Action startupAction)
		{
			Log.Logger = new LoggerConfiguration()
				.WriteTo.Console()
				.CreateBootstrapLogger();

			Log.Information("Starting up.");

			FunctionalExtensions.TryCatchFinally(
				startupAction,
				exception => Log.Fatal(exception, "Unhandled exception."),
				() =>
				{
					Log.Information("Shutting down.");
					Log.CloseAndFlush();
				});
		}
	}
}
