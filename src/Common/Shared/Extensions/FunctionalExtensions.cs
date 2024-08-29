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

namespace Shared.Extensions
{
	/// <summary>
	/// Contains extension methods for supporting some functional patterns.
	/// </summary>
	public static class FunctionalExtensions
	{
		/// <summary>
		/// Performs the specified action and returns the same instance.
		/// </summary>
		/// <typeparam name="T">The instance type.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="action">The action to perform.</param>
		/// <returns>The same instance.</returns>
		public static T Tap<T>(this T instance, Action action)
		{
			action();

			return instance;
		}

		/// <summary>
		/// Performs the specified action and returns the same instance.
		/// </summary>
		/// <typeparam name="T">The instance type.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="action">The action to perform.</param>
		/// <returns>The same instance.</returns>
		public static async Task<T> Tap<T>(this T instance, Func<Task> action)
		{
			await action();

			return instance;
		}

		/// <summary>
		/// Performs the specified action with the current instance and returns the same instance.
		/// </summary>
		/// <typeparam name="T">The instance type.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="action">The action to perform.</param>
		/// <returns>The same instance.</returns>
		public static T Tap<T>(this T instance, Action<T> action)
		{
			action(instance);

			return instance;
		}

		/// <summary>
		/// Performs the specified action with the current instance and returns the same instance.
		/// </summary>
		/// <typeparam name="T">The instance type.</typeparam>
		/// <param name="instance">The instance.</param>
		/// <param name="action">The action to perform.</param>
		/// <returns>The same instance.</returns>
		public static async Task<T> Tap<T>(this T instance, Func<T, Task> action)
		{
			await action(instance);

			return instance;
		}

		/// <summary>
		/// Invokes the specified action for each element in the collection.
		/// </summary>
		/// <typeparam name="T">The collection type.</typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="action">The action to invoke for each element.</param>
		public static void ForEachElement<T>(this IEnumerable<T> collection, Action<T> action)
		{
			foreach (T element in collection)
			{
				action(element);
			}
		}

		/// <summary>
		/// Invokes the specified action for each element in the collection.
		/// </summary>
		/// <typeparam name="T">The collection type.</typeparam>
		/// <param name="collection">The collection.</param>
		/// <param name="action">The action to invoke for each element.</param>
		/// <returns>The <see cref="Task"/>.</returns>
		public static async Task ForEachElement<T>(this IEnumerable<T> collection, Func<T, Task> action)
		{
			foreach (T element in collection)
			{
				await action(element);
			}
		}

		/// <summary>
		/// Executes a try-catch-finally block with the specified actions.
		/// </summary>
		/// <param name="action">The action in the try block.</param>
		/// <param name="catchAction">The action in the catch block.</param>
		/// <param name="finallyAction">The action in the finally block.</param>
		public static void TryCatchFinally(Action action, Action<Exception> catchAction, Action finallyAction)
		{
			try
			{
				action();
			}
			catch (Exception exception)
			{
				catchAction(exception);
			}
			finally
			{
				finallyAction();
			}
		}

		/// <summary>
		/// Executes a try-catch-finally block with the specified actions.
		/// </summary>
		/// <param name="action">The action in the try block.</param>
		/// <param name="catchAction">The action in the catch block.</param>
		public static void TryCatch(Action action, Action<Exception> catchAction)
		{
			try
			{
				action();
			}
			catch (Exception exception)
			{
				catchAction(exception);
			}
		}
	}
}
