using Microsoft.AspNetCore.Identity;

namespace lapora_ktm_api.Entities
{
	public class Student : IdentityUser
	{
		public string Id { get; set; }
		public string Name { get; set; }
		public string EmailSSO { get; set; }
		public string Nim { get; set; }
		public string Password { get; set; }
        public string Faculty { get; set; }
        public string Phone { get; set; }
        public Report Report { get; set; }
	}
}

