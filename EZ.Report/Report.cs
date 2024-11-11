using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EZ.Report
{
    [Serializable]
    class Report
    {
        public string Name { get; set; } = "Название";

        public string Path { get; set; } = "Путь сохранения";

        public List<int> Key { get; set; } = new List<int>();
}
}
