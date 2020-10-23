using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.MonitorRepair
{
   public interface ICameraRepairRepository:IRepository<CameraRepair,int>
    {
    }
}
