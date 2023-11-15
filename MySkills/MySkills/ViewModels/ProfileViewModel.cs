using System.ComponentModel.DataAnnotations;

namespace MySkills.ViewModels
{
	public class ProfileViewModel
	{
		public int? ProfileId {  get; set; }

		[Required]
		public string? FirstName { get; set; }

		[Required]
		public string? LastName { get; set; }

		[Required]
		public string? ProfileName { get; set; }

		public string? ProfileImage { get; set; }
	}
}
