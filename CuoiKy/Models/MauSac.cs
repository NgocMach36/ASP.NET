using System.ComponentModel.DataAnnotations;

namespace CuoiKy.Models
{
    public class MauSac
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Phải nhập tên thể loại")]
        public string Name { get; set; }
    }
}
