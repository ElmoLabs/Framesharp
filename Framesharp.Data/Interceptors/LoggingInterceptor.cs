using System;
using System.Diagnostics;
using NHibernate;
using NHibernate.SqlCommand;

namespace Framesharp.Data.Interceptors
{
    public class LoggingInterceptor : EmptyInterceptor, IInterceptor
    {
        public override SqlString OnPrepareStatement(SqlString sql)
        {
            Console.WriteLine(sql);
            Debug.WriteLine(sql);

            return sql;
        }
    }
}