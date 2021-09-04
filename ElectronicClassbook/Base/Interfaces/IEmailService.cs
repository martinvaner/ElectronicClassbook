using System;
using System.Collections.Generic;
using System.Text;

namespace Base.Interfaces
{
	public interface IEmailService
	{
		/// <summary>
		/// Sends email.
		/// </summary>
		/// <param name="to">E-mail address of recipient</param>
		/// <param name="subject">Subject of the e-mail</param>
		/// <param name="message">Message we want to send</param>
		void Send(string to, string subject, string message);
	}
}
