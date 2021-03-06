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
 * Date: 2017-7-27
 */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenIZ.Core.Model.DataTypes;

namespace OpenIZAdmin.Services.Entities.Places
{
	/// <summary>
	/// Represents a place concept service.
	/// </summary>
	public interface IPlaceConceptService
	{
		/// <summary>
		/// Gets the place type concepts.
		/// </summary>
		/// <param name="conceptSetMnemonicFilters">The concept set mnemonic filters.</param>
		/// <returns>Returns a list of concepts.</returns>
		IEnumerable<Concept> GetPlaceTypeConcepts(params string[] conceptSetMnemonicFilters);
	}
}
