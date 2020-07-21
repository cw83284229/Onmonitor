using System;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.HSSF.UserModel;
using System.IO;
using System.Data;
using System.Collections.Generic;
using NPOI.SS.Formula.Functions;
using System.Reflection;
using System.Globalization;
using System.Linq;
using Microsoft.AspNetCore.Http;

namespace OnMonitor.Common.Excel
{
    public class ExcelHelper
    {

        #region DataTable转Execl
        /// <summary>
        /// 将DataTable数据导入到excel中
        /// </summary>
        /// <param name="data">要导入的数据</param>
        /// <param name="fileName">写入的目标Excel的完整名称</param>
        /// <param name="isColumnWritten">DataTable的列名是否要导入</param>
        /// <param name="sheetName">要导入的excel的sheet的名称</param>
        /// <returns>导入数据行数(包含列名那一行)</returns>
        public static int DataTableToExcel(DataTable data, string fileName, string sheetName, bool isColumnWritten=true)
        {
            int i = 0;
            int j = 0;
            int count = 0;
            ISheet sheet = null;
            IWorkbook workbook = null;


            if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                workbook = new XSSFWorkbook();
            else if (fileName.IndexOf(".xls") > 0) // 2003版本
                workbook = new HSSFWorkbook();

            try
            {
                using (FileStream fs = new FileStream(fileName, FileMode.OpenOrCreate, FileAccess.ReadWrite))
                {
                    if (workbook != null)
                    {
                        sheet = workbook.CreateSheet(sheetName);
                    }
                    else
                    {
                        return -1;
                    }

                    if (isColumnWritten == true) //写入DataTable的列名
                    {
                        IRow row = sheet.CreateRow(0);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Columns[j].ColumnName);
                        }
                        count = 1;
                    }
                    else
                    {
                        count = 0;
                    }

                    for (i = 0; i < data.Rows.Count; ++i)
                    {
                        IRow row = sheet.CreateRow(count);
                        for (j = 0; j < data.Columns.Count; ++j)
                        {
                            row.CreateCell(j).SetCellValue(data.Rows[i][j].ToString());
                        }
                        ++count;
                    }
                    workbook.Write(fs); //写入到excel
                    return count;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return -1;
            }

        }
        #endregion

        #region List转Excel
        /// <summary>
        /// list转Excel
        /// </summary>
        /// <typeparam name="T">传入list 类型 </typeparam>
        /// <param name="SheetName">sheet名称</param>
        /// <param name="list">list集合</param>
        /// <param name="FiedNames">名称序列</param>
        /// <returns></returns>
        public static HSSFWorkbook ListtoExcel<T>(string SheetName, List<T> list, Dictionary<string, string> FiedNames)
        {
            HSSFWorkbook wb = new HSSFWorkbook();
            HSSFSheet sheet = (HSSFSheet)wb.CreateSheet(SheetName); //创建工作表
            sheet.CreateFreezePane(0, 1); //冻结列头行
            HSSFRow row_Title = (HSSFRow)sheet.CreateRow(0); //创建列头行
            row_Title.HeightInPoints = 30.5F; //设置列头行高
            HSSFCellStyle cs_Title = (HSSFCellStyle)wb.CreateCellStyle(); //创建列头样式
            cs_Title.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center; //水平居中
            cs_Title.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center; //垂直居中
            HSSFFont cs_Title_Font = (HSSFFont)wb.CreateFont(); //创建字体
            cs_Title_Font.IsBold = true; //字体加粗
            cs_Title_Font.FontHeightInPoints = 14; //字体大小
            cs_Title.SetFont(cs_Title_Font); //将字体绑定到样式
            #region 生成列头
            int ii = 0;
            foreach (string key in FiedNames.Keys)
            {
                HSSFCell cell_Title = (HSSFCell)row_Title.CreateCell(ii); //创建单元格
                cell_Title.CellStyle = cs_Title; //将样式绑定到单元格
                cell_Title.SetCellValue(key);
                sheet.SetColumnWidth(ii, 25 * 256);//设置列宽
                ii++;
            }

            #endregion
            //获取 实体类 类型对象
            Type t = typeof(T); // model.GetType();
            //获取 实体类 所有的 公有属性
            List<PropertyInfo> proInfos = t.GetProperties(BindingFlags.Instance | BindingFlags.Public).ToList();
            //创建 实体属性 字典集合
            Dictionary<string, PropertyInfo> dictPros = new Dictionary<string, PropertyInfo>();
            //将 实体属性 中要修改的属性名 添加到 字典集合中 键：属性名  值：属性对象
            proInfos.ForEach(p =>
            {
                if (FiedNames.Values.Contains(p.Name))
                {
                    dictPros.Add(p.Name, p);
                }
            });

            HSSFCellStyle cs_Content = (HSSFCellStyle)wb.CreateCellStyle(); //创建列头样式
            cs_Content.Alignment = NPOI.SS.UserModel.HorizontalAlignment.Center; //水平居中
            cs_Content.VerticalAlignment = NPOI.SS.UserModel.VerticalAlignment.Center; //垂直居中
            for (int i = 0; i < list.Count; i++)
            {

                HSSFRow row_Content = (HSSFRow)sheet.CreateRow(i + 1); //创建行
                row_Content.HeightInPoints = 20;
                int jj = 0;
                foreach (string proName in FiedNames.Values)
                {
                    if (dictPros.ContainsKey(proName))
                    {
                        HSSFCell cell_Conent = (HSSFCell)row_Content.CreateCell(jj); //创建单元格
                        cell_Conent.CellStyle = cs_Content;

                        //如果存在，则取出要属性对象
                        PropertyInfo proInfo = dictPros[proName];
                        //获取对应属性的值
                        object value = proInfo.GetValue(list[i], null); //object newValue = model.uName;
                        string cell_value = value == null ? "" : value.ToString();
                        cell_Conent.SetCellValue(cell_value);
                        jj++;
                    }
                }
            }
            return wb;

        } 
        #endregion

