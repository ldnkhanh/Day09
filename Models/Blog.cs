using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace day09.Models;

public class Blog : IEntity
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên không được để trống"), StringLength(100)]
    [Display(Name = "Tiêu đề")]
    public string Name { get; set; } = "";

    [Display(Name = "Trạng thái")]
    public byte Status { get; set; } = 1;

    [Display(Name = "Ngày tạo"), DataType(DataType.Date), Column(TypeName = "date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreatedDate { get; set; } = DateTime.Today;

    [Display(Name = "Hình ảnh"), StringLength(100), Column(TypeName = "varchar(100)")]
    public string? Image { get; set; }

    [Display(Name = "Mô tả"), StringLength(350)]
    public string? Description { get; set; }
}
