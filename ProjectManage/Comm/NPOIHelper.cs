using System;
using System.Data;
using System.Linq;
using System.Collections.Generic;
using System.IO;
using NPOI.HSSF.UserModel;
using NPOI.HSSF.Util;
using NPOI.SS.UserModel;
using System.Reflection;

namespace Comm {
    public class NPOIHelper<T> {
        /// <summary>
        /// 标题
        /// </summary>
        private string[] titles;
        private string[] colums;
        private List<T> data;
        private List<T> data2;
        private int[] columsWidth;
        private string[] columTitle;

        public NPOIHelper() { }

        public NPOIHelper(string[] titles, List<T> data) {
            this.titles = titles;
            this.data = data;
        }

        public NPOIHelper(string[] titles, string[] colums, List<T> data) {
            this.titles = titles;
            this.colums = colums;
            this.data = data;
        }

        public NPOIHelper(string[] titles, string[] colums, int[] columsWidth, List<T> data) {
            this.titles = titles;
            this.colums = colums;
            this.columsWidth = columsWidth;
            this.data = data;
        }

        public NPOIHelper(string[] titles, string[] colums, int[] columsWidth, List<T> data, List<T> data2) {
            this.titles = titles;
            this.colums = colums;
            this.columsWidth = columsWidth;
            this.data = data;
            this.data2 = data2;
        }

        public NPOIHelper(string[] titles, string[] colums, int[] columsWidth, string[] columTitle, List<T> data) {
            this.titles = titles;
            this.colums = colums;
            this.columsWidth = columsWidth;
            this.data = data;
            this.columTitle = columTitle;

        }

        public Stream ToExcel() {
            int rowIndex = 0;

            //创建workbook
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet("sheet1");
            IRow row = sheet.CreateRow(rowIndex);
            row.Height = 200 * 3;

            //表头样式
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Left;//居中对齐
            //表头单元格背景色
            style.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground;
            //表头单元格边框
            style.BorderBottom = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;
            style.TopBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.VerticalAlignment = VerticalAlignment.Center;
            //表头字体设置
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 12;//字号
            font.Boldweight = 600;//加粗
            //font.Color = HSSFColor.WHITE.index;//颜色
            style.SetFont(font);

            //数据样式
            ICellStyle datastyle = workbook.CreateCellStyle();
            datastyle.Alignment = HorizontalAlignment.Left;//左对齐
            //数据单元格的边框
            datastyle.BorderTop = BorderStyle.Thin;
            datastyle.BorderRight = BorderStyle.Thin;
            datastyle.BorderBottom = BorderStyle.Thin;
            datastyle.BorderLeft = BorderStyle.Thin;

            datastyle.TopBorderColor = HSSFColor.Black.Index;
            datastyle.RightBorderColor = HSSFColor.Black.Index;
            datastyle.BottomBorderColor = HSSFColor.Black.Index;
            datastyle.LeftBorderColor = HSSFColor.Black.Index;
            //数据的字体
            IFont datafont = workbook.CreateFont();
            datafont.FontHeightInPoints = 11;//字号
            datastyle.SetFont(datafont);
            //设置列宽
            sheet.SetColumnWidth(0, 20 * 256);
            int colWidth0 = sheet.GetColumnWidth(0);

            sheet.SetColumnWidth(1, 20 * 256);
            int colWidth1 = sheet.GetColumnWidth(1);

            sheet.SetColumnWidth(2, 20 * 256);
            int colWidth2 = sheet.GetColumnWidth(2);



            sheet.SetColumnWidth(9, 40 * 256);
            sheet.DisplayGridlines = false;

            try {
                //表头数据
                for (int i = 0; i < titles.Length; i++) {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(titles[i]);
                    cell.CellStyle = style;
                }


                for (int k = 0; k < data.Count; k++) {
                    row = sheet.CreateRow(k + 1);
                    row.Height = 200 * 2;
                    T t = data[k];
                    // 获得此模型的公共属性
                    PropertyInfo[] propertys = t.GetType().GetProperties();
                    for (int j = 0; j < propertys.Length; j++) {
                        var value = propertys[j].GetValue(null, null).ToString();
                        ICell cell = row.CreateCell(j);
                        cell.SetCellValue(value);
                        cell.CellStyle = datastyle;
                    }

                }


            } catch (Exception ex) {

            } finally {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook = null;
                sheet = null;
                row = null;
            }



            return ms;
        }

