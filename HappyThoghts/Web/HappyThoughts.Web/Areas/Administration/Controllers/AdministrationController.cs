﻿namespace HappyThoughts.Web.Areas.Administration.Controllers
{
    using HappyThoughts.Common;
    using HappyThoughts.Web.Controllers;

    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize(Roles = GlobalConstants.AdministratorRoleName)]
    [Area("Administration")]
    public class AdministrationController : BaseController
    {
    }
}
