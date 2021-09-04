using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace Payment
{
	public class Paypal
	{
		// Step 1: Get an access token
		// Step 2: Create the payment
		//		Step 2.1: Get the approval_url and paste it into a browser - in response will be "approved_url"
		// Step 3: Execute payment

		string clientId = "AVTr7v7PX02HulLxgihT8hFDV1JBAl3pMXMLItLqYD24Ud5P8dmV4qo5dXfBewOM0vLk8F39PX8B5iOf";
		string secret = "EEMioAYscemJGLf8u1GWxrWSjT_iaRUE-5hIMtkBoo2HNEHqL_yc5HjDWa3flmVW1no1YGWR5q4Q1qne";

		public void CreatePayment()
		{
			
		}


		private HttpClient GetPaypalHttpClient()
		{
			string address = "https://api.sandbox.paypal.com";

			var httpClient = new HttpClient()
			{
				BaseAddress = new Uri(address),
				Timeout = TimeSpan.FromSeconds(30)
			};

			return httpClient;
		}

		private void GetPayPalAccessTokenAsync(HttpClient httpClient)
		{
			//exchange client ID and secret for an access token in an OAuth 2.0 token call
			// POST on https://api-m.sandbox.paypal.com/v1/oauth2/token

		}

		private void CreatePaypalPaymentAsync(HttpClient httpClient, string token)
		{

		}
	}
}
