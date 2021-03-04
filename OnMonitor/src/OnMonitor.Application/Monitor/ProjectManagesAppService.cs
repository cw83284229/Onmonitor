using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Monitor
{

    // [Authorize(Roles ="operator")]

    public class ProjectManagesAppService :// ApplicationService
    CrudAppService<
    ProjectManages,//定义实体
    ProjectManagesDto,//定义DTO
    Int32, //实体的主键
    PagedAndSortedResultRequestDto, //获取分页排序
    UpdateProjectManagesDto, //用于创建实体
    UpdateProjectManagesDto> //用于更新实体
    , IProjectManagesAppService

    {

        IRepository<ProjectManages, Int32> _projeckManagesrepository;
        IRepository<Camera, Int32> _camerarepository;


        public ProjectManagesAppService(IRepository<ProjectManages, Int32> projeckManagesrepository, IRepository<Camera, Int32> camerarepository) : base(projeckManagesrepository)
        {

            _camerarepository = camerarepository;
            _projeckManagesrepository = projeckManagesrepository;

         }

        /// <summary>
        /// 条件筛选查询，获取工程列表信息
        /// </summary>
        /// <param name="input">查询条件</param>
        /// <param name="input2">分页信息</param>
        /// <returns></returns>

        public async Task<ResponseResultDto> GetProjectListByCondition(ProjectManagesConditionDto input, PagedSortedRequestDto input2)
        {
            var data = await _projeckManagesrepository.GetListAsync();

            if (!string.IsNullOrEmpty(input.Camera_ID))//筛选镜头信息
            {
                data = data.Where(u => u.Camera_ID.Contains(input.Camera_ID)).ToList();
            }
            if (!string.IsNullOrEmpty(input.Build)&&!string.IsNullOrEmpty(input.Floor))//筛选楼层信息
            {
                data = data.Where(u => u.Build.Contains(input.Build)).Where(i=>i.Floor.Contains(input.Floor)).ToList();
            }

            if (!string.IsNullOrEmpty(input.ManufacturerName))//筛选厂商
            {
                data = data.Where(u => u.ManufacturerName.Contains(input.ManufacturerName)).ToList();
            }
            if (!string.IsNullOrEmpty(input.ProjectName))//筛选工程名称
            {
                data = data.Where(u => u.ProjectName.Contains(input.ProjectName)).ToList();
            }
            if (!string.IsNullOrEmpty(input.ProjectOrder))//筛选工程单号
            {
                data = data.Where(u => u.ProjectOrder.Contains(input.ProjectOrder)).ToList();
            }
            if (!string.IsNullOrEmpty(input.AcceptanceDataStart)&&!string.IsNullOrEmpty(input.AcceptanceDataStart))//筛选验收时间
            {
                
                data = data.Where(u => string.Compare(u.AcceptanceData,input.AcceptanceDataStart) >= 0 && string.Compare(u.AcceptanceData, input.AcceptanceDataEnd) <= 0).ToList();
            }
            var data2= ObjectMapper.Map<List<ProjectManages>, List<ProjectManagesDto>>(data);


           var data1 = data2.AsQueryable().PageBy(input2.SkipCount, input2.MaxResultCount);
            var TTT = new PagedResultDto<ProjectManagesDto>() { Items = data1.ToList(), TotalCount = data.Count };

            var result = new ResponseResultDto();
            result.IsSuccess(TTT);

            return result;
        }


        /// <summary>
        /// 依据订单号查询工程详情
        /// </summary>
        /// <param name="ProjectOrder">订单号</param>
        /// <returns></returns>
        public async Task<RequstProjectManagesDto> GetProjectDto(string ProjectOrder)

        {

           var data=  _projeckManagesrepository.Where(u => u.ProjectOrder.Contains(ProjectOrder)).FirstOrDefault();

           List<UpdateCameraDto> updateCameralist = Newtonsoft.Json.JsonConvert.DeserializeObject<List<UpdateCameraDto>>(data.Camera_ID);

           var requst=  ObjectMapper.Map<ProjectManages, RequstProjectManagesDto>(data);

          
            requst.Camera_ID = updateCameralist;

            return requst;
           
        
        }





    }

      
       


 }



   

