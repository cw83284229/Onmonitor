using Microsoft.AspNetCore.Authorization;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Monitor
{

 // [Authorize(Roles ="admin")]
    public class DVRAppService :// ApplicationService
  CrudAppService<
  DVR,//定义实体
  DVRDto,//定义DTO
  Int32, //实体的主键
  PagedAndSortedResultRequestDto, //获取分页排序
  UpdateDVRDto, //用于创建实体
  UpdateDVRDto> //用于更新实体
  , IDVRAppService

    {
        IRepository<DVR, Int32> _dvrrepository;
        IRepository<Camera, Int32> _camerarepository;
        public DVRAppService(IRepository<DVR, Int32> dvrrepository, IRepository<Camera, Int32> camerarepository) : base(dvrrepository)
        {

            _camerarepository = camerarepository;
            _dvrrepository = dvrrepository;
        }

      
        #region DVR条件查询
        /// <summary>
        /// DVR多条件查询
        /// </summary>
        /// <param name="Monitoring_room"></param>
        /// <param name="Build"></param>
        /// <param name="Floor"></param>
        /// <param name="DVR_ID"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<DVRDto>> GetListByCondition(string Monitoring_room, String Build, String Floor, string DVR_ID)
        {
            var data1 = await _dvrrepository.GetListAsync();

            List<DVR> data2 = new List<DVR>();
            if (!String.IsNullOrEmpty(Monitoring_room))
            {
                data1 = data1.Where(u => u.Monitoring_room == Monitoring_room).ToList();
            }
            if (!String.IsNullOrEmpty(Build))
            {
                data1 = data1.Where(u => u.Camera_build == Build).ToList();
            }

            if (!String.IsNullOrEmpty(Floor))
            {
                data1 = data1.Where(u => u.Camera_foor == Floor).ToList();
            }

            if (!String.IsNullOrEmpty(DVR_ID))
            {
                data1 = data1.Where(u => u.DVR_ID == DVR_ID).ToList();
            }


            var data = ObjectMapper.Map<List<DVR>, List<DVRDto>>(data1);

            return new PagedResultDto<DVRDto>() { TotalCount = data.Count, Items = data };

        }

        #endregion

        //public async Task<List<TreeViewDto>> GetTreeViewAsync()

        //{

        //    var data = from a in _dvrrepository
        //               join b in _camerarepository on a.DVR_ID equals b.DVR_ID
        //               select new dvrCameraDto
        //               {
        //                   Factory = a.Factory,
        //                   Monitoring_room = a.Monitoring_room,
        //                   Camera_build = b.Build,
        //                   Camera_foor = b.floor,
        //                   DVR_ID = a.DVR_ID,
        //                   channel_ID = b.channel_ID,
        //                   Camera_ID = b.Camera_ID,
        //                   Location = b.Direction + b.Location
        //               };





        //    List<TreeViewDto> treeViewdata = new List<TreeViewDto>();
        //    foreach (var item in data)
        //    {
        //        TreeViewDto tree = new TreeViewDto();
        //        tree.name = item.Factory;
        //        tree.id = item.;

        //        treeViewdata.Add(tree);
        //    }








        //}

        #region 重写增/改方法，增加简繁转换
        [Authorize("CCTV_Modification")]
        public override Task<DVRDto> CreateAsync(UpdateDVRDto input)
        {

            string data = Newtonsoft.Json.JsonConvert.SerializeObject(input);

            string data2 = ChineseConverter.Convert(data, ChineseConversionDirection.SimplifiedToTraditional);

            UpdateDVRDto input2 = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateDVRDto>(data2);

            return base.CreateAsync(input2);
        }
        [Authorize("CCTV_Modification")]
        public override Task<DVRDto> UpdateAsync(int id, UpdateDVRDto input)
        {
            string data = Newtonsoft.Json.JsonConvert.SerializeObject(input);

            string data2 = ChineseConverter.Convert(data, ChineseConversionDirection.SimplifiedToTraditional);

            UpdateDVRDto input2 = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateDVRDto>(data2);

            return base.UpdateAsync(id, input2);
        }

        #endregion











    }








    //}






}




   



   

