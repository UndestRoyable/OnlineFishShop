using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace OnlineFishShop.Web.Controllers
{
    //TODO Stoyan Lupov 26-12-2018 Implement methods
    public class BaseController : Controller
    {
        public void Success(string message, bool dismissable = true)
        {
//            AddAlert(AlertStyles.Success, message, dismissable);
        }

        public void Information(string message, bool dismissable = true)
        {
//            AddAlert(AlertStyles.Information, message, dismissable);
        }

        public void Warning(string message, bool dismissable = false)
        {
//            AddAlert(AlertStyles.Warning, message, dismissable);
        }

        public void Danger(string message, bool dismissable = false)
        {
//            AddAlert(AlertStyles.Danger, message, dismissable);
        }

        private void AddAlert(string alertStyle, string message, bool dismissable)
        {
//            var alerts = this.TempData.ContainsKey(Alert.TempDataKey)
//                ? this.TempData.Get<List<Alert>>(Alert.TempDataKey)
//                : new List<Alert>();
//
//            alerts.Add(new Alert
//            {
//                AlertStyle = alertStyle,
//                Message = message,
//                Dismissable = dismissable
//            });
//
//            this.TempData.Put<List<Alert>>(Alert.TempDataKey, alerts);
        }
    }
}
