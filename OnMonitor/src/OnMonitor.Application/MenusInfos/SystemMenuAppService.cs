using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using OnMonitor.MenusInfos.Dtos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace OnMonitor.MenusInfos
{
    public class SystemMenuAppService : CrudAppService<SystemMenu, SystemMenuDto, long, PagedAndSortedResultRequestDto, CreateUpdateSystemMenuDto, CreateUpdateSystemMenuDto>,
        ISystemMenuAppService
    {
        IRepository<SystemMenu, long> _repository;

        public SystemMenuAppService(IRepository<SystemMenu, long> repository) : base(repository)
        {
            _repository = repository;
        }

        /// <summary>
        /// ��ȡ���νṹ/�޲λ�ȡȫ���ڵ�/PID��ȡ��ǰ·���½ڵ�
        /// </summary>
        /// <param name="Pid">���ڵ�</param>
        /// <returns></returns>
        public SystemMenuDto GetSystemMenuDtobyPid(long? Pid)
        {
            SystemMenuDto systemMenuDto = new SystemMenuDto();
            var data = _repository.Where(u=>u.Id>0).ToList();
            var data1 = data.Where(u => u.Id == Pid).FirstOrDefault();

            if (Pid==null)
            {

                systemMenuDto.Id = 0;
                systemMenuDto.icon = "";
                systemMenuDto.href = "";
                systemMenuDto.title = "��Ŀ¼";
                
            }
            else
            {
                systemMenuDto.Id = data1.Id;
                systemMenuDto.icon = data1.icon;
                systemMenuDto.href = data1.href;
                systemMenuDto.title = data1.title;
            }

            var requst = GetSystemMenuDto(data, systemMenuDto);
            return requst;


        }

        /// <summary>
        /// ѭ������SystemMenu���νṹ
        /// </summary>
        /// <param name="listsystemMenus"></param>
        /// <param name="systemMenuDto"></param>
        /// <returns></returns>
        protected SystemMenuDto GetSystemMenuDto(List<SystemMenu> listsystemMenus, SystemMenuDto systemMenuDto)
        {

            if (systemMenuDto == null)
            {

            }
            var childdatalist = listsystemMenus.Where(u => u.pid == systemMenuDto.Id);

            if (childdatalist != null && childdatalist.Count() > 0)
            {
                systemMenuDto.Child = new List<SystemMenuDto>();

                foreach (var item in childdatalist)
                {

                    SystemMenuDto treeNode = new SystemMenuDto()
                    {
                        Id = item.Id,
                        icon = item.icon,
                        href = item.href,
                        title = item.title,
                        target = item.target

                    };

                    systemMenuDto.Child.Add(treeNode);
                }
                foreach (var item in systemMenuDto.Child)
                {
                    GetSystemMenuDto(listsystemMenus, item);
                }


            }




            return systemMenuDto;

        }

    }
}