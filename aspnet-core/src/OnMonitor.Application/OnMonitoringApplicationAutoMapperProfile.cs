using AutoMapper;
using OnMonitor.Monitor;
using OnMonitor.MonitorRepair;

namespace OnMonitor
{
    public class OnMonitorApplicationAutoMapperProfile : Profile
    {
        public OnMonitorApplicationAutoMapperProfile()
        {
            /* You can configure your AutoMapper mapping configuration here.
             * Alternatively, you can split your mapping configurations
             * into multiple profile classes for a better organization. */
            CreateMap<Camera, CameraDto>();
            CreateMap<UpdateCameraDto, Camera>();
            CreateMap<DVR, DVRDto>();
            CreateMap<UpdateDVRDto, DVR>();
            CreateMap<CameraRepair, CameraRepairDto>();
            CreateMap<UpdateCameraRepairDto, CameraRepair>();
            CreateMap<ProjectManages, ProjectManagesDto>();
            CreateMap<ProjectManagesDto, ProjectManages>();
            CreateMap<DVRCheckInfo, DVRCheckInfoDto>();
            CreateMap<UpdateDVRCheckInfoDto, DVRCheckInfo>();
            CreateMap<Alarm,AlarmDto>();
            CreateMap<UpdateAlarmDto, Alarm>();


        }
    }
}
