namespace HappyThoughts.Web.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using HappyThoughts.Web.ViewModels.InputModels;
    using Microsoft.AspNetCore.Mvc;

    public class TopicsController : BaseController
    {
        public IActionResult Create()
        {
            return this.View();
        }

        [HttpPost]
        public IActionResult Create(CreateTopicInputModel input)
        {
            ;
            return this.Redirect("/");
        }

        public IActionResult Details()
        {
            return this.View();
        }
    }
}
