using System;

namespace log4net.Core
{
	public abstract class SecurityContext
	{
		public abstract IDisposable Impersonate(object state);
	}
}