        public Stream CommonToExcel() {
            int rowIndex = 0;

            //创建workbook
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet("sheet1");
            IRow row = sheet.CreateRow(rowIndex);
            row.Height = 200 * 3;

            //表头样式
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Left;//居中对齐
            //表头单元格背景色
            style.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground; ;
            ////表头单元格边框
            style.BorderTop = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;

            style.TopBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.VerticalAlignment = VerticalAlignment.Center;
            //表头字体设置
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 12;//字号
            font.Boldweight = 600;//加粗
            //font.Color = HSSFColor.WHITE.index;//颜色
            style.SetFont(font);

            //数据样式
            ICellStyle datastyle = workbook.CreateCellStyle();
            datastyle.Alignment = HorizontalAlignment.Left;//左对齐
            //数据单元格的边框
            datastyle.BorderTop = BorderStyle.Thin;
            datastyle.BorderRight = BorderStyle.Thin;
            datastyle.BorderBottom = BorderStyle.Thin;
            datastyle.BorderLeft = BorderStyle.Thin;

            datastyle.TopBorderColor = HSSFColor.Black.Index;
            datastyle.RightBorderColor = HSSFColor.Black.Index;
            datastyle.BottomBorderColor = HSSFColor.Black.Index;
            datastyle.LeftBorderColor = HSSFColor.Black.Index;
            //数据的字体
            IFont datafont = workbook.CreateFont();
            datafont.FontHeightInPoints = 11;//字号
            datastyle.SetFont(datafont);
            //设置列宽
            for (int i = 0; i < columsWidth.Length; i++) {
                //sheet.SetColumnWidth(i, columsWidth[i] * 256);
                sheet.SetColumnWidth(i, columsWidth[i] * 58);
            }
            sheet.DisplayGridlines = false;

            try {
                //表头数据
                for (int i = 0; i < titles.Length; i++) {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(titles[i]);
                    cell.CellStyle = style;
                }
                for (int k = 0; k < data.Count; k++) {
                    row = sheet.CreateRow(k + 1);
                    row.Height = 200 * 2;
                    T t = data[k];
                    // 获得此模型的公共属性
                    PropertyInfo[] propertys = t.GetType().GetProperties();
                    for (int j = 0; j < colums.Length; j++) {
                        var temp = propertys.ToList().Where(p => p.Name == colums[j]).Single();
                        ICell cell = row.CreateCell(j);

                        var value = temp.GetValue(t, null);

                        if (value == null) {
                            value = "";
                        }
                        else {
                            if (value.GetType() == Type.GetType("System.DateTime")) {
                                value = DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd");
                            }
                        }
                        cell.SetCellValue(value.ToString());
                        cell.CellStyle = datastyle;
                    }
                }
            } catch (Exception ex) {
            } finally {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook = null;
                sheet = null;
                row = null;
            }
            return ms;
        }