        #region Excel转Datatable
        /// <summary>
        /// 将excel中的数据导入到DataTable中
        /// </summary>
        /// <param name="fileName">读取的Excel的完整名称</param>
        /// <param name="sheetName">excel工作薄sheet的名称</param>
        /// <param name="isFirstRowColumn">第一行是否是DataTable的列名</param>
        /// <returns>返回的DataTable</returns>
        public static DataTable ExcelToDataTable(string fileName, string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                IWorkbook workbook = null;
                using (FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read))
                {
                    if (fileName.IndexOf(".xlsx") > 0) // 2007版本
                        workbook = new XSSFWorkbook(fs);
                    else if (fileName.IndexOf(".xls") > 0) // 2003版本
                        workbook = new HSSFWorkbook(fs);

                    if (sheetName != null)
                    {
                        sheet = workbook.GetSheet(sheetName);
                        if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                        {
                            sheet = workbook.GetSheetAt(0);
                        }
                    }
                    else
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                    if (sheet != null)
                    {
                        IRow firstRow = sheet.GetRow(0);
                        int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                        if (isFirstRowColumn)
                        {
                            for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                            {
                                ICell cell = firstRow.GetCell(i);
                                if (cell != null)
                                {
                                    string cellValue = cell.StringCellValue;
                                    if (cellValue != null)
                                    {
                                        DataColumn column = new DataColumn(cellValue);
                                        data.Columns.Add(column);
                                    }
                                }
                            }
                            startRow = sheet.FirstRowNum + 1;
                        }
                        else
                        {
                            startRow = sheet.FirstRowNum;
                        }

                        //最后一列的标号
                        int rowCount = sheet.LastRowNum;
                        for (int i = startRow; i <= rowCount; ++i)
                        {
                            IRow row = sheet.GetRow(i);
                            if (row == null) continue; //没有数据的行默认是null　　　　　　　

                            DataRow dataRow = data.NewRow();
                            for (int j = row.FirstCellNum; j < cellCount; ++j)
                            {
                                if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                    dataRow[j] = row.GetCell(j).ToString();
                            }
                            data.Rows.Add(dataRow);
                        }
                    }

                    return data;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
        #endregion

        #region Excel转Stream
        /// <summary>
        /// 把Excel的stream导入到表
        /// </summary>
        /// <param name="fs"></param>
        /// <param name="fileName"></param>
        /// <param name="sheetName"></param>
        /// <param name="isFirstRowColumn"></param>
        /// <returns></returns>
        public static DataTable ExcelToDataTable(Stream fs, string fileName, string sheetName, bool isFirstRowColumn)
        {
            ISheet sheet = null;
            DataTable data = new DataTable();
            int startRow = 0;
            try
            {
                IWorkbook workbook = null;
                if (fileName == ".xlsx") // 2007版本
                    workbook = new XSSFWorkbook(fs);
                else if (fileName == ".xls") // 2003版本
                    workbook = new HSSFWorkbook(fs);

                if (sheetName != null)
                {
                    sheet = workbook.GetSheet(sheetName);
                    if (sheet == null) //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    sheet = workbook.GetSheetAt(0);
                }
                if (sheet != null)
                {
                    IRow firstRow = sheet.GetRow(0);
                    int cellCount = firstRow.LastCellNum; //一行最后一个cell的编号 即总的列数

                    if (isFirstRowColumn)
                    {
                        for (int i = firstRow.FirstCellNum; i < cellCount; ++i)
                        {
                            ICell cell = firstRow.GetCell(i);
                            if (cell != null)
                            {
                                string cellValue = cell.StringCellValue;
                                if (cellValue != null)
                                {
                                    DataColumn column = new DataColumn(cellValue);
                                    data.Columns.Add(column);
                                }
                            }
                        }
                        startRow = sheet.FirstRowNum + 1;
                    }
                    else
                    {
                        startRow = sheet.FirstRowNum;
                    }

                    //最后一列的标号
                    int rowCount = sheet.LastRowNum;
                    for (int i = startRow; i <= rowCount; ++i)
                    {
                        IRow row = sheet.GetRow(i);
                        if (row == null) continue; //没有数据的行默认是null　　　　　　　

                        DataRow dataRow = data.NewRow();
                        for (int j = row.FirstCellNum; j < cellCount; ++j)
                        {
                            if (row.GetCell(j) != null) //同理，没有数据的单元格都默认是null
                                dataRow[j] = row.GetCell(j).ToString();
                        }
                        data.Rows.Add(dataRow);
                    }
                }

                return data;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Exception: " + ex.Message);
                return null;
            }
        }
        #endregion


        #region List转Byte
        /// <summary>
        /// 使用实体数据导出
        /// </summary>
        /// <param name="entitys"></param>
        /// <param name="title"></param>
        /// <returns></returns>
        public static byte[] OutputExcel(List<T> entitys, string[] title)
        {
            IWorkbook workbook = new XSSFWorkbook();
            ISheet sheet = workbook.CreateSheet("sheet");
            IRow Title = null;
            IRow rows = null;
            Type entityType = entitys[0].GetType();
            PropertyInfo[] entityProperties = entityType.GetProperties();

            for (int i = 0; i <= entitys.Count; i++)
            {
                if (i == 0)
                {
                    Title = sheet.CreateRow(0);
                    for (int k = 1; k < title.Length + 1; k++)
                    {
                        Title.CreateCell(0).SetCellValue("序号");
                        Title.CreateCell(k).SetCellValue(title[k - 1]);
                    }

                    continue;
                }
                else
                {
                    rows = sheet.CreateRow(i);
                    object entity = entitys[i - 1];
                    for (int j = 1; j <= entityProperties.Length; j++)
                    {
                        object[] entityValues = new object[entityProperties.Length];
                        entityValues[j - 1] = entityProperties[j - 1].GetValue(entity);
                        rows.CreateCell(0).SetCellValue(i);
                        rows.CreateCell(j).SetCellValue(entityValues[j - 1].ToString());
                    }
                }
            }

            byte[] buffer = new byte[1024 * 2];
            using (MemoryStream ms = new MemoryStream())
            {
                workbook.Write(ms);
                buffer = ms.GetBuffer();
                ms.Close();
            }

            return buffer;
        }
        #endregion

    }
        /// <summary>
        /// Excel导入帮助类
        /// </summary>
        public class ImportExcelUtil<T> where T : new()
        {
            //合法文件扩展名
            private static List<string> extName = new List<string>() { ".xls", ".xlsx" };
        /// <summary>
        /// 导入Excel内容读取到List<T>中
        /// </summary>
        /// <param name="file"></param>
        /// <param name="sheetName"></param>
        /// <returns></returns>
        public static List<T> InputExcel(IFormFile file, string sheetName = null)
            {
                //获取文件后缀名
                string type = Path.GetExtension(file.FileName);
                //判断是否导入合法文件
                if (!extName.Contains(type))
                {
                    return null;
                }
                //转成为文件流
                MemoryStream ms = new MemoryStream();
                file.CopyTo(ms);
                ms.Seek(0, SeekOrigin.Begin);
                //实例化T数组
                List<T> list = new List<T>();
                //获取数据
                list = InputExcel(ms, sheetName);
                return list;
            }

