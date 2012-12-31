using System;
using System.Linq;

namespace Simplickr
{
    public class Extras
    {
        public static Extras Description = new Extras("description");
        public static Extras License = new Extras("license");
        public static Extras DateUpload = new Extras("date_upload");
        public static Extras DateTaken = new Extras("date_taken");
        public static Extras OwnerName = new Extras("owner_name");
        public static Extras IconServer = new Extras("icon_server");
        public static Extras OriginalFormat = new Extras("original_format");
        public static Extras LastUpdate = new Extras("last_update");
        public static Extras Geo = new Extras("geo");
        public static Extras Tags = new Extras("tags");
        public static Extras MachineTags = new Extras("machine_tags");
        public static Extras ODims = new Extras("o_dims");
        public static Extras Views = new Extras("views");
        public static Extras Media = new Extras("media");
        public static Extras PathAlias = new Extras("path_alias");
        public static Extras UrlSq = new Extras("url_sq");
        public static Extras UrlT = new Extras("url_t");
        public static Extras UrlS = new Extras("url_s");
        public static Extras UrlQ = new Extras("url_q");
        public static Extras UrlM = new Extras("url_m");
        public static Extras UrlN = new Extras("url_n");
        public static Extras UrlZ = new Extras("url_z");
        public static Extras UrlC = new Extras("url_c");
        public static Extras UrlL = new Extras("url_l");
        public static Extras UrlO = new Extras("url_o");

        private readonly string[] _values;

        private Extras(params string[] values)
        {
            _values = values;
        }

        public static Extras operator |(Extras extras1, Extras extras2)
        {
            var values = extras1._values.Concat(extras2._values).ToArray();
            return new Extras(values);
        }

        public override string ToString()
        {
            return string.Join(", ", _values);
        }
    }
}