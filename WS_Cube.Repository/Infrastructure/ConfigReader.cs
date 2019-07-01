using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Nancy.Json;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;


namespace WS_Cube.Repository.Infrastructure
{
    public class ScriptEngine : IScriptEngine
    {
        private readonly IHostingEnvironment environment;
        public static Dictionary<string, object> scriptengine_Rdata;
        public string finalquery;

        public ScriptEngine(
          IHostingEnvironment environment)
        {
            this.environment = environment;
        }

        private static string GetConnectionString()
        {
            return "Data Source=DESKTOP-8PPKK3C; Integrated Security=true; Database=Wscube_DB;" +
             "Initial Catalog=Wscube_DB; ";
        }

        public string GetValue(string tmpname)
        {
            try
            {
                scriptengine_Rdata = null;
                if (scriptengine_Rdata == null)
                {
                    var filePath = Path.Combine(environment.ContentRootPath + @"\APPMOD\CONFIG\");                    
                    StreamReader sr = File.OpenText(filePath + "MASTERS1.1.config");
                    string strSettings = sr.ReadToEnd();
                    scriptengine_Rdata = JsonConvert.DeserializeObject<Dictionary<string, object>>(strSettings);
                    sr.Close();
                    Dictionary<string, object> tmpdata = new Dictionary<string, object>();
                    tmpdata = scriptengine_Rdata;
                    string str = tmpdata.FirstOrDefault(x => x.Key == tmpname).Value.ToString();
                    string[] queries = str.Split('-');
                    string[] finalqueries = new string[queries.Length];
                    for (var k = 0; k <= queries.Length - 1; k++)
                    {
                        var FilePathForSr1 = Path.Combine(filePath + queries[k] + ".config");
                        StreamReader sr1 = File.OpenText(FilePathForSr1);
                        finalqueries[k] = sr1.ReadToEnd();
                        sr.Close();
                    }
                    finalquery = string.Join(";", finalqueries).ToString();
                }
                return finalquery;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public string GetValue1(string tmpname)
        {
            scriptengine_Rdata = null;
            if (scriptengine_Rdata == null)
            {
                var FilePath = Path.Combine(environment.WebRootPath, "~/APPMOD/SQL/" + tmpname + ".config", "configFile");
                StreamReader sr = File.OpenText(FilePath);
                string strSettings = sr.ReadToEnd();
                finalquery = strSettings;
                sr.Close();
            }
            return finalquery;
        }

        public string GetValueForApp(string tmpname, string appver)
        {
            scriptengine_Rdata = null;
            if (scriptengine_Rdata == null)
            {
                var FilePath = Path.Combine(environment.WebRootPath, "~/APPMOD/CONFIG/MASTERS1.1.config", "configFile");
                StreamReader sr = File.OpenText(FilePath);
                string strSettings = sr.ReadToEnd();
                scriptengine_Rdata = (new Nancy.Json.JavaScriptSerializer()).Deserialize<Dictionary<string, object>>(strSettings);
                sr.Close();
                Dictionary<string, object> tmpdata = new Dictionary<string, object>();
                tmpdata = scriptengine_Rdata;
                string str = tmpdata.FirstOrDefault(x => x.Key == tmpname).Value.ToString();
                string[] queries = str.Split('-');
                string[] finalqueries = new string[queries.Length];
                for (var k = 0; k <= queries.Length - 1; k++)
                {
                    string aa = "~/APPMOD/CONFIG/" + queries[k] + ".config";
                    var FilePathForSr1 = Path.Combine(environment.WebRootPath, aa, "configFile");
                    StreamReader sr1 = File.OpenText(FilePathForSr1);
                    finalqueries[k] = sr1.ReadToEnd();
                    sr.Close();
                }
                finalquery = string.Join(";", finalqueries).ToString();
            }
            return finalquery;
        }

        public List<Dictionary<string, object>> GetTableRows(DataTable dtData)
        {
            List<Dictionary<string, object>>
            lstRows = new List<Dictionary<string, object>>();
            Dictionary<string, object> dictRow = null;
            foreach (DataRow dr in dtData.Rows)
            {
                dictRow = new Dictionary<string, object>();
                foreach (DataColumn col in dtData.Columns)
                {
                    dictRow.Add(col.ColumnName, dr[col]);
                }
                lstRows.Add(dictRow);
            }
            return lstRows;
        }

        public string DataTableToJSONWithStringBuilder(DataTable table)
        {
            var JSONString = new StringBuilder();
            if (table.Rows.Count > 0)
            {
                JSONString.Append("{");
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    JSONString.Append("{");
                    for (int j = 0; j < table.Columns.Count; j++)
                    {
                        if (j < table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\",");
                        }
                        else if (j == table.Columns.Count - 1)
                        {
                            JSONString.Append("\"" + table.Columns[j].ColumnName.ToString() + "\":" + "\"" + table.Rows[i][j].ToString() + "\"");
                        }
                    }
                    if (i == table.Rows.Count - 1)
                    {
                        JSONString.Append("}");
                    }
                    else
                    {
                        JSONString.Append("},");
                    }
                }
                JSONString.Append("}");
            }
            return JSONString.ToString();
        }
    }
}