            /// <summary>
            /// 将Excel文件内容读取到List<T>中
            /// </summary>
            /// <param name="fileName">文件完整路径名</param>
            /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
            /// <param name="isFirstRowColumn">第一行是否是DataTable的列名：true=是，false=否</param>
           
         public static List<T> InputExcel(string fileName, string sheetName = null)
            {
                if (!File.Exists(fileName))
                {
                    return null;
                }
                //根据指定路径读取文件
                FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
                //实例化T数组
                List<T> list = new List<T>();
                //获取数据
                list = InputExcel(fs, sheetName);

                return list;
            }

            /// <summary>
            /// 将Excel文件内容读取到List<T>中
            /// </summary>
            /// <param name="fileStream">文件流</param>
            /// <param name="sheetName">指定读取excel工作薄sheet的名称</param>
           
            private static List<T> InputExcel(Stream fileStream, string sheetName = null)
            {
                //创建Excel数据结构
                IWorkbook workbook = WorkbookFactory.Create(fileStream);
                //如果有指定工作表名称
                ISheet sheet = null;
                if (!string.IsNullOrEmpty(sheetName))
                {
                    sheet = workbook.GetSheet(sheetName);
                    //如果没有找到指定的sheetName对应的sheet，则尝试获取第一个sheet
                    if (sheet == null)
                    {
                        sheet = workbook.GetSheetAt(0);
                    }
                }
                else
                {
                    //如果没有指定的sheetName，则尝试获取第一个sheet
                    sheet = workbook.GetSheetAt(0);
                }
                //实例化T数组
                List<T> list = new List<T>();
                if (sheet != null)
                {
                    //一行最后一个cell的编号 即总的列数
                    IRow cellNum = sheet.GetRow(0);
                    int num = cellNum.LastCellNum;
                    //获取泛型对象T的所有属性
                    var propertys = typeof(T).GetProperties();
                    //每行转换为单个T对象
                    for (int i = 1; i <= sheet.LastRowNum; i++)
                    {
                        IRow row = sheet.GetRow(i);
                        var obj = new T();
                        for (int j = 0; j < num; j++)
                        {
                            //没有数据的单元格都默认是null
                            ICell cell = row.GetCell(j);
                            if (cell != null)
                            {
                                var value = row.GetCell(j).ToString();
                                string str = (propertys[j].PropertyType).FullName;
                                if (str == "System.String")
                                {
                                    propertys[j].SetValue(obj, value, null);
                                }
                                else if (str == "System.DateTime")
                                {
                                    DateTime pdt = Convert.ToDateTime(value, CultureInfo.InvariantCulture);
                                    propertys[j].SetValue(obj, pdt, null);
                                }
                                else if (str == "System.Boolean")
                                {
                                    bool pb = Convert.ToBoolean(value);
                                    propertys[j].SetValue(obj, pb, null);
                                }
                                else if (str == "System.Int16")
                                {
                                    short pi16 = Convert.ToInt16(value);
                                    propertys[j].SetValue(obj, pi16, null);
                                }
                                else if (str == "System.Int32")
                                {
                                    int pi32 = Convert.ToInt32(value);
                                    propertys[j].SetValue(obj, pi32, null);
                                }
                                else if (str == "System.Int64")
                                {
                                    long pi64 = Convert.ToInt64(value);
                                    propertys[j].SetValue(obj, pi64, null);
                                }
                                else if (str == "System.Byte")
                                {
                                    byte pb = Convert.ToByte(value);
                                    propertys[j].SetValue(obj, pb, null);
                                }
                                else
                                {
                                    propertys[j].SetValue(obj, null, null);
                                }
                            }
                        }
                        list.Add(obj);
                    }
                }
                return list;
            }

        }

    }
