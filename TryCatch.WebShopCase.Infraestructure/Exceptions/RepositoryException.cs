using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TryCatch.WebShopCase.Infraestructure.Exceptions
{
    public class RepositoryException : Exception
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="message"></param>
        /// <param name="ex"></param>
        public RepositoryException(string message, Exception ex)
            :base(message,ex)
        {            
        }

    }
}
