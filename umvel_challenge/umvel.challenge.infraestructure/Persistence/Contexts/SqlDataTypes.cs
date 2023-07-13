using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace umvel.challenge.infraestructure.Persistence.Contexts
{
    public class SqlDataTypes
    {
        public static readonly string BigInt = "BIGINT";
        public static readonly string Int = "INT";
        public static readonly string SmallInt = "SMALLINT";
        public static readonly string Varchar = "VARCHAR";
        public static readonly string Date = "DATE";
        public static readonly string Time = "DATE";
        public static readonly string DateTime = "DATETIME";
        public static readonly string DateTime2 = "DATETIME2";
        public static readonly string Decimal = "DECIMAL(22, 6)";
        public static readonly string ExtendedDecimal = "DECIMAL(22, 6)";
        public static readonly string Bit = "BIT";
        public static readonly string Varchar2 = "VARCHAR({0})";
        public static readonly string Float = "FLOAT";
        public static readonly string NVarchar2 = "NVARCHAR({0})";
    }
}
