﻿/*
 * Copyright 2016-2017 Mohawk College of Applied Arts and Technology
 *
 * Licensed under the Apache License, Version 2.0 (the "License"); you
 * may not use this file except in compliance with the License. You may
 * obtain a copy of the License at
 *
 * http://www.apache.org/licenses/LICENSE-2.0
 *
 * Unless required by applicable law or agreed to in writing, software
 * distributed under the License is distributed on an "AS IS" BASIS, WITHOUT
 * WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied. See the
 * License for the specific language governing permissions and limitations under
 * the License.
 *
 * User: khannan
 * Date: 2016-12-12
 */

using OpenIZAdmin.Localization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web.Mvc;

namespace OpenIZAdmin.Extensions
{
	/// <summary>
	/// Represents a collection of extension methods for the <see cref="List{T}" /> class.
	/// </summary>
	public static class ListExtensions
	{
		/// <summary>
		/// Converts a list of items to a select list.
		/// </summary>
		/// <typeparam name="T">The type of items in the list.</typeparam>
		/// <param name="source">The source list.</param>
		/// <param name="textPropertyName">The text property name.</param>
		/// <param name="valuePropertyName">The value property name.</param>
		/// <param name="selectedExpression">An expression which evaluates to set selected items.</param>
		/// <param name="skipDefaultEntry">if set to <c>true</c> [skip default entry].</param>
		/// <returns>Returns a select list.</returns>
		/// <exception cref="System.ArgumentNullException">If the source or textPropertyName or valuePropertyName is null.</exception>
		public static List<SelectListItem> ToSelectList<T>(this IEnumerable<T> source, string textPropertyName, string valuePropertyName, Expression<Func<T, bool>> selectedExpression = null, bool skipDefaultEntry = false)
		{
			if (source == null)
			{
				throw new ArgumentNullException(nameof(source), Locale.ValueCannotBeNull);
			}

			var clonedList = new List<T>(source);

			var selectList = new List<SelectListItem>();

			if (!skipDefaultEntry)
			{
				selectList.Add(new SelectListItem { Text = string.Empty, Value = string.Empty });
			}

			if (string.IsNullOrEmpty(textPropertyName) || string.IsNullOrWhiteSpace(textPropertyName))
			{
				throw new ArgumentNullException(nameof(textPropertyName), Locale.ValueCannotBeNull);
			}

			if (string.IsNullOrEmpty(valuePropertyName) || string.IsNullOrWhiteSpace(valuePropertyName))
			{
				throw new ArgumentNullException(nameof(valuePropertyName), Locale.ValueCannotBeNull);
			}

			selectList.AddRange(selectedExpression == null ?
				clonedList.Select(x => new SelectListItem
				{
					Text = x.GetType().GetProperty(textPropertyName).GetValue(x)?.ToString(),
					Value = x.GetType().GetProperty(valuePropertyName).GetValue(x).ToString()
				}) :
				clonedList.Select(x => new SelectListItem
				{
					Selected = Convert.ToBoolean(selectedExpression.Compile().DynamicInvoke(x)),
					Text = x.GetType().GetProperty(textPropertyName).GetValue(x)?.ToString(),
					Value = x.GetType().GetProperty(valuePropertyName).GetValue(x).ToString()
				}));

			return selectList.OrderBy(x => x.Text).ToList();
		}
	}
}