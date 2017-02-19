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
 * User: Nityan
 * Date: 2016-7-23
 */

using OpenIZ.Core.Model.Entities;
using System;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OpenIZAdmin.Models.MaterialModels.ViewModels
{
	/// <summary>
	/// Represents a material search result view model.
	/// </summary>
	public class MaterialSearchResultViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="MaterialSearchResultViewModel"/> class.
		/// </summary>
		public MaterialSearchResultViewModel()
		{
		}

		public MaterialSearchResultViewModel(Material material)
		{
			this.CreationTime = material.CreationTime.DateTime;
			this.Id = material.Key.Value;
			this.Name = string.Join(", ", material.Names.SelectMany(m => m.Component).Select(c => c.Value));
		}

		/// <summary>
		/// Gets or sets the creation time of the material.
		/// </summary>
		[Display(Name = "CreationTime", ResourceType = typeof(Localization.Locale))]
		public DateTime CreationTime { get; set; }

		/// <summary>
		/// Gets or sets th key of the material.
		/// </summary>
		public Guid Id { get; set; }

		/// <summary>
		/// Gets or sets the name of the material.
		/// </summary>
		[Display(Name = "Name", ResourceType = typeof(Localization.Locale))]
		public string Name { get; set; }
	}
}