using System;
using Simplickr.Configuration;

namespace Simplickr
{
    public interface IRequestModifier
    {
        void Modify(ParameterMap parameterMap);
    }

    public class OAuthRequestModifier : IRequestModifier
    {
        private readonly IFlickrRequestUrlProvider _flickrRequestUrlProvider;

        public OAuthRequestModifier(IFlickrRequestUrlProvider flickrRequestUrlProvider)
        {
            _flickrRequestUrlProvider = flickrRequestUrlProvider;
        }

        public void Modify(ParameterMap parameterMap)
        {

            throw new NotImplementedException();
        }
    }

    public class SignRequestModifier : IRequestModifier
    {
        private readonly IFlickrSignatureGenerator _signatureGenerator;

        public SignRequestModifier(IFlickrSignatureGenerator signatureGenerator)
        {
            _signatureGenerator = signatureGenerator;
        }

        public void Modify(ParameterMap parameterMap)
        {
            _signatureGenerator.AddSignature(parameterMap);
        }
    }

}
