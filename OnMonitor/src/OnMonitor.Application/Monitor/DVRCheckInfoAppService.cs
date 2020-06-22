using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Monitor
{

    // [Authorize(Roles ="admin")]

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

        IRepository<DVRCheckInfo, Int32> _dvrchenkrepository;
        IRepository<DVR, Int32> _dvrrepository;
        public DVRCheckInfoAppService(IRepository<DVRCheckInfo, Int32> dvrcheckrepository,IRepository<DVR, Int32> dvrrepository) : base(dvrcheckrepository)
        {
           _dvrchenkrepository = dvrcheckrepository;
            _dvrrepository = dvrrepository;
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
        /// 按条件获取主机异常信息 默认false 单条件其他为null
        /// </summary>
        /// <param name="DVR_room">监控室</param>
        /// <param name="DiskChenk">硬盘检查</param>
        /// <param name="DVR_Online">在线检查</param>
        /// <param name="SNChenk">Sn号检查</param>
        /// <param name="TimeInfoChenk">时间检查</param>
        /// <returns></returns>
        public async Task<PagedResultDto<DVRCheckInfoDto>> GetDVRInfoByCondition( string DVR_room,bool? DiskChenk, bool? DVR_Online, bool? SNChenk ,bool? TimeInfoChenk, PagedSortedRequestDto input)
        {

            var data1 = await _dvrchenkrepository.GetListAsync();
            var dvrdata = _dvrrepository.Where(u => u.Monitoring_room == DVR_room);

            List<DVRCheckInfo> data2 = new List<DVRCheckInfo>() ;
            List<DVRCheckInfo> data3 = new List<DVRCheckInfo>();
            if (DVR_room.IsNullOrEmpty())
            {
                data2 = data1;
            }
            foreach (var item in dvrdata)
            {
                data2.Add( data1.Where(u => u.DVR_ID == item.DVR_ID).FirstOrDefault());
            }

            foreach (var item in data2)
            {
                if (item.DiskChenk == DiskChenk|| item.DVR_Online == DVR_Online || item.SNChenk == SNChenk || item.TimeInfoChenk == TimeInfoChenk)
                {
                    data3.Add(item);
                }
            }


            var data9 = data3.Distinct();

            var data = ObjectMapper.Map<List<DVRCheckInfo>, List<DVRCheckInfoDto>>(data3);

           var data4 = data.Skip(input.SkipCount).Take(input.MaxResultCount);
              

            return new PagedResultDto<DVRCheckInfoDto> { TotalCount = data.Count(), Items = data4.ToList() };




        }


        /// <summary>
        /// 按监控室获取主机异常信息
        /// </summary>
        /// <param name="DVR_room"></param>
        /// <returns></returns>
        public List<DVRCheckInfoDto> GetDVRInfoCheckFalseByDVRroom(string DVR_room)
        {


            var data1 = _dvrchenkrepository.ToList();
           
            
            var dvrdata = _dvrrepository.Where(u => u.Monitoring_room == DVR_room);
            if (dvrdata.ToList()!=null)
            {
                var FF = dvrdata.Count();
            }
            List<DVRCheckInfo> data2 = new List<DVRCheckInfo>();
            List<DVRCheckInfo> data3 = new List<DVRCheckInfo>();
            if (DVR_room.IsNullOrEmpty())
            {
                data2 = data1.ToList();
            }
            foreach (var item in dvrdata)
            {
                data2.Add(data1.Where(u => u.DVR_ID == item.DVR_ID).FirstOrDefault());
            }

            foreach (var item in data2)
            {
                if (item.DiskChenk==false||item.DVR_Online==false||item.SNChenk==false||item.TimeInfoChenk==false)
                {
                    data3.Add(item);
                }
            }

            data3 = data3.Distinct().ToList();

            var data = ObjectMapper.Map<List<DVRCheckInfo>, List<DVRCheckInfoDto>>(data3);

            //  var data3 = data.AsQueryable().OrderBy(input.Sorting).PageBy(input.SkipCount, input.MaxResultCount);

            return data;

















        
    }
}

}