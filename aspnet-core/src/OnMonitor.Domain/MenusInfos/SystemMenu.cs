using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Domain.Entities;

namespace OnMonitor.MenusInfos
{
  public  class SystemMenu: Entity<long>
    {
        /// <summary>
        /// 父级ID
        /// </summary>
       
        public long pid { get; set; }
        /// <summary>
        /// 名称
        /// </summary>
        
        public string title { get; set; }

        /// <summary>
        /// 菜单图标
        /// </summary>
        public string icon { get; set; }

        /// <summary>
        /// 链接
        /// </summary>
        public string href { get; set; }

        /// <summary>
        /// 新Tab打开方式
        /// </summary>
        public string target { get; set; }


        /// <summary>
        /// 排序编号
        /// </summary>
        public int sort { get; set; }

        /// <summary>
        /// 是否菜单
        /// </summary>
        public bool status { get; set; }



      

        protected SystemMenu()
        {
        }

        public SystemMenu(
            long id,
            long pid,
            string title,
            string icon,
            string href,
            string target,
            int sort,
            bool status
        ) :base(id)
        {
            pid = pid;
            title = title;
            icon = icon;
            href = href;
            target = target;
            sort = sort;
            status = status;
        }
    }
}
