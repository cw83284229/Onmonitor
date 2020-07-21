using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Linq.Dynamic.Core;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Volo.Abp.ObjectMapping;

namespace OnMonitor.Monitor
{


    public class DVRChannelInfoAppService ://IApplicationService
        CrudAppService<
            DVRChannelInfo,
            DVRChannelInfoDto,//定义DTO
            Int32, //实体的主键
            PagedAndSortedResultRequestDto, //获取分页排序
            UpdateDVRChannelInfoDto, //用于创建实体
            UpdateDVRChannelInfoDto>,IDVRChannelInfoAppService //用于更新实体
    {
        IRepository<DVRChannelInfo, Int32> _repository;
        IRepository<DVR, Int32> _dVRrepository;
        IRepository<Camera, Int32> _camerarepository;
        public DVRChannelInfoAppService(IRepository<DVRChannelInfo, Int32> repository, IRepository<DVR, Int32> dVRrepository, IRepository<Camera, Int32> camerarepository) : base(repository)
        {
            _repository = repository;
            _camerarepository = camerarepository;
            _dVRrepository = dVRrepository;

        }
        /// <summary>
        /// 查询镜头检查结果
        /// </summary>
        /// <param name="ChannelCheck"></param>
        /// <param name="ImageCheck"></param>
        /// <param name="DvrRoom"></param>
        /// <param name="input"></param>
        /// <returns></returns>
       public PagedResultDto<RequstDVRChannelInfoDto> GetDVRChannels(bool? ChannelCheck,bool? ImageCheck,string DvrRoom,PagedSortedRequestDto input)
        {

            var data = from a in _repository
                                      join q in _camerarepository on a.Camera_ID equals q.Camera_ID into q_join
                                      from b in q_join.DefaultIfEmpty()
                                      join c in _dVRrepository on a.DVR_ID equals c.DVR_ID into c_join
                                      from v in c_join.DefaultIfEmpty()
                                      select new RequstDVRChannelInfoDto
                                      {
                                          DataChannelName=a.DataChannelName,
                                          DVRChannelEncoding=a.DVRChannelEncoding,
                                          CameraType=a.CameraType,
                                          Camera_ID=a.Camera_ID,
                                          DVRChannelName=a.DVRChannelName,
                                          ChannelNameCheck=a.ChannelNameCheck,
                                          channel_ID=a.channel_ID,
                                          DVR_Room=v.Monitoring_room,
                                          Factory=v.Factory,
                                          ImageCheck=a.ImageCheck,
                                          DVR_ID=a.DVR_ID,
                                          Id=a.Id,
                                          LastUpdateTime=a.LastUpdateTime,
                                          Remark=a.Remark
                                      };
          //  IQueryable<RequstDVRChannelInfoDto> data;

            if (ChannelCheck!=null)
            {
                data = data.Where(u => u.ChannelNameCheck == ChannelCheck).Where(i => i.DataChannelName != "无");
            }
            if (ImageCheck != null)
            {
                data =data.Where(u => u.ImageCheck == ImageCheck); 
            }
            if (!string.IsNullOrEmpty(DvrRoom))
            {
                data = data.Where(u => u.DVR_Room.Contains(DvrRoom));
            }

            var  data1 = data.PageBy(input.SkipCount, input.MaxResultCount).ToList();

         

          return new PagedResultDto<RequstDVRChannelInfoDto>() { Items = data1, TotalCount = data.Count() };

        }

        






    }
      
}
