using Magicodes.ExporterAndImporter.Core;
using Magicodes.ExporterAndImporter.Core.Extension;
using Magicodes.ExporterAndImporter.Excel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace OnMonitor.Excel
{
    public static class ListToDataTable
    {


        /// <summary>
        /// 傳入list轉換為DataTable
        /// </summary>
        /// <typeparam name="T">泛型類別</typeparam>
        /// <param name="data">數據源</param>
        /// <returns></returns>
        public static DataTable toDataTable<T>(List<T> data)
        {
            PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(T));
            DataTable dt = new DataTable();
            for (int i = 0; i < properties.Count; i++)
            {
                PropertyDescriptor property = properties[i];
                dt.Columns.Add(property.Name);
            }
            Object[] value = new object[properties.Count];
            foreach (T item in data)
            {
                for (int i = 0; i < value.Length; i++)
                {
                    value[i] = properties[i].GetValue(item);
                }
                dt.Rows.Add(value);
            }
            return dt;

        }
        /// <summary>
        /// DataTable 轉換為list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static List<T> tolist<T>(DataTable data) where T : class, new()
        {
            Type type = typeof(T);
            List<T> list = new List<T>();
            String tempName = "";

            foreach (DataRow row in data.Rows)
            {
                PropertyInfo[] pArray = type.GetProperties();
                T entity = new T();

                foreach (PropertyInfo p in pArray)
                {
                    tempName = p.Name;
                    if (data.Columns.Contains(tempName))
                    {
                        Type type1 = p.PropertyType;

                        if (type1.IsGenericType&&type1.GetGenericTypeDefinition().Equals(typeof(Nullable<>)))
                        {
                            System.ComponentModel.NullableConverter nullableConverter = new NullableConverter(type1);
                            type1 = nullableConverter.UnderlyingType;
                        }

                        if (!p.CanWrite) continue;
                        object value = row[tempName];
                        if (value != DBNull.Value&& (String)value!="")
                        {
                            

                            p.SetValue(entity,Convert.ChangeType(value,type1), null);

                        }
                        else
                        {
                            p.SetValue(entity, null, null);
                        }
                    }
                }
                list.Add(entity);

            }

            return list;
        }

        /// <summary>
        /// 导出文件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="FileName"></param>
        public static void MagicodesIEOutExcel<T>(List<T> list,string FileName) where T : class
        {

            IExporter exporter = new ExcelExporter();

            var requst = exporter.ExportAsByteArray(list);

           var DD= requst.Result.ToExcelExportFileInfo(FileName);
        
        }
      

    }
}
