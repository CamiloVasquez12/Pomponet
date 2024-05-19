

namespace PomponetWebsite.ViewsModels
{
	public class PeopleVM
	{
		public int Id_Person { get; set; }
		public required string Names { get; set; }
		public required string Email { get; set; }
		public required string UserName { get; set; }
		public required string Password { get; set; }
		public required int Age { get; set; }
		public bool Deleted { get; set; }
		public required string ConfirmPassword {  get; set; }
	}
}
