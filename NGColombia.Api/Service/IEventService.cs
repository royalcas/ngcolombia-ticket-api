using System.Collections.Generic;
using System.Threading.Tasks;
using NGColombia.Api.Dto.Output;

namespace NGColombia.Api.Service
{
    public interface IEventService
    {
        Task<IEnumerable<AvailableTicketInformation>> GetAvailableTicketsSummary();
    }
}