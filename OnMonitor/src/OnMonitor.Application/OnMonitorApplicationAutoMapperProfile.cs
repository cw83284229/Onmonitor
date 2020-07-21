using AutoMapper;
using OnMonitor.MenusInfos;
using OnMonitor.MenusInfos.Dtos;
using OnMonitor.Monitor;
using OnMonitor.MonitorRepair;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;

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
            CreateMap<UpdateCameraDto, Camera>(MemberList.Source);
            CreateMap<DVR, DVRDto>();
            CreateMap<UpdateDVRDto, DVR>(MemberList.Source);
            CreateMap<CameraRepair, CameraRepairDto>();
            CreateMap<UpdateCameraRepairDto, CameraRepair>(MemberList.Source);
            CreateMap<ProjectManages, ProjectManagesDto>();
            CreateMap<ProjectManagesDto, ProjectManages>(MemberList.Source);
            CreateMap<DVRCheckInfo, DVRCheckInfoDto>();
            CreateMap<UpdateDVRCheckInfoDto, DVRCheckInfo>(MemberList.Source);
            CreateMap<Alarm, AlarmDto>();
            CreateMap<UpdateAlarmDto, Alarm>(MemberList.Source);
            CreateMap<MonitorRoom, MonitorRoomDto>();
            CreateMap<UpdateMonitorRoomDto,MonitorRoom>(MemberList.Source);
            CreateMap<DVRChannelInfo, DVRChannelInfoDto>();
            CreateMap<UpdateDVRChannelInfoDto, DVRChannelInfo>(MemberList.Source);


            // CreateMap<SystemMenu, SystemMenuDto>();
            CreateMap<CreateUpdateSystemMenuDto, SystemMenu>(MemberList.Source);


            CreateMap<MaterialRepertory, MaterialRepertoryDto>();
            CreateMap<CreateUpdateMaterialRepertoryDto, MaterialRepertory>(MemberList.Source);
            CreateMap<ProcurementContent, ProcurementContentDto>();
            CreateMap<CreateUpdateProcurementContentDto, ProcurementContent>(MemberList.Source);
            CreateMap<ProcurementDeltail, ProcurementDeltailDto>();
            CreateMap<CreateUpdateProcurementDeltailDto, ProcurementDeltail>(MemberList.Source);
            CreateMap<ProductInfo, ProductInfoDto>();
            CreateMap<CreateUpdateProductInfoDto, ProductInfo>(MemberList.Source);
            CreateMap<SaleContent, SaleContentDto>();
            CreateMap<CreateUpdateSaleContentDto, SaleContent>(MemberList.Source);
            CreateMap<SaleDeltail, SaleDeltailDto>();
            CreateMap<CreateUpdateSaleDeltailDto, SaleDeltail>(MemberList.Source);
        }
    }
}