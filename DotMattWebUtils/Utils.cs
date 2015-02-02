using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DotMattLibrary.Utils
{
    class DotMatt
    {
    }

    public class ExceptionHelper
    {
        public string GetExceptionMessage(Exception ex, string headerMessage = "")
        {
            string exceptionMessage = GetExceptionDetail(ex, headerMessage + "\r\n--------- Outer Exception Data ---------");

            if (ex.InnerException != null)
            {
                exceptionMessage += GetExceptionDetail(ex.InnerException, "--------- Inner Exception Data ---------");
            }

            return exceptionMessage;
        }


        public string GetExceptionDetail(Exception ex, string Header)
        {
            var sb = new StringBuilder();

            sb.AppendLine(Header);
            sb.AppendLine(string.Format("Message: {0}", ex.Message));
            sb.AppendLine(string.Format("Exception Type: {0}", ex.GetType().FullName));
            sb.AppendLine(string.Format("Source: {0}", ex.Source));
            sb.AppendLine(string.Format("StrackTrace: {0}", ex.StackTrace));
            sb.AppendLine(string.Format("TargetSite: {0}", ex.TargetSite));

            return sb.ToString();
        }

        /// <summary>
        /// This method will parse out just the message string from a full execption message text block.
        /// </summary>
        /// <param name="ExceptionMessage"></param>
        /// <returns></returns>
        public string GetMessageFromException(string ExceptionMessage)
        {
            string messageBeginText = "Message: ";
            string endMessageText = "Exception Type:";

            int startOfMessage = ExceptionMessage.IndexOf(messageBeginText);
            int endOfMessage = ExceptionMessage.IndexOf(endMessageText);

            if (startOfMessage > -1 && endOfMessage > -1)
            {
                string message = ExceptionMessage.Substring(startOfMessage + messageBeginText.Length, (endOfMessage - startOfMessage - messageBeginText.Length));
                return message;
            }
            else
                return ExceptionMessage;

        }
    }
}