        public Stream DiyCommonToExcel() {
            int rowIndex = 0;

            //创建workbook
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet1 = workbook.CreateSheet("接入交换机");

            ISheet sheet2 = workbook.CreateSheet("核心交换机");
            IRow row1 = sheet1.CreateRow(rowIndex);
            IRow row2 = sheet2.CreateRow(rowIndex);
            row1.Height = 200 * 3;
            row2.Height = 200 * 3;

            //表头样式
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Left;//居中对齐
            //表头单元格背景色
            style.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground; ;
            //表头单元格边框
            style.BorderTop = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;

            style.TopBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.VerticalAlignment = VerticalAlignment.Center;
            //表头字体设置
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 12;//字号
            font.Boldweight = 600;//加粗
            //font.Color = HSSFColor.WHITE.index;//颜色
            style.SetFont(font);

            //数据样式
            ICellStyle datastyle = workbook.CreateCellStyle();
            datastyle.Alignment = HorizontalAlignment.Left;//左对齐
            //数据单元格的边框
            datastyle.BorderTop = BorderStyle.Thin;
            datastyle.BorderRight = BorderStyle.Thin;
            datastyle.BorderBottom = BorderStyle.Thin;
            datastyle.BorderLeft = BorderStyle.Thin;

            datastyle.TopBorderColor = HSSFColor.Black.Index;
            datastyle.RightBorderColor = HSSFColor.Black.Index;
            datastyle.BottomBorderColor = HSSFColor.Black.Index;
            datastyle.LeftBorderColor = HSSFColor.Black.Index;
            //数据的字体
            IFont datafont = workbook.CreateFont();
            datafont.FontHeightInPoints = 11;//字号
            datastyle.SetFont(datafont);
            //设置列宽
            for (int i = 0; i < columsWidth.Length; i++) {
                //sheet.SetColumnWidth(i, columsWidth[i] * 256);
                sheet1.SetColumnWidth(i, columsWidth[i] * 58);
                sheet2.SetColumnWidth(i, columsWidth[i] * 58);
            }
            sheet1.DisplayGridlines = false;
            sheet2.DisplayGridlines = false;
            try {
                //表头数据
                for (int i = 0; i < titles.Length; i++) {
                    ICell cell1 = row1.CreateCell(i);
                    ICell cell2 = row2.CreateCell(i);
                    cell1.SetCellValue(titles[i]);
                    cell1.CellStyle = style;
                    cell2.SetCellValue(titles[i]);
                    cell2.CellStyle = style;
                }
                for (int k = 0; k < data.Count; k++) {
                    row1 = sheet1.CreateRow(k + 1);
                    row1.Height = 200 * 2;
                    T t = data[k];
                    // 获得此模型的公共属性
                    PropertyInfo[] propertys = t.GetType().GetProperties();
                    for (int j = 0; j < colums.Length; j++) {
                        var temp = propertys.ToList().Where(p => p.Name == colums[j]).Single();
                        ICell cell1 = row1.CreateCell(j);

                        var value = temp.GetValue(t, null);

                        if (value == null) {
                            value = "";
                        }
                        else {
                            if (value.GetType() == Type.GetType("System.DateTime")) {
                                value = DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd");
                            }
                        }
                        cell1.SetCellValue(value.ToString());
                        cell1.CellStyle = datastyle;
                    }
                }

                for (int k = 0; k < data2.Count; k++) {
                    row2 = sheet2.CreateRow(k + 1);
                    row2.Height = 200 * 2;
                    T t = data2[k];
                    // 获得此模型的公共属性
                    PropertyInfo[] propertys = t.GetType().GetProperties();
                    for (int j = 0; j < colums.Length; j++) {
                        var temp = propertys.ToList().Where(p => p.Name == colums[j]).Single();
                        ICell cell2 = row2.CreateCell(j);

                        var value = temp.GetValue(t, null);

                        if (value == null) {
                            value = "";
                        }
                        else {
                            if (value.GetType() == Type.GetType("System.DateTime")) {
                                value = DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd");
                            }
                        }
                        cell2.SetCellValue(value.ToString());
                        cell2.CellStyle = datastyle;
                    }
                }
            } catch (Exception ex) {
            } finally {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook = null;
                sheet1 = null;
                row1 = null;
                sheet2 = null;
                row2 = null;
            }
            return ms;
        }


