using System.Collections.Generic;
using System.Threading.Tasks;
using NGColombia.Api.Dto.Output;
using NGColombia.Api.Models;

namespace NGColombia.Api.Service
{
    public interface IEventService
    {
        Task<IEnumerable<AvailableTicketInformation>> GetAvailableTicketsSummary();
        Task<IEnumerable<Transaction>> GetTransactions();
        Task<int> ConfirmPendingTransactions();
    }
}