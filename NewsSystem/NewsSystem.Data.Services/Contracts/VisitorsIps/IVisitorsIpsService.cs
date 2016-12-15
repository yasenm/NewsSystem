using NewsSystem.Data.Models;

namespace NewsSystem.Data.Services.Contracts.VisitorsIps
{
    public interface IVisitorsIpsService
    {
        VisitorIp AddOrGetVisitorIp(string userHostAddress);
    }
}
