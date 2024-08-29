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

using System.Globalization;

namespace Shared.Extensions
{
	/// <summary>
	/// Provides extension methods for <see langword="string"/> type values.
	/// </summary>
	public static class StringExtensions
	{
		/// <summary>
		/// Converts a <see langword="string"/> to a <see langword="float"/>. Throws <see cref="FormatException"/> if the <see langword="string"/> cannot be converted to a <see langword="float"/>.
		/// </summary>
		/// <param name="value">The <see langword="string"/> value to be converted.</param>
		/// <returns>The parsed <see langword="float"/> value.</returns>
		/// <exception cref="FormatException">Thrown if the <see langword="string"/> cannot be converted to a <see langword="float"/>.</exception>
		public static float ToFloat(this string value)
		{
			value = value.Replace(',', '.');
			return float.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Tries to convert a <see langword="string"/> to a <see langword="float"/>. If the <see langword="string"/> cannot be converted to a <see langword="float"/>, returns <see langword="null"/>.
		/// </summary>
		/// <param name="value">The <see langword="string"/> value to be converted.</param>
		/// <returns>The parsed <see langword="float"/> value, or <see langword="null"/> if parsing fails.</returns>
		public static float? TryToFloat(this string value)
		{
			value = value.Replace(',', '.');
			if (float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out float result))
				return result;
			else
				return null;
		}

		/// <summary>
		/// Tries to convert a <see langword="string"/> to a <see langword="float"/>. Returns <see langword="true"/> if parsing succeeds, otherwise <see langword="false"/>.
		/// </summary>
		/// <param name="value">The <see langword="string"/> value to be converted.</param>
		/// <param name="result">The parsed <see langword="float"/> value if parsing succeeds, otherwise <see langword="default"/>.</param>
		/// <returns><see langword="true"/> if parsing succeeds, otherwise <see langword="false"/>.</returns>
		public static bool TryToFloat(this string value, out float result)
		{
			value = value.Replace(',', '.');
			return float.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
		}

		/// <summary>
		/// Converts a <see langword="string"/> to a <see langword="double"/>. Throws <see cref="FormatException"/> if the <see langword="string"/> cannot be converted to a <see langword="double"/>.
		/// </summary>
		/// <param name="value">The <see langword="string"/> value to be converted.</param>
		/// <returns>The parsed <see langword="double"/> value.</returns>
		/// <exception cref="FormatException">Thrown if the <see langword="string"/> cannot be converted to a <see langword="double"/>.</exception>
		public static double ToDouble(this string value)
		{
			value = value.Replace(',', '.');
			return double.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Tries to convert a <see langword="string"/> to a <see langword="double"/>. If the <see langword="string"/> cannot be converted to a <see langword="double"/>, returns <see langword="null"/>.
		/// </summary>
		/// <param name="value">The <see langword="string"/> value to be converted.</param>
		/// <returns>The parsed <see langword="double"/> value, or <see langword="null"/> if parsing fails.</returns>
		public static double? TryToDouble(this string value)
		{
			value = value.Replace(',', '.');
			if (double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out double result))
				return result;
			else
				return null;
		}

		/// <summary>
		/// Tries to convert a <see langword="string"/> to a <see langword="double"/>. Returns <see langword="true"/> if parsing succeeds, otherwise <see langword="false"/>.
		/// </summary>
		/// <param name="value">The <see langword="string"/> value to be converted.</param>
		/// <param name="result">The parsed <see langword="double"/> value if parsing succeeds, otherwise <see langword="default"/>.</param>
		/// <returns><see langword="true"/> if parsing succeeds, otherwise <see langword="false"/>.</returns>
		public static bool TryToDouble(this string value, out double result)
		{
			value = value.Replace(',', '.');
			return double.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
		}

		/// <summary>
		/// Converts a <see langword="string"/> to an <see langword="int"/>. Throws <see cref="FormatException"/> if the <see langword="string"/> cannot be converted to an <see langword="int"/>.
		/// </summary>
		/// <param name="value">The <see langword="string"/> value to be converted.</param>
		/// <returns>The parsed <see langword="int"/> value.</returns>
		/// <exception cref="FormatException">Thrown if the <see langword="string"/> cannot be converted to an <see langword="int"/>.</exception>
		public static int ToInt(this string value)
		{
			return int.Parse(value, NumberStyles.Any, CultureInfo.InvariantCulture);
		}

		/// <summary>
		/// Tries to convert a <see langword="string"/> to an <see langword="int"/>. If the <see langword="string"/> cannot be converted to an <see langword="int"/>, returns <see langword="null"/>.
		/// </summary>
		/// <param name="value">The <see langword="string"/> value to be converted.</param>
		/// <returns>The parsed <see langword="int"/> value, or <see langword="null"/> if parsing fails.</returns>
		public static int? TryToInt(this string value)
		{
			if (int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out int result))
				return result;
			else
				return null;
		}

		/// <summary>
		/// Tries to convert a <see langword="string"/> to an <see langword="int"/>. Returns <see langword="true"/> if parsing succeeds, otherwise <see langword="false"/>.
		/// </summary>
		/// <param name="value">The <see langword="string"/> value to be converted.</param>
		/// <param name="result">The parsed <see langword="int"/> value if parsing succeeds, otherwise <see langword="default"/>.</param>
		/// <returns><see langword="true"/> if parsing succeeds, otherwise <see langword="false"/>.</returns>
		public static bool TryToInt(this string value, out int result)
		{
			return int.TryParse(value, NumberStyles.Any, CultureInfo.InvariantCulture, out result);
		}
	}
}
