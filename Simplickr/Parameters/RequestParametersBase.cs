namespace Simplickr.Parameters
{
    public class RequestParametersBase<TDescendant> : IRequestParameters
        where TDescendant: RequestParametersBase<TDescendant>
    {
        public ParameterMap ParameterMap { get; private set; }
        public RequestModifierMode RequestModifierMode { get; private set; }

        public RequestParametersBase()
        {
            ParameterMap = new ParameterMap();
            RequestModifierMode = RequestModifierMode.None;
        }

        public TDescendant Sign()
        {
            return WithRequestModifier(RequestModifierMode.Sign);
        }

        public TDescendant Auth()
        {
            return WithRequestModifier(RequestModifierMode.Auth);
        }

        public TDescendant OAuth()
        {
            return WithRequestModifier(RequestModifierMode.OAuth);
        }

        private TDescendant WithRequestModifier(RequestModifierMode requestModifierMode)
        {
            RequestModifierMode = requestModifierMode;
            return (TDescendant)this;
        }
    }
}