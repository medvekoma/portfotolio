using System;

namespace Portfotolio.Site.Models
{
    public enum AccountType
    {
        Flickr,
        Picasa,
        SmugMug,
    }

    public interface IUserAccount
    {
        string AccountName { get; }
        AccountType AccountType { get; }
    }

    public class FlickrAccount : IUserAccount
    {
        private readonly string _accountName;

        public FlickrAccount(string accountName)
        {
            _accountName = accountName;
        }

        public string AccountName
        {
            get { return _accountName; }
        }

        public AccountType AccountType
        {
            get { return AccountType.Flickr; }
        }
    }

    public class PicasaAccount : IUserAccount
    {
        private readonly string _accountName;

        public PicasaAccount(string accountName)
        {
            _accountName = accountName;
        }

        public string AccountName
        {
            get { return _accountName; }
        }

        public AccountType AccountType
        {
            get { return AccountType.Picasa; }
        }
    }

    public class SmugMugAccount : IUserAccount
    {
        private readonly string _accountName;

        public SmugMugAccount(string accountName)
        {
            _accountName = accountName;
        }

        public string AccountName
        {
            get { return _accountName; }
        }

        public AccountType AccountType
        {
            get { return AccountType.SmugMug; }
        }
    }
}