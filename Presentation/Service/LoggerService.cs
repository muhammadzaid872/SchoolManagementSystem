using Presentation.Service.Interface;
using System;

namespace Presentation.Service
{
    public class LoggerService: ILoggerService
    {
       public void ErrorLog(Exception exception)
        {
            //Here we have to log our errors
        }
        public void InfoLog(Exception exception)
        {
            //Here we have to log our Info
        }
    }
}
