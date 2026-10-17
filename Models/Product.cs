using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace day09.Models;

public class Product : IEntity
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Tên không được để trống"), StringLength(100)]
    [Display(Name = "Tên sản phẩm")]
    public string Name { get; set; } = "";

    [Required(ErrorMessage = "Giá không được để trống")]
    [Range(0, double.MaxValue, ErrorMessage = "Giá phải >= 0")]
    [Display(Name = "Giá")]
    public double Price { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Giá khuyến mãi phải >= 0")]
    [Display(Name = "Giá khuyến mãi")]
    public double SalePrice { get; set; } = 0;

    [Display(Name = "Trạng thái")]
    public byte Status { get; set; } = 1;

    [Required(ErrorMessage = "Vui lòng chọn danh mục")]
    [Display(Name = "Danh mục")]
    public int CategoryId { get; set; }

    [Display(Name = "Ngày tạo"), DataType(DataType.Date), Column(TypeName = "date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime CreatedDate { get; set; } = DateTime.Today;

    [Display(Name = "Hình ảnh"), StringLength(100), Column(TypeName = "varchar(100)")]
    public string? Image { get; set; }

    [Display(Name = "Mô tả"), StringLength(350)]
    public string? Description { get; set; }

    [ValidateNever]
    public Category? Category { get; set; }
}
