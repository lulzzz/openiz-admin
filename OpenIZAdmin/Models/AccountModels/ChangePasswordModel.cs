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
 * Date: 2016-8-27
 */

using OpenIZAdmin.Localization;
using System.ComponentModel.DataAnnotations;

namespace OpenIZAdmin.Models.AccountModels
{
	/// <summary>
	/// Represents a model to change a password.
	/// </summary>
	public class ChangePasswordModel
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="ChangePasswordModel"/> class.
		/// </summary>
		public ChangePasswordModel()
		{
		}

		/// <summary>
		/// Gets or sets the password confirmation of the model.
		/// </summary>
		[DataType(DataType.Password)]
		[Display(Name = "ConfirmPassword", ResourceType = typeof(Locale))]
		[StringLength(50, ErrorMessageResourceName = "PasswordLength50", ErrorMessageResourceType = typeof(Locale))]
		[Required(ErrorMessageResourceName = "ConfirmPasswordRequired", ErrorMessageResourceType = typeof(Locale))]
		[RegularExpression(Constants.RegExPassword, ErrorMessageResourceName = "PasswordValidationErrorMessage", ErrorMessageResourceType = typeof(Locale))]
		[Compare("Password", ErrorMessageResourceName = "ConfirmPasswordMatch", ErrorMessageResourceType = typeof(Locale))]
		public string ConfirmPassword { get; set; }

		/// <summary>
		/// Gets or sets the current user password of the model.
		/// </summary>
		[DataType(DataType.Password)]
		[Display(Name = "CurrentPassword", ResourceType = typeof(Locale))]
		[StringLength(50, ErrorMessageResourceName = "PasswordLength50", ErrorMessageResourceType = typeof(Locale))]
		[Required(ErrorMessageResourceName = "CurrentPasswordRequired", ErrorMessageResourceType = typeof(Locale))]
		[RegularExpression(Constants.RegExPassword, ErrorMessageResourceName = "PasswordValidationErrorMessage", ErrorMessageResourceType = typeof(Locale))]
		public string CurrentPassword { get; set; }

		/// <summary>
		/// Gets or sets the password of the model.
		/// </summary>
		[DataType(DataType.Password)]
		[Display(Name = "Password", ResourceType = typeof(Locale))]
		[StringLength(50, ErrorMessageResourceName = "PasswordLength50", ErrorMessageResourceType = typeof(Locale))]
		[Required(ErrorMessageResourceName = "PasswordRequired", ErrorMessageResourceType = typeof(Locale))]
		[RegularExpression(Constants.RegExPassword, ErrorMessageResourceName = "PasswordValidationErrorMessage", ErrorMessageResourceType = typeof(Locale))]
		public string Password { get; set; }

		/// <summary>
		/// Gets or sets the username.
		/// </summary>
		/// <value>The username.</value>
		public string Username { get; set; }
	}
}