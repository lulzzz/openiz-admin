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
 * Date: 2016-7-17
 */

using OpenIZ.Core.Model.AMI.Auth;
using OpenIZ.Core.Model.AMI.Security;
using OpenIZAdmin.Attributes;
using OpenIZAdmin.Models.UserModels;
using OpenIZAdmin.Models.UserModels.ViewModels;
using OpenIZAdmin.Services;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace OpenIZAdmin.Controllers
{
	/// <summary>
	/// Provides operations for administering users.
	/// </summary>
	[TokenAuthorize]
	public class UserController : Controller
	{
		/// <summary>
		/// The internal reference to the administrative interface endpoint.
		/// </summary>
		private static readonly Uri amiEndpoint = new Uri(RealmConfig.GetCurrentRealm().AmiEndpoint);

		/// <summary>
		/// The internal reference to the <see cref="OpenIZAdmin.Services.RestClient"/> instance.
		/// </summary>
		private RestClient client;

		/// <summary>
		/// Initializes a new instance of the <see cref="OpenIZAdmin.Controllers.UserController"/> class.
		/// </summary>
		public UserController()
		{
		}

		[HttpGet]
		public ActionResult CreateUser()
		{
			return View();
		}

		[HttpPost]
		[ActionName("CreateUser")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> CreateUserAsync(CreateUserModel model)
		{
			if (ModelState.IsValid)
			{
				SecurityUserInfo user = model.ToSecurityUserInfo();

				var result = await this.client.PostAsync("/user/", user);

				if (result.IsSuccessStatusCode)
				{
					TempData["success"] = "User created successfully";

					return RedirectToAction("Index");
				}
			}

			TempData["error"] = "Unable to create user";

			return View(model);
		}

		[HttpPost]
		[ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<ActionResult> DeleteUserAsync(Guid id)
		{
			if (id != Guid.Empty)
			{
				var result = await this.client.DeleteAsync(string.Format("/user/{0}", id));

				if (result.IsSuccessStatusCode)
				{
					TempData["success"] = "User deleted successfully";

					return RedirectToAction("Index");
				}
			}

			TempData["error"] = "Unable to delete user";

			return RedirectToAction("Index");
		}

		/// <summary>
		/// Dispose of any managed resources.
		/// </summary>
		/// <param name="disposing">Whether the current invocation is disposing.</param>
		protected override void Dispose(bool disposing)
		{
			Trace.TraceInformation("{0} disposing", nameof(UserController));

			this.client?.Dispose();

			base.Dispose(disposing);
		}

		[HttpGet]
		[ActionName("User")]
		public async Task<ActionResult> GetUserAsync(Guid id)
		{
			if (id != Guid.Empty)
			{
				var result = await this.client.GetAsync(string.Format("/user/{0}", id));

				if (result.IsSuccessStatusCode)
				{
					var content = await result.Content.ReadAsAsync<SecurityUserInfo>();

					return View(new UserViewModel(content));
				}
			}

			TempData["error"] = "User not found";

			return RedirectToAction("Index");
		}

		[HttpGet]
		[ActionName("Users")]
		public async Task<ActionResult> GetUsersAsync()
		{
			var result = await this.client.GetAsync(string.Format("/users/"));

			if (result.IsSuccessStatusCode)
			{
				var content = await result.Content.ReadAsAsync<AmiCollection<SecurityUserInfo>>();

				return View(content.CollectionItem.Select(u => new UserViewModel(u)));
			}

			TempData["error"] = "Unable to retrieve user list";

			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		[ActionName("Index")]
		public async Task<ActionResult> IndexAsync()
		{
			var result = await this.client.GetAsync(string.Format("/users/"));

			if (result.IsSuccessStatusCode)
			{
				var content = await result.Content.ReadAsAsync<AmiCollection<SecurityUserInfo>>();

				return View(content.CollectionItem.Select(u => new UserViewModel(u)));
			}

			TempData["error"] = "Unable to retrieve user list";

			return RedirectToAction("Index", "Home");
		}

		protected override void OnActionExecuting(ActionExecutingContext filterContext)
		{
			this.client = new RestClient(amiEndpoint, new Credentials(HttpContext.Request));

			base.OnActionExecuting(filterContext);
		}

		[HttpGet]
		public ActionResult UpdateUser()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult UpdateUser(object model)
		{
			if (ModelState.IsValid)
			{
			}

			TempData["error"] = "Unable to update user";

			return View(model);
		}
	}
}