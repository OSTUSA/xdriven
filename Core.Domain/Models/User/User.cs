using System;

namespace Core.Domain.Models
{
	public class User : ModelBase<User>
	{
		public User ()
		{
		}

		public User (string userName, string password)
		{
			UserName = userName;
			Password = password;
		}

		public string UserName { get; set; }
		public string Password { get; set; }
	}
}

