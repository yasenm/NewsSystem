using NewsSystem.Data.Models;
using NewsSystem.Data.Services.Contracts.VisitorsIps;
using NewsSystem.Data.UnitOfWork;
using System.Linq;
using System;

namespace NewsSystem.Data.Services.Services.VisitorsIps
{
    public class VisitorsIpsService : IVisitorsIpsService
    {
        private INewsSystemData _data;

		public VisitorsIpsService(INewsSystemData data)
        {
            _data = data;
        }

        public VisitorIp AddOrGetVisitorIp(string userHostAddress)
        {
            try
            {
                var ip = _data.VisitorsIps.All().FirstOrDefault(vi => vi.IpAddress == userHostAddress);
                if (ip != null)
                {
                    return ip;
                }
                var ipAddress = new VisitorIp { IpAddress = userHostAddress, LastVisit = DateTime.Now };
                _data.VisitorsIps.Add(ipAddress);
                _data.SaveChanges();
                return ipAddress;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
