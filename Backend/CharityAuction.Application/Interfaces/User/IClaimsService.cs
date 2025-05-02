using CharityAuction.Domain.Entities;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace CharityAuction.Application.Interfaces.User
{
    /// <summary>
    /// Service interface for generating security claims for application users.
    /// </summary>
    public interface IClaimsService
    {
        /// <summary>
        /// Creates a list of claims for the specified user.
        /// </summary>
        /// <param name="user">The user for whom to generate claims.</param>
        /// <returns>A task that represents the asynchronous operation. The task result contains a list of <see cref="Claim"/> objects associated with the user.</returns>
        Task<List<Claim>> CreateClaimsAsync(ApplicationUser user);
    }
}
