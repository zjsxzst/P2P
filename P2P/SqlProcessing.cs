using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P2P
{
    public  class SqlProcessing<T> where T :new()
    {
        public static string Flie_Path = System.IO.Directory.GetCurrentDirectory() + "\\Data\\DATA.accdb";//当前路径
        public static string PassWD = null;//获取数据库密码
        OleDbConnection con;
        DataTable dt;
        public SqlProcessing()
        {
            String connStr = String.Format("Provider=Microsoft.ACE.OLEDB.12.0;Data Source={0}; Jet OLEDB:Database Password ={1}", Flie_Path, PassWD);//;
            con = new OleDbConnection(connStr);   // TODO: 在此处添加构造函数逻辑
        }
        public DataTable ExeQuery(String sql)//查询
        {
            //if (con.State == ConnectionState.Closed)
            //    con.Open();
            OleDbDataAdapter oda = new OleDbDataAdapter(sql, con);
            dt = new DataTable();
            oda.Fill(dt);
            con.Close();
            return dt;
        }
        public IList<T> ExeQuerys(string sql)
        {
            OleDbDataAdapter oda = new OleDbDataAdapter(sql, con);
            dt = new DataTable();
            oda.Fill(dt);
            con.Close();
            if (dt.Rows.Count > 0)
                return TableProcessing<T>.DataTableToList(dt);
            return new List<T>();
        }


    }
}
