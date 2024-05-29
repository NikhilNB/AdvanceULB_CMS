using AdvanceULB_CMS.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace AdvanceULB_CMS.Controllers
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

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //public IActionResult Map(IFormFile file)
        //{
        //    if (file != null && file.Length > 0)
        //    {
        //        try
        //        {
        //            var fileName = Path.GetFileName(file.FileName);
        //            var path = Path.Combine(Server.MapPath("~/Uploads"), fileName);
        //            file. (path);

        //            var kml = new Kml();
        //            using (var stream = System.IO.File.OpenRead(path))
        //            {
        //                var parser = new Parser();
        //                parser.Parse(stream, false);
        //                kml = parser.Root as Kml;
        //            }

        //            var placemarks = kml.Flatten().OfType<Placemark>();
        //            var coordinates = new List<LatLng>();
        //            foreach (var placemark in placemarks)
        //            {
        //                if (placemark.Geometry is Point point)
        //                {
        //                    coordinates.Add(new LatLng(point.Coordinate.Latitude, point.Coordinate.Longitude));
        //                }
        //                // Add conditions to handle other geometry types as needed
        //            }

        //            ViewBag.Coordinates = coordinates;
        //            ViewBag.Message = "File uploaded successfully!";
        //        }
        //        catch (Exception ex)
        //        {
        //            ViewBag.Message = "Error occurred: " + ex.Message;
        //        }
        //    }
        //    else
        //    {
        //        // Default coordinates when no file is uploaded
        //        ViewBag.Coordinates = new List<LatLng> { new LatLng(40.7128, -74.0060) }; // Default coordinates for New York City
        //        ViewBag.Message = "Please select a file.";
        //    }

        //    return View();
        //}

        //// Action method to render the map view
        //public ActionResult MapView()
        //{
        //    return View();
        //}
    }


}
