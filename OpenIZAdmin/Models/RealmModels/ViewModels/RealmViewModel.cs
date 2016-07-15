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
 * Date: 2016-7-15
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenIZAdmin.Models.RealmModels.ViewModels
{
	public class RealmViewModel
	{
		public RealmViewModel()
		{

		}

		public string Address { get; set; }

		[Display(Name = "Authorization Endpoint")]
		public string AmiAuthEndpoint { get; set; }

		[Display(Name = "Administrative Interface Endpoint")]
		public string AmiEndpoint { get; set; }

		[Display(Name = "Application Id")]
		public string ApplicationId { get; set; }

		[Display(Name = "Application Secret")]
		public string ApplicationSecret { get; set; }

		[Display(Name = "Creation Time")]
		public DateTime CreationTime { get; set; }

		public string Description { get; set; }

		public Guid Id { get; set; }

		public string Name { get; set; }
	}
}