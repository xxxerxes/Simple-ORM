using System;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace Framework
{
	public class ConfigurationManager
	{
		private static string _SqlConnectionStringCustom = null;

		static ConfigurationManager()
		{
			IConfigurationRoot configuration = new ConfigurationBuilder()
				.SetBasePath(Directory.GetCurrentDirectory())
				.AddJsonFile("appsettings.json")
				.Build();
			_SqlConnectionStringCustom = configuration["ConnectionStrings:Customers"];
				
		}

		public static string SqlConnectionStringCustom
        {
            get
            {
				return _SqlConnectionStringCustom;
            }
        }
	}
}

