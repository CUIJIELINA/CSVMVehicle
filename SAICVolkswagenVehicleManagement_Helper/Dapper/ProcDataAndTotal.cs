using System;
using System.Collections.Generic;
using System.Text;

namespace SAICVolkswagenVehicleManagement_Helper.Dapper
{
    public class ProcDataAndTotal<T> where T : class, new()
    {
        //分页显示的集合
        public List<T> ProcData { get; set; }
        //总条数
        public int Total { get; set; }
    }
}
