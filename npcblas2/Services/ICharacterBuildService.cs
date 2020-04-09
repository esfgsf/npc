using System;
using System.Collections.Generic;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using npcblas2.Models;

namespace npcblas2.Services
{
    /// <summary>
    /// Describes how to handle character builds.
    /// Gotcha : This service should never return entities, because then we can end up with a dependency
    /// on whether the entity is currently tracked or not.  Rather, it should return objects with valid
    /// IDs so that we can do lookups again as required.
    /// </summary>
    public interface ICharacterBuildService
    {
        /// <summary>
        /// Adds a new character.
        /// </summary>
        Task<CharacterBuildModel> AddAsync(ClaimsPrincipal user, NewCharacterModel model);

        /// <summary>
        /// Continues the build of the given character by choosing the given option.
        /// </summary>
        Task<CharacterBuildModel> BuildAsync(ClaimsPrincipal user, CharacterBuildModel model, string choice);

        /// <summary>
        /// Gets all builds for the given user.
        /// The user name fields will be empty.
        /// </summary>
        Task<List<CharacterBuildSummary>> GetAllAsync(ClaimsPrincipal user);

        /// <summary>
        /// Gets the public builds.
        /// </summary>
        Task<List<CharacterBuildSummary>> GetPublicAsync();

        /// <summary>
        /// Gets a character build.
        /// </summary>
        Task<CharacterBuildModel> GetAsync(ClaimsPrincipal user, string id);

        /// <summary>
        /// Gets the number of characters a user has.
        /// </summary>
        Task<int> GetCountAsync(ClaimsPrincipal user);

        /// <summary>
        /// Gets the maximum number of characters you're allowed.
        /// </summary>
        int GetMaximumCount();

        /// <summary>
        /// Removes the given character build.
        /// </summary>
        Task<bool> RemoveAsync(ClaimsPrincipal user, Guid id);

        /// <summary>
        /// Sets the public flag.
        /// </summary>
        Task<bool> SetPublicAsync(ClaimsPrincipal user, Guid id, bool isPublic);

        /// <summary>
        /// Exports the user's characters (or all characters for an admin) as JSON to the given stream.
        /// </summary>
        Task ExportJsonAsync(ClaimsPrincipal user, Stream stream);

        /// <summary>
        /// Imports characters from the given stream as JSON.
        /// </summary>
        /// <returns>An object describing what was imported or null if the import failed.</returns>
        Task<ImportResult> ImportJsonAsync(ClaimsPrincipal user, Stream stream);
    }
}