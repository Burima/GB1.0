using System;
using GB.Web.BE;

namespace GB.Web.BE
{
    public class EmailMessageBody
    {
        public string CreateBody(User user, string messagebodyPath)
        {
            string message = System.IO.File.ReadAllText(messagebodyPath);
            message = message.Replace("#name", user.name);
            message = message.Replace("#email", user.email);
            message = message.Replace("#phone", user.phone);
            message = message.Replace("#message", user.message);           

            return message.ToString();
        }
    }
}