
using Company.G05.PL.Services;
using Company.G05.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text;

namespace Company.G05.PL.Controllers
{
	[Authorize]
	public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IScopedServices scopedServices01;
        private readonly IScopedServices scopedServices02;
        private readonly ITransientServices transientServices01;
        private readonly ITransientServices transientServices02;
        private readonly ISingeltonServices singeltonServices01;
        private readonly ISingeltonServices singeltonServices02;

        public HomeController(ILogger<HomeController> logger ,
            IScopedServices scopedServices01,
            IScopedServices scopedServices02,
            ITransientServices transientServices01,
            ITransientServices transientServices02,
            ISingeltonServices singeltonServices01,
            ISingeltonServices singeltonServices02


            )
        {
            _logger = logger;
            this.scopedServices01 = scopedServices01;
            this.scopedServices02 = scopedServices02;
            this.transientServices01 = transientServices01;
            this.transientServices02 = transientServices02;
            this.singeltonServices01 = singeltonServices01;
            this.singeltonServices02 = singeltonServices02;
        }

        // Get /Home/TestLifeTime
        public string TestLifeTime()
        {
            StringBuilder builder = new StringBuilder();
            builder.Append($"Scoped01 :: {scopedServices01.GetGuid()}\n");
            builder.Append($"Scoped02 :: {scopedServices02.GetGuid()}\n\n");

            builder.Append($"transient01 :: {transientServices01.GetGuid()}\n");
            builder.Append($"transient02 :: {transientServices02.GetGuid()}\n\n");

            builder.Append($"singelton01 :: {singeltonServices01.GetGuid()}\n");
            builder.Append($"singelton02 :: {singeltonServices02.GetGuid()}\n\n");

            return  builder.ToString();

        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
