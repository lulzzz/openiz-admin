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
 * User: Andrew
 * Date: 2017-5-10
 */

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using OpenIZAdmin.Localization;

namespace OpenIZAdmin.Models.Core
{
    /// <summary>
    /// Represents a model of a user.
    /// </summary>
    public abstract class UserModel : UserModelBase
    {       
        /// <summary>
        /// Gets or sets the list of roles of the user.
        /// </summary>
        [Display(Name = "Roles", ResourceType = typeof(Localization.Locale))]
        [Required(ErrorMessageResourceName = "RolesRequired", ErrorMessageResourceType = typeof(Locale))]
        public List<string> Roles { get; set; }

        /// <summary>
        /// Gets or sets the list of available roles
        /// </summary>
        public List<SelectListItem> RolesList { get; set; }      

        /// <summary>
        /// Checks if any of the Role(s) assigned are an empty selection
        /// </summary>
        /// <returns>Returns true if an empty string is contained in the List</returns>
        public void CheckForEmptyRoleAssigned()
        {
            if (Roles != null && Roles.Any())
            {
                Roles.RemoveAll(string.IsNullOrWhiteSpace);
            }            
        }
                             
    }
}