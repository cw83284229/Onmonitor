using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;

namespace OnMonitor.Monitor
{

    // [Authorize(Roles ="operator")]

    public interface IDVRCheckInfoAppService : ICrudAppService<DVRCheckInfoDto,//定义DTO
                       Int32, //实体的主键
                       PagedAndSortedResultRequestDto, //获取分页排序
                       UpdateDVRCheckInfoDto, //用于创建实体
                       UpdateDVRCheckInfoDto>




    {
        public List<DVRCheckInfoDto> GetDVRInfoCheckFalseByDVRroom(string DVR_room);
        public Task<PagedResultDto<DVRCheckInfoDto>> GetDVRInfoByCondition(string DVR_room, bool? DiskChenk, bool? DVR_Online, bool? SNChenk, bool? TimeInfoChenk, PagedSortedRequestDto input);
        //   public Task<PagedResultDto<DVRCheckInfoDto>> GetDVRInfoByCondition(string DVR_room, bool? DiskChenk, bool? DVR_Online, bool? SNChenk, bool? TimeInfoChenk, PagedAndSortedResultRequestDto input);
    }





}



   

