using System;

namespace Portfotolio.Domain
{
    public class ListItem
    {
        public string Id { get; private set; }
        public string Name { get; private set; }
        public string ItemUrl { get; private set; }

        public ListItem()
        {
            
        }

        public ListItem(string id, string name, string imageUrl)
        {
            Id = id;
            Name = name;
            ItemUrl = imageUrl;
        }

        public ListItem(string id, string name) : this (id, name, null)
        {
        }
    }
}