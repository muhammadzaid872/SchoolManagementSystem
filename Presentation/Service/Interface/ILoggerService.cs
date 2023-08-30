using System;

namespace Presentation.Service.Interface
{
    public interface ILoggerService
    {
        void ErrorLog(Exception exception);
        void InfoLog(Exception exception);
    }
}
