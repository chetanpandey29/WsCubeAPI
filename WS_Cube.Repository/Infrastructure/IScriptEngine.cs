using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace WS_Cube.Repository.Infrastructure
{
    public interface IScriptEngine
    {
        string GetValue(string tmpname);

        string GetValue1(string tmpname);

        string GetValueForApp(string tmpname, string appver);

        List<Dictionary<string, object>> GetTableRows(DataTable dtData);

        string DataTableToJSONWithStringBuilder(DataTable table);
    }
}
