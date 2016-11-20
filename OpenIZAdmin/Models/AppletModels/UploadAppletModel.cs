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
 * Date: 2016-7-13
 */

using System.ComponentModel.DataAnnotations;
using System.Web;

namespace OpenIZAdmin.Models.AppletModels
{
	/// <summary>
	/// Represents a model to upload an applet.
	/// </summary>
	public class UploadAppletModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UploadAppletModel"/> class.
		/// </summary>
		public UploadAppletModel()
		{
		}

		/// <summary>
		/// Gets or sets the description of the applet.
		/// </summary>
		[Required]
		[StringLength(255)]
		public string Description { get; set; }

		/// <summary>
		/// Gets or sets the applet content.
		/// </summary>
		[Required]
		//[FileExtensions(Extensions = ".pak.gz, .pak, .gz", ErrorMessage = "Unsupported file format, the allow file types are .pak.gz, .gz, .pak")]
		public HttpPostedFileBase File { get; set; }

		/// <summary>
		/// Gets or sets the name of the applet.
		/// </summary>
		[Required]
		[StringLength(100)]
		public string Name { get; set; }

		[Display(Name = "Upload another file?")]
		public bool UploadAnotherFile { get; set; }
	}
}