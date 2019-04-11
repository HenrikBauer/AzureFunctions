using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SDX.FunctionsDemo.ImageProcessing;
using SDX.FunctionsDemo.Web.Models;

namespace SDX.FunctionsDemo.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly IImageFileService _imageFileService;

        public HomeController(IImageFileService imageFileService)
        {
            _imageFileService = imageFileService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost("UploadFile")]
        public async Task<IActionResult> Post(List<IFormFile> files)
        {
            var file = files.FirstOrDefault();
            if (file == null)
            {
                this.ViewData.SetMessage("warning", "Keine Datei angegeben!");
                return View(nameof(Index));
            }

            if (file.ContentType != ImageUtils.ContentTypePng)
            {
                this.ViewData.SetMessage("error", "Es werden nur .png-Dateien unterstützt!");
                return View(nameof(Index));
            }

            var data = file.CopyToArray();
            var id = await _imageFileService.UploadImageAsync(file.FileName, file.ContentType, data);
            if (string.IsNullOrEmpty(id))
            {
                this.ViewData.SetMessage("danger", "Der Upload konnte nicht durchgeführt werden!");
                return View(nameof(Index));
            }

            return RedirectToAction(nameof(Images), new { id, file.FileName });
        }

        public async Task<IActionResult> Images(string id, string fileName)
        {
            var images = new List<Tuple<string, string>>();
            foreach (var imageType in ImageUtils.ImageTypes)
            {
                var image = await GetImageDataAsync(id, imageType);
                if (image != null)
                    images.Add(image);
            }

            var available = images.Where(image => image.Item2 != null).Count();
            if (available == images.Count)
            {
                this.ViewData.SetMessage("success", "Alle Bilder wurden verarbeitet!");
            }
            else
            {
                this.ViewData.SetMessage("warning", "Die Verarbeitung ist noch unvollständig: " + available + "/" + images.Count);
                ViewData["refresh"] = 2;
            }

            this.ViewData.SetImages(fileName, images.ToArray());
            return View();
        }

        private async Task<Tuple<string, string>> GetImageDataAsync(string id, string imageType)
        {
            // <img alt="..." src="data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAADIA..." />
            var image = await _imageFileService.GetImageAsync(id, imageType);
            if (image == null)
                return Tuple.Create(imageType, (string)null);
            var data = "data:image/png;base64," + image.ToBase64String();
            return Tuple.Create(imageType, data);
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
