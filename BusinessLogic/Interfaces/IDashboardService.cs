using BusinessLogic.DTOs;

namespace BusinessLogic.Interfaces
{
    public interface IDashboardService
    {
        Task<GetDashboardDTO> GetDashboard();
    }
}