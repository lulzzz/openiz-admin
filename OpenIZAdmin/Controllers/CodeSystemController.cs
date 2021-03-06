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
 * Date: 2017-4-17
 */


using OpenIZ.Core.Model.AMI.Security;
using OpenIZ.Core.Model.DataTypes;
using OpenIZAdmin.Attributes;
using OpenIZAdmin.Localization;
using OpenIZAdmin.Models.CodeSystemModels;
using OpenIZAdmin.Models.ConceptModels;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace OpenIZAdmin.Controllers
{
	/// <summary>
	/// Provides operations for managing code systems.
	/// </summary>
	[TokenAuthorize(Constants.AdministerConceptDictionary)]
	public class CodeSystemController : MetadataController
	{
		/// <summary>
		/// Displays the create view.
		/// </summary>
		/// <returns>Returns the create view.</returns>
		[HttpGet]
		public ActionResult Create()
		{
			return View();
		}

		/// <summary>
		/// Creates a code system.
		/// </summary>
		/// <param name="model">The model containing the information to create a code system.</param>
		/// <returns>Returns the created concept.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(CreateCodeSystemModel model)
		{
			try
			{
                var exists = this.AmiClient.GetCodeSystems(c => c.Oid == model.Oid);
                if (exists != null && exists.CollectionItem.Any()) ModelState.AddModelError("Oid", Locale.OidMustBeUnique);

                var duplicate = this.AmiClient.GetCodeSystems(c => c.Url == model.Url);
                if (duplicate != null && duplicate.CollectionItem.Any()) ModelState.AddModelError("Url", Locale.UrlMustBeUnique);

                var duplicateDomainName = this.AmiClient.GetCodeSystems(c => c.Authority == model.Domain);
                if (duplicateDomainName != null && duplicateDomainName.CollectionItem.Any()) ModelState.AddModelError("Domain", Locale.DomainNameMustBeUnique);


                if (ModelState.IsValid)
				{
					var codeSystem = this.AmiClient.CreateCodeSystem(model.ToCodeSystem());

					TempData["success"] = Locale.CodeSystemCreatedSuccessfully;

					return RedirectToAction("ViewCodeSystem", new { id = codeSystem.Key });
				}
			}
			catch (Exception e)
			{
				
				Trace.TraceError($"Unable to create code system: {e}");
			}

			TempData["error"] = Locale.UnableToCreateCodeSystem;

			return View(model);
		}

		/// <summary>
		/// Edits the specified concept.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <returns>ActionResult.</returns>
		[HttpGet]
		public ActionResult Edit(Guid id)
		{
			try
			{
				var codeSystem = this.AmiClient.GetCodeSystem(id.ToString());

				if (codeSystem == null)
				{
					TempData["error"] = Locale.CodeSystemNotFound;

					return RedirectToAction("Index");
				}

				var model = new EditCodeSystemModel(codeSystem);

				return View(model);
			}
			catch (Exception e)
			{
				
				Trace.TraceError($"Unable to update code system: {e}");
				this.TempData["error"] = Locale.UnexpectedErrorMessage;
			}

			return RedirectToAction("Index");
		}

		/// <summary>
		/// Updates a Concept from the <see cref="EditConceptModel"/> instance.
		/// </summary>
		/// <param name="model">The EditCodeSystemViewModel instance</param>
		/// <returns></returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(EditCodeSystemModel model)
		{
			try
			{
                var codeSystem = this.AmiClient.GetCodeSystem(model.Id.ToString());

                if (codeSystem == null)
                {
                    TempData["error"] = Locale.CodeSystemNotFound;

                    return RedirectToAction("Index");
                }                                
                
                if (!codeSystem.Oid.Equals(model.Oid))
                {
                    var exists = this.AmiClient.GetCodeSystems(c => c.Oid == model.Oid);
                    if (exists != null && exists.CollectionItem.Any()) ModelState.AddModelError("Oid", Locale.OidMustBeUnique);
                }

			    if (!codeSystem.Url.Equals(model.Url))
			    {
                    var duplicate = this.AmiClient.GetCodeSystems(c => c.Url == model.Url);
                    if (duplicate != null && duplicate.CollectionItem.Any()) ModelState.AddModelError("Url", Locale.UrlMustBeUnique);                    
			    }

			    if (!codeSystem.Authority.Equals(model.Domain))
			    {

			        var duplicateDomainName = this.AmiClient.GetCodeSystems(c => c.Authority == model.Domain);
			        if (duplicateDomainName != null && duplicateDomainName.CollectionItem.Any()) ModelState.AddModelError("Domain", Locale.DomainNameMustBeUnique);
			    }

			    if (ModelState.IsValid)
				{					
					codeSystem = this.AmiClient.UpdateCodeSystem(model.Id.ToString(), model.ToCodeSystem(codeSystem));

					TempData["success"] = Locale.CodeSystemUpdatedSuccessfully;

					return RedirectToAction("ViewCodeSystem", new { id = codeSystem.Key });
				}
			}
			catch (Exception e)
			{
				
				Trace.TraceError($"Unable to update code system: {e}");
			}

			TempData["error"] = Locale.UnableToUpdateCodeSystem;

			return View(model);
		}

		/// <summary>
		/// Displays the index view.
		/// </summary>
		/// <returns>Returns the index view.</returns>
		public ActionResult Index()
		{
			TempData["searchType"] = "CodeSystem";
            TempData["searchTerm"] = "*";
            return View();
		}

		/// <summary>
		/// Displays the search view.
		/// </summary>
		/// <returns>Returns the search view.</returns>
		[HttpGet]
		[ValidateInput(false)]
		public ActionResult Search(string searchTerm)
		{
			var results = new List<CodeSystemViewModel>();

			if (this.IsValidId(searchTerm))
			{
				AmiCollection<CodeSystem> collection;

				if (searchTerm == "*")
				{
					collection = this.AmiClient.GetCodeSystems(c => c.ObsoletionTime == null);
					results = collection.CollectionItem.Select(p => new CodeSystemViewModel(p)).ToList();
				}
				else
				{
					collection = this.AmiClient.GetCodeSystems(c => c.Name.Contains(searchTerm) && c.ObsoletionTime == null);
					results = collection.CollectionItem.Select(p => new CodeSystemViewModel(p)).ToList();
				}

				TempData["searchTerm"] = searchTerm;

				return PartialView("_CodeSystemsPartial", results.OrderBy(c => c.Name));
			}

			TempData["error"] = Locale.InvalidSearch;
			TempData["searchTerm"] = searchTerm;

			return PartialView("_CodeSystemsPartial", results);
		}

		/// <summary>
		/// Retrieves the Concept by identifier
		/// </summary>
		/// <param name="id">The identifier of the Concept</param>
		/// <returns>Returns the concept view.</returns>
		[HttpGet]
		public ActionResult ViewCodeSystem(Guid id)
		{
			try
			{
				var codeSystem = this.AmiClient.GetCodeSystem(id.ToString());

				if (codeSystem == null)
				{
					TempData["error"] = Locale.CodeSystemNotFound;

					return RedirectToAction("Index");
				}

				return View(new CodeSystemViewModel(codeSystem));
			}
			catch (Exception e)
			{
				Trace.TraceError($"Unable to retrieve code system: {e}");
				this.TempData["error"] = Locale.UnexpectedErrorMessage;
			}

			return RedirectToAction("Index");
		}
	}
}