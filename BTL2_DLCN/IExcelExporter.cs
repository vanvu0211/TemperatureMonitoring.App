using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BTL2_DLCN
{
    public interface IExcelExporter
    {
        void ExportReport(string filePath, IEnumerable<ReportData> filters);
    }
}
