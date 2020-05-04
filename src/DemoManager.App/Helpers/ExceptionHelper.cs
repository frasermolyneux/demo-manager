using System;

namespace DemoManager.App.Helpers
{
    public static class ExceptionHelper
    {
        public static Exception GetDeepestException(this Exception exception)
        {
            while (true)
            {
                if (exception?.InnerException == null || exception.InnerException == exception) return exception;
                exception = exception.InnerException;
            }
        }
    }
}