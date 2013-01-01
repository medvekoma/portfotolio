namespace Simplickr.Parameters
{
    public class SafeSearch
    {
        private readonly string _value;

        private SafeSearch(string value)
        {
            _value = value;
        }

        public static SafeSearch Safe = new SafeSearch("1");
        public static SafeSearch Moderate = new SafeSearch("2");
        public static SafeSearch Restricted = new SafeSearch("3");

        public override string ToString()
        {
            return _value;
        }
    }
}