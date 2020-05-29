using Microsoft.AspNetCore.Authorization;
using Microsoft.International.Converters.TraditionalChineseToSimplifiedConverter;
using System;
using System.Collections.Generic;
using System.Linq;
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

        /// <summary>
        /// 获取DVR/Camera数据树形结构
        /// </summary>
        /// <returns></returns>
        //public string  GetTreeViewAsync()

        //{
        //   // var cameradata = _camerarepository.GetList();
        //    //查询全部数据
        //    var data = from c in _dvrrepository
        //             // join b in _camerarepository on c.DVR_ID equals b.DVR_ID
        //               select new DVRCameraDto
        //               {
        //                   Factory = c.Factory,
        //                   Monitoring_room = c.Monitoring_room,
        //                   Build = c.Camera_build,
        //                   floor = c.Camera_foor,
        //                   DVR_ID = c.DVR_ID,
        //                   //CameraID = b.Camera_ID,
        //                   //Direction = b.Direction,
        //                   //Location = b.Location
        //               };

        //    List<TreeModelEx<DVRCameraDto>> treeModels = new List<Learun.Util.TreeModelEx<DVRCameraDto>>();

        //    List<Tree> trees = new List<Tree>();
        //     //循环加载树形
        //    foreach (var item in data)
        //    {
        //        if (treeModels.Where(u=>u.id==item.Factory).FirstOrDefault()==null)//加载厂区
        //        {
        //            TreeModelEx<DVRCameraDto> treeModel1 = new TreeModelEx<DVRCameraDto>();
        //            treeModel1.id = item.Factory;
        //            treeModel1.parentId = "0";
        //            treeModels.Add(treeModel1);

        //        }
        //        if (treeModels.Where(u => u.id == item.Monitoring_room).FirstOrDefault() == null)//加载监控室
        //        {
        //            TreeModelEx<DVRCameraDto> treeModel1 = new TreeModelEx<DVRCameraDto>();
        //            treeModel1.id = item.Monitoring_room;
        //            treeModel1.parentId = item.Factory;
        //            treeModels.Add(treeModel1);

        //        }

        //        if (treeModels.Where(u => u.id ==$"{item.Build}-{item.floor}").FirstOrDefault() == null)//加载楼层
        //        {
        //            TreeModelEx<DVRCameraDto> treeModel1 = new TreeModelEx<DVRCameraDto>();
        //            treeModel1.id = $"{item.Build}-{item.floor}";
        //            treeModel1.parentId = item.Monitoring_room;
        //            treeModels.Add(treeModel1);

        //        }

        //        if (treeModels.Where(u => u.id == item.DVR_ID).FirstOrDefault() == null)//加载主机
        //        {
        //            TreeModelEx<DVRCameraDto> treeModel1 = new TreeModelEx<DVRCameraDto>();
        //            treeModel1.id = item.DVR_ID;
        //            treeModel1.parentId =$"{item.Build}-{item.floor}";
        //            var camed = _camerarepository.Where(u => u.DVR_ID == item.DVR_ID);//获取镜头信息
        //            List<string> vs = new List<string>();
        //            //镜头信息拼装
        //            //foreach (var tem in camed)
        //            //{
        //            //    string str = $"{tem.Camera_ID} {tem.Build}-{tem.floor} {tem.Direction}{tem.Location}";
        //            //    vs.Add(str);
        //            //}
        //            //treeModel1.data = Newtonsoft.Json.JsonConvert.SerializeObject(vs);
        //            treeModels.Add(treeModel1);

        //        }
        //    }
        //    //调用递归加载
        //    treeModels = treeModels.ToTree();


        //    var dadd = from c in treeModels
        //               select new { c.ChildNodes, c.id };



        //    string requst = Newtonsoft.Json.JsonConvert.SerializeObject(dadd);


        //    return requst;


        //}

        #region 重写增/改方法，增加简繁转换
       
        public override Task<DVRDto> CreateAsync(UpdateDVRDto input)
        {

            string data = Newtonsoft.Json.JsonConvert.SerializeObject(input);

            string data2 = ChineseConverter.Convert(data, ChineseConversionDirection.SimplifiedToTraditional);

            UpdateDVRDto input2 = Newtonsoft.Json.JsonConvert.DeserializeObject<UpdateDVRDto>(data2);

            return base.CreateAsync(input2);
        }
        
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




   



   

