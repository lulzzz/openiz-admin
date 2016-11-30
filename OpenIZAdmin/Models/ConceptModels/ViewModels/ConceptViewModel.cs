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
 * Date: 2016-7-23
 */

using OpenIZ.Core.Model.DataTypes;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace OpenIZAdmin.Models.ConceptModels.ViewModels
{
	public class ConceptViewModel
	{
		public ConceptViewModel()
		{
			this.ReferenceTerms = new List<ReferenceTermModel>();
		}

		[Display(Name = "Created By")]
		public string CreatedBy { get; set; }

		[Display(Name = "Creation Time")]
		public DateTime CreationTime { get; set; }

		[Display(Name = "Concept Details")]
		public List<DetailedConceptViewModel> Details { get; set; }

		[Display(Name = "Is Read Only?")]
		public bool IsReadOnly { get; set; }

		public Guid Key { get; set; }

		public List<string> Languages { get; set; }

		public string Mnemonic { get; set; }

        public string Language { get; set; }

		public List<ReferenceTermModel> ReferenceTerms { get; set; }

		public string Class { get; set; }
		public List<string> Name { get; set; }
	}
}