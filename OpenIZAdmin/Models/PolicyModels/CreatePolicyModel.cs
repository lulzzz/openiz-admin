﻿/*
 * Copyright 2016-2016 Mohawk College of Applied Arts and Technology
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
 * Date: 2016-7-30
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OpenIZAdmin.Models.PolicyModels
{
	public class CreatePolicyModel
	{
		public CreatePolicyModel()
		{
			this.GrantsList = new List<SelectListItem>();
		}

		[Display(Name = "CanOverride", ResourceType = typeof(Localization.Resources))]
		public bool CanOverride { get; set; }

		[Display(Name = "Grants", ResourceType = typeof(Localization.Resources))]
		[Required(ErrorMessageResourceName = "GrantsRequired", ErrorMessageResourceType = typeof(Localization.Resources))]
		public int Grant { get; set; }

		public List<SelectListItem> GrantsList { get; set; }

		[Display(Name = "Name", ResourceType = typeof(Localization.Resources))]
		[Required(ErrorMessageResourceName = "NameRequired", ErrorMessageResourceType = typeof(Localization.Resources))]
		[StringLength(255, ErrorMessageResourceName = "NameTooLong", ErrorMessageResourceType = typeof(Localization.Resources))]
		public string Name { get; set; }

		[Display(Name = "Oid", ResourceType = typeof(Localization.Resources))]
		[Required(ErrorMessageResourceName = "OidRequired", ErrorMessageResourceType = typeof(Localization.Resources))]
		public string Oid { get; set; }
	}
}