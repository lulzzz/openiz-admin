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
 * Date: 2016-7-9
 */
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace OpenIZAdmin.Models.CertificateModels
{
	public class SubmitCertificateSigningRequestModel
	{
		public SubmitCertificateSigningRequestModel()
		{

		}

		[Required]
		[Display(Name = "CMC Request")]
		public string CmcRequest { get; set; }

		[Required]
		[StringLength(255)]
		[Display(Name = "Administrative Contact Name")]
		public string AdministrativeContactName { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Administrative Contact Email")]
		public string AdministrativeContactEmail { get; set; }
	}
}