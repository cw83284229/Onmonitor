
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.Monitor
{


    public interface IReportFormsAppService :IApplicationService
       
    {
        public List<ReportFormsDto> GetReportFormsByMonitorRoom();
        public List<ReportFormsDto> GetReportFormsByBuild();
        public List<ReportFormsDto> GetReportFormsBydepartment();
        public List<ReportFormsDto> GetReportFormsByYear();

        public List<ReportFormsDto> GetReportFormsByMonitorRoomorAnomalyCondition(string StartTime, string EndTime, string AnomalyGrade, string AnomalyType);

        public List<ReportFormsDto> GetReportFormsByTime(string StartTime, string EndTime, string AnomalyGrade, string AnomalyType, string MonitorRoom);
    }
}
