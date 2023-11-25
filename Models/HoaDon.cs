using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CuoiKy.Models
{
	public class HoaDon
	{
		[Key]
		public int Id { get; set; }
		public string ApplicationUserId { get; set; }
		[ForeignKey("ApplicationUserId")]
		public ApplicationUser ApplicationUser { get; set; }
		public double Total { get; set; }
		public DateTime OrderDate { get; set; }
		public string? OrderStatus { get; set; }
		public string PhoneNumber { get; set; }
		public string Name { get; set; }
		public string Address { get; set; }
	}
}
