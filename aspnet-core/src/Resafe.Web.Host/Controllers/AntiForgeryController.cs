using Microsoft.AspNetCore.Antiforgery;
using Resafe.Controllers;

namespace Resafe.Web.Host.Controllers
{
    public class AntiForgeryController : ResafeControllerBase
    {
        private readonly IAntiforgery _antiforgery;

        public AntiForgeryController(IAntiforgery antiforgery)
        {
            _antiforgery = antiforgery;
        }

        public void GetToken()
        {
            _antiforgery.SetCookieTokenAndHeader(HttpContext);
        }
    }
}
