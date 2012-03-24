﻿using System.Collections.Generic;
using FlickrNet;
using Portfotolio.Domain;

namespace Portfotolio.FlickrEngine
{
    public interface IFlickrConverter
    {
        DomainPhotos Convert(PhotoCollection photoCollection);
        DomainPhotos Convert(PhotosetPhotoCollection photosetPhotos);
        ListItems Convert(PublicGroupInfoCollection groups);
        ListItems Convert(ContactCollection contacts);
        Album Convert(Photoset photoCollection);
        Albums Convert(PhotosetCollection photosets);
    }
}