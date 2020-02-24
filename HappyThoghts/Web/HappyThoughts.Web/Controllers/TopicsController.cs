namespace HappyThoughts.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using Microsoft.AspNetCore.Mvc;

    public class TopicsController : BaseController
    {
        public IActionResult Create()
        {
            return this.View();
        }

        public IActionResult Details()
        {
            return this.View();
        }
    }
}
