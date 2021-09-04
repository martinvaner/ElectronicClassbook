using System;
using System.Collections.Generic;
using System.Text;

namespace Base
{
	public class SecurityHelper
	{
		public string GenerateToken()
		{
			var guid1 = Guid.NewGuid().ToString();
			var guid2 = Guid.NewGuid().ToString();
			return guid1 + guid2;
		}
	}
}
