using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.Ajax.Utilities;
using Model.EF;
using Newtonsoft.Json;

namespace OnlineShopAPI.Controllers
{
    [RoutePrefix("api/LeftSiddebar")]
    public class LeftSiddebarController : ApiController
    {
       private OnlineShopDbContext db=new OnlineShopDbContext();

        public string GetSidebarMenu()
        {
            List<Model.EF.LeftSidebar> sidebar = db.LeftSidebars.ToList();
            return JsonConvert.SerializeObject(sidebar, Formatting.Indented);
        }
        
    }
}