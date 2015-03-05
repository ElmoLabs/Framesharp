using System;
using System.Diagnostics;
using NHibernate;
using NHibernate.SqlCommand;

namespace Framesharp.Persistence.Interceptors
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