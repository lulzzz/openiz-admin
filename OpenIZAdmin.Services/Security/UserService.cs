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
 * User: khannan
 * Date: 2017-7-10
 */

using OpenIZ.Core.Model.Constants;
using OpenIZ.Core.Model.Entities;
using OpenIZ.Messaging.IMSI.Client;
using OpenIZAdmin.Core;
using OpenIZAdmin.Core.Extensions;
using OpenIZAdmin.Localization;
using OpenIZAdmin.Services.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using OpenIZ.Core.Model;

namespace OpenIZAdmin.Services.Security
{
	/// <summary>
	/// Represents a user service.
	/// </summary>
	/// <seealso cref="OpenIZAdmin.Services.Core.ImsiServiceBase" />
	/// <seealso cref="OpenIZAdmin.Services.Security.IUserService" />
	public class UserService : ImsiServiceBase, IUserService
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="UserService"/> class.
		/// </summary>
		/// <param name="client">The client.</param>
		public UserService(ImsiServiceClient client) : base(client)
		{
		}

		/// <summary>
		/// Gets the user entity by security user key.
		/// </summary>
		/// <param name="securityUserId">The security user identifier.</param>
		/// <returns>Returns the user entity for the given security user key.</returns>
		public UserEntity GetUserEntityBySecurityUserKey(Guid securityUserId)
		{
			if (securityUserId == Guid.Parse(Constants.SystemUserId))
			{
				return new UserEntity
				{
					Names = new List<EntityName>
					{
						new EntityName(NameUseKeys.OfficialRecord, Locale.System)
					}
				};
			}

			var bundle = this.Client.Query<UserEntity>(u => u.SecurityUserKey == securityUserId && u.ObsoletionTime == null, 0, null, true);

			bundle.Reconstitute();

			return bundle.Item.OfType<UserEntity>().Where(u => u.SecurityUserKey == securityUserId).LatestVersionOnly().FirstOrDefault();
		}

		/// <summary>
		/// Updates the user entity.
		/// </summary>
		/// <param name="userEntity">The user entity.</param>
		/// <returns>Returns the updated user entity.</returns>
		/// <exception cref="System.NotImplementedException"></exception>
		public UserEntity UpdateUserEntity(UserEntity userEntity)
		{
			throw new NotImplementedException();
		}
	}
}