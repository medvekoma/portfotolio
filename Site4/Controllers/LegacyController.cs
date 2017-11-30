using System.Web.Mvc;
using Portfotolio.Domain;
using Portfotolio.Site.Services.Models;
using Portfotolio.Site4.Attributes;

namespace Portfotolio.Site4.Controllers
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