using AutoMapper;
using OnMonitor.Monitor;
using OnMonitor.MenusInfos;
using OnMonitor.MenusInfos.Dtos;
using OnMonitor.OrderMaterials;
using OnMonitor.OrderMaterials.Dtos;
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


            CreateMap<SystemMenu, SystemMenuDto>();
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
