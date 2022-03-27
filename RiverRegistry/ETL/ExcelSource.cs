using OfficeOpenXml;
using RiverRegistry.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Threading.Tasks;

namespace RiverRegistry.ETL
{
    public class ExcelSource<TOutput>
    {
        private readonly Action<TOutput[]> Load;
        private readonly Func<DataRow, TOutput> Transform;
        private readonly Stream _file;
        
        public bool HasHeader { get; set; }

        public ExcelSource(Stream file, Func<DataRow, TOutput> transform, Action<TOutput[]> load)
        {
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;
            Transform = transform;
            Load = load;
            _file = file;
        }

        private async Task ProcessAsync(DataRowCollection rows)
        {
            TOutput[] items = new TOutput[rows.Count];


            await Task.Run(() => Parallel.For(0, rows.Count, (int row) =>
            {
                var transformed = Transform(rows[row]);
                items[row] = transformed;
            }));

            Load(items);
        }

        public async Task ExecuteAsync()
        {
            using var pck = new ExcelPackage(_file);

            ExcelWorksheet worksheet = pck.Workbook.Worksheets[0];
            ExcelRange cells = worksheet.Cells;

            int colCount = worksheet.Dimension.End.Column;
            int rowCount = worksheet.Dimension.End.Row;

            int startRow = HasHeader ? 1 : 0;

            var table = cells[startRow, 1, rowCount + 1, colCount].ToDataTable();

            await ProcessAsync(table.Rows);
        }
    }
}
