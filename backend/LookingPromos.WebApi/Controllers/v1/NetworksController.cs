using LookingPromos.SharedKernel.Extensions;
using LookingPromos.WebApi.Application.Networks.Commands.CreateEventFetchNetworks;
using Microsoft.AspNetCore.Mvc;

namespace LookingPromos.Controllers.v1;

public class NetworksController : ApiBaseController
{
    [HttpPost]
    public async Task<IResult> CreateEventFetchNetworksAsync(CancellationToken cancellationToken) =>
        await Sender
            .Send(new CreateEventFetchNetworksCommand(), cancellationToken)
            .HandleResultAsync();
}