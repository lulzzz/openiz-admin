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
 * Date: 2017-3-27
 */


using OpenIZAdmin.Localization;
using System;
using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;
using OpenIZ.Core.Model.Constants;
using OpenIZ.Core.Model.Entities;
using OpenIZAdmin.Extensions;
using OpenIZAdmin.Models.EntityRelationshipModels;
using OpenIZAdmin.Util;

namespace OpenIZAdmin.Controllers
{
	/// <summary>
	/// Provides operations for managing entity relationships.
	/// </summary>
	/// <seealso cref="OpenIZAdmin.Controllers.BaseController" />
	public class EntityRelationshipController : EntityBaseController
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="EntityRelationshipController"/> class.
		/// </summary>
		public EntityRelationshipController()
		{
		}

		/// <summary>
		/// Deletes the specified identifier.
		/// </summary>
		/// <param name="id">The identifier.</param>
		/// <param name="sourceId">The source identifier.</param>
		/// <param name="type">The type.</param>
		/// <returns>ActionResult.</returns>
		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Delete(Guid id, Guid sourceId, string type)
		{
			try
			{
				var entityRelationship = this.ImsiClient.Get<EntityRelationship>(id, null) as EntityRelationship;

				if (entityRelationship == null)
				{
					this.TempData["error"] = Locale.RelationshipNotFound;
					return RedirectToAction("Edit", type, new { id = sourceId });
				}

				this.ImsiClient.Obsolete(entityRelationship);

				this.TempData["success"] = Locale.RelationshipDeletedSuccessfully;

				return RedirectToAction("Edit", type, new { id = sourceId });
			}
			catch (Exception e)
			{
				
				Trace.TraceError($"Unable to delete entity relationship: { e }");
			}

			this.TempData["error"] = Locale.UnableToDeleteRelationship;

			return RedirectToAction("Edit", type, new { id = sourceId });
		}

        /// <summary>
        /// Edits the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <param name="sourceId">The source identifier.</param>	
        /// <param name="type">The type.</param>	
        /// <returns>ActionResult.</returns>
        [HttpGet]
	    public ActionResult Edit(Guid id, Guid sourceId, string type)
	    {	      
	        Guid? versionKey = null;

	        try
	        {
                var modelType = this.GetModelType(type);
                var entity = this.GetEntity(sourceId, modelType);
                versionKey = entity.VersionKey;

                var model = new EntityRelationshipModel(Guid.NewGuid(), id)
                {
                    //ExistingRelationships = place.Relationships.Select(r => new EntityRelationshipViewModel(r)).ToList()
                };

                model.RelationshipTypes.AddRange(this.GetConceptSet(ConceptSetKeys.EntityRelationshipType).Concepts.ToSelectList(this.HttpContext.GetCurrentLanguage(), c => c.Key == EntityRelationshipTypeKeys.OwnedEntity).ToList());

                //entity.Relationships.RemoveAll(r => r.Key == id);

                //var updatedEntity = this.UpdateEntity(entity, modelType);

                //this.TempData["success"] = Locale.Relationship + " " + Locale.Deleted + " " + Locale.Successfully;

                //return RedirectToAction("Edit", type, new { id = updatedEntity.Key.Value, versionId = updatedEntity.VersionKey.Value });
                return View(new EntityRelationshipModel(id, sourceId, (Guid)versionKey));
	        }
	        catch (Exception e)
	        {
	            Trace.TraceError($"Unable to delete entity relationship: {e}");
	        }

	        this.TempData["error"] = Locale.UnableToEditRelationship;

	        return RedirectToAction("Edit" + type, type, new {id = sourceId, versionId = versionKey});
	     
	    }
	}
}