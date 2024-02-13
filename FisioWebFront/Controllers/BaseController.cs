using Microsoft.AspNetCore.Mvc;
using static FisioWebFront.Enum.Enum;

namespace FisioWebFront.Controllers
{
    public class BaseController : Controller
    {
        public void Alert(string message, NotificationType notificationType)
        {
            //var msg = "<script language='javascript'>Swal.fire('" + notificationType.ToString().ToUpper() + "', '" + message + "','" + notificationType + "')" + "</script>";
            var msg = "<script language='javascript'>Swal.fire({icon:'success',title:'Fisiovet:',text: '" + message + "',type:'" + notificationType + "',allowOutsideClick: false,allowEscapeKey: false,allowEnterKey: false})" + "</script>";
            TempData["notification"] = msg;
        }

        /// </summary>
        /// <param name="message">The message to display to the user.</param>
        /// <param name="notifyType">The type of notification to display to the user: Success, Error or Warning.</param>
        public void Message(string message, NotificationType notifyType)
        {
            TempData["Notification2"] = message;

            switch (notifyType)
            {
                case NotificationType.success:
                    TempData["NotificationCSS"] = "alert-box success";
                    break;
                case NotificationType.error:
                    TempData["NotificationCSS"] = "alert-box errors";
                    break;
                case NotificationType.warning:
                    TempData["NotificationCSS"] = "alert-box warning";
                    break;

                case NotificationType.info:
                    TempData["NotificationCSS"] = "alert-box notice";
                    break;
            }
        }
    }
}

