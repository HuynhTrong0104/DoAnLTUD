using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Data;
namespace CuaHangTienLoiCircleK
{
    internal class Connect
    {
        private static string conn = "Data Source=HUYNHTRONG\\MAYA0;Initial Catalog=QLCUAHANGTLCIRCLEKS;Integrated Security=True";
        public static SqlConnection KetNoi()
        {
            return new SqlConnection(conn);
        }
    }
}
