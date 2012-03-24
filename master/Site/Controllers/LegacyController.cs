using System.Web.Mvc;
using Portfotolio.Domain;

namespace Portfotolio.Site.Controllers
{
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