using System;
using System.Text;

namespace JoyKirito.Extensions
{
    public static class ExceptionExtensions
    {
        public static void Debug(this Exception exception)
        {
            StringBuilder message = new StringBuilder();
            message.AppendLine(exception.Message)
                   .AppendLine(exception.StackTrace)
                   .AppendLine(exception.Source);
            UnityEngine.Debug.LogWarning(message);
        }
    }
}
