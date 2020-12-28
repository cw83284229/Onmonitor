using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using System.Threading.Tasks;

namespace EquipmentStatus.Models
{
  public  class EFDBHelp<T> where T : class
    {

        EFContext context=new EFContext();

        /// <summary>
        /// 新增一个实体
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public int Add(T entity)
            {
                context.Entry<T>(entity).State = EntityState.Added;
                return context.SaveChanges();
            }
            /// <summary>
            /// 删除一个实体
            /// </summary>
            /// <param name="entity"></param>
            /// <returns></returns>
            public int Remove(T entity)
            {
                context.Entry<T>(entity).State = EntityState.Deleted;
                return context.SaveChanges();
            }
            /// <summary>
            /// 修改一个实体
            /// </summary>
            /// <param name="entity"></param>
            /// <returns></returns>
            public int Update(T entity)
            {
                context.Entry<T>(entity).State = EntityState.Modified;
                return context.SaveChanges();
            }
            /// <summary>
            /// 批量新增实体
            /// </summary>
            /// <param name="dbContext"></param>
            /// <returns></returns>
            public int AddList(params T[] entities)
            {
                int result = 0;
                for (int i = 0; i < entities.Count(); i++)
                {
                    if (entities[i] == null)
                        continue;
                    context.Entry<T>(entities[i]).State = EntityState.Added;
                    if (i != 0 && i % 20 == 0)
                    {
                        result += context.SaveChanges();
                    }
                }
                if (entities.Count() > 0)
                    result += context.SaveChanges();
                return result;
            }
            /// <summary>
            /// 批量删除实体
            /// </summary>
            /// <param name="where"></param>
            /// <returns></returns>
            public int RemoveList(Expression<Func<T, bool>> where)
            {
                var temp = context.Set<T>().Where(where);
                foreach (var item in temp)
                {
                    context.Entry<T>(item).State = EntityState.Deleted;
                }
                return context.SaveChanges();
            }
            /// <summary>
            /// 按条件查询
            /// </summary>
            /// <param name="where"></param>
            /// <returns></returns>
            public IQueryable<T> FindList(Expression<Func<T, bool>> where)
            {
                var temp = context.Set<T>().Where(where);
                return temp;
            }
            /// <summary>
            /// 按条件查询，排序
            /// </summary>
            /// <typeparam name="S"><peparam>
            /// <param name="where"></param>
            /// <param name="orderBy"></param>
            /// <param name="isAsc"></param>
            /// <returns></returns>
            public IQueryable<T> FindList<S>(Expression<Func<T, bool>> where, Expression<Func<T, S>> orderBy, bool isAsc)
            {

                var list = context.Set<T>().Where(where);
                if (isAsc)
                    list = list.OrderBy<T, S>(orderBy);
                else
                    list = list.OrderByDescending<T, S>(orderBy);
                return list;
            }
            /// <summary>
            /// 按条件查询，分页
            /// </summary>
            /// <param name="pageIndex"></param>
            /// <param name="pageSize"></param>
            /// <param name="rowCount"></param>
            /// <param name="where"></param>
            /// <returns></returns>
            public IQueryable<T> FindPagedList(int pageIndex, int pageSize, out int rowCount, Expression<Func<T, bool>> where)
            {
                var list = context.Set<T>().Where(where);
                rowCount = list.Count();
                list = list.Skip(pageSize * (pageIndex - 1)).Take(pageSize);
                return list;
            }
            /// <summary>
            /// 按条件查询，分页，排序
            /// </summary>
            /// <typeparam name="S"><peparam>
            /// <param name="pageIndex"></param>
            /// <param name="pageSize"></param>
            /// <param name="rowCount"></param>
            /// <param name="where"></param>
            /// <param name="orderBy"></param>
            /// <param name="isAsc"></param>
            /// <returns></returns>
            public IQueryable<T> FindPagedList<S>(int pageIndex, int pageSize, out int rowCount, Expression<Func<T, bool>> where, Expression<Func<T, S>> orderBy, bool isAsc)
            {
                var list = context.Set<T>().Where(where);
                rowCount = list.Count();
                if (isAsc)
                    list = list.OrderBy<T, S>(orderBy).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
                else
                    list = list.OrderByDescending<T, S>(orderBy).Skip(pageSize * (pageIndex - 1)).Take(pageSize);
                return list;
            }
        }
    }

