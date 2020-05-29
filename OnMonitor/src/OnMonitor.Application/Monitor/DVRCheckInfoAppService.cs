using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using OnMonitor.Monitor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Monitor
{

    // [Authorize(Roles ="operator")]

    public class DVRCheckInfoAppService :// ApplicationService
    CrudAppService<
    DVRCheckInfo,//定义实体
   DVRCheckInfoDto,//定义DTO
    Int32, //实体的主键
    PagedAndSortedResultRequestDto, //获取分页排序
    UpdateDVRCheckInfoDto, //用于创建实体
    UpdateDVRCheckInfoDto> //用于更新实体
    , IDVRCheckInfoAppService

    {

        IRepository<DVRCheckInfo, Int32> _repository;
        public DVRCheckInfoAppService(IRepository<DVRCheckInfo, Int32> repository) : base(repository)
        {
            _repository = repository;
        }
        #region 手动加载方法
        ///// <summary>
        ///// 重写方法，josn格式导入数据库
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public  async Task<DVRCheckInfoDto> CreateinfoAsync(string input1)
        //{
        //    var input = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateDVRCheckInfoDto>(input1);
        //    var DD = ObjectMapper.Map<UpdateDVRCheckInfoDto, DVRCheckInfo>(input);
        //    DD.DVRDISK = Newtonsoft.Json.JsonConvert.SerializeObject(input.DVRDISK);
        //    DD.DVRChannelInfo = Newtonsoft.Json.JsonConvert.SerializeObject(input.DVRChannelInfo);
        //    DD.LibraryChannelInfo = Newtonsoft.Json.JsonConvert.SerializeObject(input.LibraryChannelInfo);

        //    var list = await _repository.InsertAsync(DD);

        //    #region 配置映射关系 
        //    //配置新映射
        //    DVRCheckInfoDto dvrcheckInfoDto = new DVRCheckInfoDto();
        //    dvrcheckInfoDto.DVR_ID = list.DVR_ID;
        //    dvrcheckInfoDto.DVR_SN = list.DVR_SN;
        //    dvrcheckInfoDto.DVR_type = list.DVR_type;
        //    dvrcheckInfoDto.DVR_Online = list.DVR_Online;
        //    dvrcheckInfoDto.DVR_Channel = list.DVR_Channel;
        //    dvrcheckInfoDto.ChannelChenk = list.ChannelChenk;
        //    dvrcheckInfoDto.CreationTime = list.CreationTime;
        //    dvrcheckInfoDto.CreatorId = list.CreatorId;
        //    dvrcheckInfoDto.DiskTotal = list.DiskTotal;
        //    dvrcheckInfoDto.Id = list.Id;
        //    dvrcheckInfoDto.InfoChenk = list.InfoChenk;
        //    dvrcheckInfoDto.LastModificationTime = list.LastModificationTime;
        //    dvrcheckInfoDto.LastModifierId = list.LastModifierId;
        //    dvrcheckInfoDto.Remark = list.Remark;

        //    dvrcheckInfoDto.DVRDISK = list.DVRDISK;
        //    dvrcheckInfoDto.DVRChannelInfo = list.DVRChannelInfo;
        //    dvrcheckInfoDto.LibraryChannelInfo = list.LibraryChannelInfo;
        //    return dvrcheckInfoDto;
        //    #endregion
        //}

        ///// <summary>
        ///// 重写方法，获取DVR全部信息并构建
        ///// </summary>
        ///// <param name="input"></param>
        ///// <returns></returns>
        //public async override Task<PagedResultDto<DVRCheckInfoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        //{

        //    var data = await _repository.GetListAsync();
        //    List<DVRCheckInfoDto> listCheckInfoDto = new List<DVRCheckInfoDto>();

        //    foreach (var item in data)
        //    {
        //        #region 配置映射关系 
        //        //配置新映射
        //        DVRCheckInfoDto dvrcheckInfoDto = new DVRCheckInfoDto();
        //        dvrcheckInfoDto.DVR_ID = item.DVR_ID;
        //        dvrcheckInfoDto.DVR_SN = item.DVR_SN;
        //        dvrcheckInfoDto.DVR_type = item.DVR_type;
        //        dvrcheckInfoDto.DVR_Online = item.DVR_Online;
        //        dvrcheckInfoDto.DVR_Channel = item.DVR_Channel;
        //        dvrcheckInfoDto.ChannelChenk = item.ChannelChenk;
        //        dvrcheckInfoDto.CreationTime = item.CreationTime;
        //        dvrcheckInfoDto.CreatorId = item.CreatorId;
        //        dvrcheckInfoDto.DiskTotal = item.DiskTotal;
        //        dvrcheckInfoDto.Id = item.Id;
        //        dvrcheckInfoDto.InfoChenk = item.InfoChenk;
        //        dvrcheckInfoDto.LastModificationTime = item.LastModificationTime;
        //        dvrcheckInfoDto.LastModifierId = item.LastModifierId;
        //        dvrcheckInfoDto.Remark = item.Remark;

        //        dvrcheckInfoDto.DVRDISK = item.DVRDISK;
        //        dvrcheckInfoDto.DVRChannelInfo = item.DVRChannelInfo;
        //        dvrcheckInfoDto.LibraryChannelInfo = item.LibraryChannelInfo;
        //        #endregion
        //        listCheckInfoDto.Add(dvrcheckInfoDto);
        //    }

        //  //  listCheckInfoDto = listCheckInfoDto.AsQueryable().Page(input.SkipCount, input.MaxResultCount).OrderBy(d =>d.Id).ToList();


        //    return new PagedResultDto<DVRCheckInfoDto>()

        //    {
        //        TotalCount = listCheckInfoDto.Count(),
        //        Items = listCheckInfoDto
        //    };
        //} 
        #endregion

        /// <summary>
        /// 条件查询获取主机信息
        /// </summary>
        /// <param name="condition"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<DVRCheckInfoDto>> GetDVRInfoByCondition(UpdateDVRCheckInfoDto condition)
        {

            var data1 = await _repository.GetListAsync();

            List<DVRCheckInfoDto> data2 = new List<DVRCheckInfoDto>();
            if (!String.IsNullOrEmpty(condition.DVR_ID))
            {
                data1 = data1.Where(u => u.DVR_ID == condition.DVR_ID).ToList();
            }
            if (!String.IsNullOrEmpty(condition.DVR_SN))
            {
                data1 = data1.Where(u => u.DVR_SN == condition.DVR_SN).ToList();
            }

            if (condition.DiskTotal!=null)
            {
                data1 = data1.Where(u => u.DiskTotal >= condition.DiskTotal).ToList();
            }

            if (condition.DVR_Channel!=null)
            {
                data1 = data1.Where(u => u.DVR_Channel >= condition.DVR_Channel).ToList();
            }
            if (!String.IsNullOrEmpty(condition.DVRChannelInfo))
            {
                data1 = data1.Where(u => u.DVRChannelInfo.Contains(condition.DVRChannelInfo)).ToList();
            }
            if (!String.IsNullOrEmpty(condition.DVRDISK))
            {
                data1 = data1.Where(u => u.DVRDISK.Contains(condition.DVRDISK)).ToList();
            }
            if (condition.DVR_Online!=null)
            {  
             data1 = data1.Where(u => u.DVR_Online == condition.DVR_Online).ToList();
            }

            //if (condition.InfoChenk!=null)
            //{
            //   data1 = data1.Where(u => u.InfoChenk == condition.InfoChenk).ToList();
            //}

            var data = ObjectMapper.Map<List<DVRCheckInfo>, List<DVRCheckInfoDto>>(data1);

           // data = data.AsQueryable().PageBy<CameraDto>(input.SkipCount, input.MaxResultCount).OrderBy(d => input.Sorting ?? d.Camera_ID).ToList();

            return new PagedResultDto<DVRCheckInfoDto> { TotalCount = data.Count, Items = data };

















        }
    }

}