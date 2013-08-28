namespace Portfotolio.Domain.Persistency
{
	public interface IAuthenticationStorage
	{
		AuthenticationInfo GetAuthenticationInfo();
		void SetAuthenticationInfo(AuthenticationInfo authenticationInfo);
		void Clear();
	}
}