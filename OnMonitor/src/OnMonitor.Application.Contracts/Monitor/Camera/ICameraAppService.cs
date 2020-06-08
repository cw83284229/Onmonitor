
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.Monitor
{

    
    public interface ICameraAppService://IApplicationService
        ICrudAppService<
            CameraDto,//定义DTO
            Int32, //实体的主键
            PagedAndSortedResultRequestDto, //获取分页排序
            UpdateCameraDto, //用于创建实体
            UpdateCameraDto> //用于更新实体
    {
        //   public String GetList();

        public Task< PagedResultDto<CameraDto>> GetListByCondition(CameraCondition condition, PagedAndSortedResultRequestDto input);

        public Task< List<CameraDto>> GetListByCameraID(string CameraID);
        public Task<List<CameraDto>> GetListByDVRID(string DVRID);


    }
}
