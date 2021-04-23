using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.Monitor.Alarm
{

    // [Authorize(Roles ="admin")]
    public class AlarmManageStateAppService :// ApplicationService
  CrudAppService<
  AlarmManageState,//定义实体
  AlarmManageStateDto,//定义DTO
  Int32, //实体的主键
  PagedAndSortedResultRequestDto, //获取分页排序
  UpdateAlarmManageStateDto, //用于创建实体
  UpdateAlarmManageStateDto> //用于更新实体
  , IAlarmManageStateAppService

    {

        IRepository<AlarmStatus, Int32> _alarmStatusrepository;
        IRepository<AlarmManageState, Int32> _repository;
        IRepository<Alarm, Int32> _alarmrepository;
        IRepository<AlarmHost, Int32> _alrmHostrepository;

        public AlarmManageStateAppService(IRepository<AlarmManageState, Int32> repository, IRepository<AlarmStatus, Int32> alarmStatusrepository, IRepository<Alarm, Int32> alarmrepository, IRepository<AlarmHost, Int32> alrmHostrepository) : base(repository)
        {
            _alarmStatusrepository = alarmStatusrepository;
            _repository = repository;
            _alrmHostrepository = alrmHostrepository;
            _alarmrepository = alarmrepository;
        }


        /// <summary>
        /// 条件筛选查询
        /// </summary>
        /// <param name="condition"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<PagedResultDto<RequstAlarmManageStateDto>> GetRequstListAsync(ConditionAlarmManageStateDto condition, PagedSortedRequestDto input)
        {

            //加载RequstAlarmManageStateDto
            var data = from a in _alrmHostrepository
                          join b in _alarmrepository on a.AlarmHost_ID equals b.AlarmHost_ID
                          join d in _repository on b.Alarm_ID equals d.Alarm_ID
                          select new RequstAlarmManageStateDto
                          {
                              AlarmHost_ID = a.AlarmHost_ID,
                              Monitoring_room = a.Monitoring_room,


                              department = b.department,
                              Camera_ID = b.Camera_ID,
                              Alarm_ID = b.Alarm_ID,
                              Build = b.Build,
                              floor = b.floor,
                              Location = b.Location,
                              Cost_code = b.Cost_code,
                              IsAlertor = b.IsAlertor,
                              IsOpenOrClosed = b.IsOpenOrClosed,
                              Channel_ID = b.Channel_ID,
                              GeteType = b.GeteType,
                              SensorType = b.SensorType,

                              Remark = d.Remark,
                              TreatmentReply = d.TreatmentReply,
                              TreatmentTime = d.TreatmentTime,
                              TreatmentTimeState = d.TreatmentTimeState,
                              WithdrawMan = d.WithdrawMan,
                              WithdrawRemark = d.WithdrawRemark,
                              WithdrawTime = d.WithdrawTime,
                              AlarmTime = d.AlarmTime,
                              DefenceTime = d.DefenceTime,
                              AnomalyType=d.AnomalyType,
                              TreatmentMan=d.TreatmentMan,
                              Id = d.Id,
                              CreationTime = d.CreationTime,
                              CreatorId = d.CreatorId,
                              LastModificationTime = d.LastModificationTime,
                              LastModifierId = d.LastModifierId
                          };

            var dd = data.ToList();
            var DDF = _repository.ToList();
            var DFD = _alarmrepository.ToList();



            //条件筛选
            //监控室筛选
            if (!string.IsNullOrEmpty(condition.Monitoring_room))
            {
                data = data.Where(u => u.Monitoring_room.Contains(condition.Monitoring_room));
            }
            //楼栋楼层筛选
            if (!string.IsNullOrEmpty(condition.Build)&&!string.IsNullOrEmpty(condition.floor))
            {
                data = data.Where(u => u.Build.Contains(condition.Build)).Where(i => i.floor.Contains(condition.floor));
            }
            //位置筛选
            if (!string.IsNullOrEmpty(condition.Location))
            {
                data = data.Where(u => u.Location.Contains(condition.Location));
            }
            //报警主机号筛选
            if (!string.IsNullOrEmpty(condition.AlarmHost_ID))
            {
                data = data.Where(u => u.AlarmHost_ID.Contains(condition.AlarmHost_ID));
            }
          
            //ID筛选
            if (!string.IsNullOrEmpty(condition.Alarm_ID))
            {
                data = data.Where(u => u.Alarm_ID.Contains(condition.Alarm_ID));
            }
            //报警时间筛选
            if (!string.IsNullOrEmpty(condition.AlarmTimeStart)&& !string.IsNullOrEmpty(condition.AlarmTimeEnd))
            {
                data = data.Where(u =>string.Compare(u.AlarmTime,condition.AlarmTimeStart)>=0&&string.Compare(u.AlarmTime,condition.AlarmTimeEnd)<=0);
            }
            //开岗状态筛选
            if (condition.IsOpenOrClosed!=null)
            {
                data = data.Where(u => u.IsOpenOrClosed == condition.IsOpenOrClosed);
            }
           
            var requstdata = data.OrderByDescending(u =>u.LastModificationTime).PageBy(input.SkipCount, input.MaxResultCount);

           

            return new PagedResultDto<RequstAlarmManageStateDto>() { Items = requstdata.ToList(), TotalCount = data.Count() };

        }



        /// <summary>
        /// 重写新增方法，内置修改AlarmStatus表treatmentState内容“1”
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<AlarmManageStateDto> CreateAsync(UpdateAlarmManageStateDto input)
        {
          var status=  _alarmStatusrepository.Where(u => u.Alarm_ID == input.Alarm_ID).FirstOrDefault();

            if (status!=null)
            {
                status.TreatmentState = 1;

                _alarmStatusrepository.UpdateAsync(status);
            }


            return base.CreateAsync(input);
        }
        /// <summary>
        /// 重写修改方法，修改自动更新AlarmStatus.TreatmentState=0
        /// </summary>
        /// <param name="id"></param>
        /// <param name="input"></param>
        /// <returns></returns>
        public override Task<AlarmManageStateDto> UpdateAsync(int id, UpdateAlarmManageStateDto input)
        {
            
            var status = _alarmStatusrepository.Where(u => u.Alarm_ID == input.Alarm_ID).FirstOrDefault();

            if (status != null)
            {
                if (string.IsNullOrEmpty(input.TreatmentTimeState))
                {
                    status.TreatmentState = 1;
                    _alarmStatusrepository.UpdateAsync(status);
                }
                else
                {
                    status.TreatmentState = 0;
                    _alarmStatusrepository.UpdateAsync(status);
                }
            }


            return base.UpdateAsync(id, input);
        }

        /// <summary>
        /// 依据报警号查询记录信息，并按时间倒序排序
        /// </summary>
        /// <param name="Alarm_ID"></param>
        /// <returns></returns>
        public List<AlarmManageStateDto> GetAlarmManageStates(string Alarm_ID)
        {
         var data=   _repository.GetQueryableAsync().Result;

            if (!string.IsNullOrEmpty(Alarm_ID))
            {
              data=  data.Where(u => u.Alarm_ID == Alarm_ID);
            }
            else
            {
                    
            }
            data = data.OrderByDescending(u=>u.LastModificationTime);

            var requst = ObjectMapper.Map<List<AlarmManageState>, List<AlarmManageStateDto>>(data.ToList());

            return requst;
        
        
        }

    }








    //}






}




   



   

