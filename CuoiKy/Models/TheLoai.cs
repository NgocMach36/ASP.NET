using System.ComponentModel.DataAnnotations;

namespace CuoiKy.Models
{
    public class TheLoai
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "Phải nhập tên thể loại")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Không đúng định dạng ngày")]
        public DateTime DateCreate { get; set; } = DateTime.Now;
    }
}