        /// <summary>
        /// 导出数据到excel文件
        /// </summary>
        /// <param name="titleList">特殊处理的数据的标题集合</param>
        /// <returns></returns>
        public Stream CommonToExcel(List<string> titleList) {
            int rowIndex = 0;

            //创建workbook
            HSSFWorkbook workbook = new HSSFWorkbook();
            MemoryStream ms = new MemoryStream();
            ISheet sheet = workbook.CreateSheet("sheet1");
            IRow row = sheet.CreateRow(rowIndex);
            row.Height = 200 * 3;

            //表头样式
            ICellStyle style = workbook.CreateCellStyle();
            style.Alignment = HorizontalAlignment.Left;//居中对齐
            //表头单元格背景色
            style.FillForegroundColor = HSSFColor.Grey25Percent.Index;
            style.FillPattern = FillPattern.SolidForeground;
            //表头单元格边框
            style.BorderTop = BorderStyle.Thin;
            style.BorderRight = BorderStyle.Thin;
            style.BorderBottom = BorderStyle.Thin;
            style.BorderLeft = BorderStyle.Thin;

            style.TopBorderColor = HSSFColor.Black.Index;
            style.RightBorderColor = HSSFColor.Black.Index;
            style.BottomBorderColor = HSSFColor.Black.Index;
            style.LeftBorderColor = HSSFColor.Black.Index;
            style.VerticalAlignment = VerticalAlignment.Center;
            //表头字体设置
            IFont font = workbook.CreateFont();
            font.FontHeightInPoints = 12;//字号
            font.Boldweight = 600;//加粗
            //font.Color = HSSFColor.WHITE.index;//颜色
            style.SetFont(font);

            //数据样式
            ICellStyle datastyle = workbook.CreateCellStyle();
            datastyle.Alignment = HorizontalAlignment.Left;//左对齐
            //数据单元格的边框
            datastyle.BorderTop = BorderStyle.Thin;
            datastyle.BorderRight = BorderStyle.Thin;
            datastyle.BorderBottom = BorderStyle.Thin;
            datastyle.BorderLeft = BorderStyle.Thin;

            datastyle.TopBorderColor = HSSFColor.Black.Index;
            datastyle.RightBorderColor = HSSFColor.Black.Index;
            datastyle.BottomBorderColor = HSSFColor.Black.Index;
            datastyle.LeftBorderColor = HSSFColor.Black.Index;
            //数据的字体
            IFont datafont = workbook.CreateFont();
            datafont.FontHeightInPoints = 11;//字号
            datastyle.SetFont(datafont);
            //设置列宽
            for (int i = 0; i < columsWidth.Length; i++) {
                //sheet.SetColumnWidth(i, columsWidth[i] * 256);
                sheet.SetColumnWidth(i, columsWidth[i] * 58);
            }
            sheet.DisplayGridlines = false;

            try {
                //表头数据
                for (int i = 0; i < titles.Length; i++) {
                    ICell cell = row.CreateCell(i);
                    cell.SetCellValue(titles[i]);
                    cell.CellStyle = style;
                }

                for (int k = 0; k < data.Count; k++) {
                    row = sheet.CreateRow(k + 1);
                    row.Height = 200 * 2;
                    T t = data[k];
                    // 获得此模型的公共属性
                    PropertyInfo[] propertys = t.GetType().GetProperties();
                    for (int j = 0; j < colums.Length; j++) {
                        var temp = propertys.ToList().Where(p => p.Name == colums[j]).Single();
                        ICell cell = row.CreateCell(j);

                        var value = temp.GetValue(t, null);

                        if (value == null) {
                            value = "";
                        }
                        else {

                            if (value.GetType() == Type.GetType("System.DateTime")) {
                                if (titleList != null && titleList.Contains(colums[j])) {
                                    value = DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd HH:mm");
                                }
                                else {
                                    value = DateTime.Parse(value.ToString()).ToString("yyyy-MM-dd");
                                }
                            }
                        }
                        cell.SetCellValue(value.ToString());
                        cell.CellStyle = datastyle;
                    }
                }


            } catch (Exception ex) {
            } finally {
                workbook.Write(ms);
                ms.Flush();
                ms.Position = 0;
                workbook = null;
                sheet = null;
                row = null;
            }



            return ms;
        }

        public List<object> GetExcel(Stream s, string sheetName) {
            IWorkbook hw = WorkbookFactory.Create(s);
            try {
                bool existSheetName = false;
                for (int i = 0; i < hw.NumberOfSheets; i++) {
                    if (sheetName == hw.GetSheetName(i)) {
                        existSheetName = true;
                        break;
                    }
                }
                if (!existSheetName) { //如果sheet名称不存在
                    throw new Exception("导入的Excel不包含输入的工作簿！");
                }
                ISheet hs = hw.GetSheet(sheetName);

                int rowLength = hs.LastRowNum;

                List<object> list = new List<object>();

                List<string> titleList = new List<string>();

                int cellLength = hs.GetRow(0).LastCellNum;
                for (int z = 0; z < cellLength; z++) {
                    titleList.Add(hs.GetRow(0).GetCell(z).StringCellValue.Trim());
                }

                for (int i = 1; i <= rowLength; i++) {
                    Dictionary<string, string> rows = new Dictionary<string, string>();
                    for (int j = 0; j < cellLength; j++) {
                        if (hs.GetRow(i).GetCell(j) != null) {
                            rows.Add(titleList[j], hs.GetRow(i).GetCell(j).ToString());
                        }
                        else {
                            rows.Add(titleList[j], "");
                        }
                    }
                    list.Add(rows);
                }
                return list;
            } catch (Exception ex) {

                throw ex;
            } finally {
                hw.Close();
                s.Close();
            }

           
        }


        public DataTable GetExcel(Stream s) {
            var wk = new HSSFWorkbook(s);
            var dt = new DataTable();
            for (var i = 0; i < wk.NumberOfSheets; i++) //NumberOfSheets是xls文件中总共的表数
            {
                var sheet = wk.GetSheetAt(i); //读取当前表数据
                System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
                for (int j = 0; j < sheet.LastRowNum; j++) {
                    dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
                }

                while (rows.MoveNext()) {
                    HSSFRow row = (HSSFRow)rows.Current;
                    DataRow dr = dt.NewRow();

                    for (int k = 0; k < row.LastCellNum; k++) {
                        ICell cell = row.GetCell(k);


                        if (cell == null) {
                            dr[k] = null;
                        }
                        else {
                            dr[k] = cell.ToString();
                        }
                    }
                    dt.Rows.Add(dr);
                }
            }
            return dt;
        }
    }
}

