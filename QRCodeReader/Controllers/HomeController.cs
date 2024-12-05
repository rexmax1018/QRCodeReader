using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRCodeReader.Models;
using System.Diagnostics;

namespace QRCodeReader.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error(int? statusCode = null)
        {
            var viewModel = new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier };

            if (statusCode.HasValue)
            {
                // 根據狀態碼進行判斷，傳遞錯誤訊息
                ViewData["ErrorMessage"] = statusCode switch
                {
                    404 => "Page not found.",
                    403 => "You do not have permission to view this page.",
                    401 => "You are not authorized.",
                    500 => "An unexpected error occurred. Please try again later.",
                    _ => "An unexpected error occurred."
                };
            }

            return View(viewModel);
        }
    }
}
