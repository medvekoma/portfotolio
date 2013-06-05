using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Site.Helpers;
using Portfotolio.Site.Models;
using Portfotolio.Site.Services.Models;

namespace Portfotolio.Site.Controllers
{
    [HideFromSearchEngines(AllowRobots.None)]
    public class LegacyController : Controller
    {
        private readonly IPhotoEngine _photoEngine;

        public LegacyController(IPhotoEngine photoEngine)
        {
            _photoEngine = photoEngine;
        }

        public ActionResult Set(string id)
        {
            var album = _photoEngine.GetAlbum(id);
            return RedirectToActionPermanent("album", "photo", new {id = album.AuthorId, secondaryId = id});
        }
    }
}