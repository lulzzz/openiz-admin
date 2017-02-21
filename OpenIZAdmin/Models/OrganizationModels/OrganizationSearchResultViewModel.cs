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
 * Date: 2017-2-19
 */

using System;
using System.ComponentModel.DataAnnotations;
using OpenIZ.Core.Model.Entities;
using OpenIZAdmin.Models.Core;

namespace OpenIZAdmin.Models.OrganizationModels
{
	/// <summary>
	/// Represents an organization search result view model.
	/// </summary>
	public class OrganizationSearchResultViewModel : EntityViewModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="OrganizationSearchResultViewModel"/> class.
		/// </summary>
		public OrganizationSearchResultViewModel()
		{
			
		}

		/// <summary>
		/// Initializes a new instance of the <see cref="OrganizationSearchResultViewModel"/> class
		/// with a specific <see cref="Organization"/> instance.
		/// </summary>
		/// <param name="organization">The <see cref="Organization"/> instance.</param>
		public OrganizationSearchResultViewModel(Organization organization) : base(organization)
		{
		}
	}
}