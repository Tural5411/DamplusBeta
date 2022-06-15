using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Damplus.Entities.ComplexTypes;

namespace Damplus.Entities.Concrete
{
    public class ArticleRightSideBarWidgetOptions
    {
        [DisplayName("Widget Başlığı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [MaxLength(150, ErrorMessage = "{0} alanı en fazla {1} karakter olmalıdır.")]
        [MinLength(5, ErrorMessage = "{0} alanı en az {1} karakter olmalıdır.")]
        public string Header { get; set; }
        [DisplayName("Məqalə Sayısı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [Range(0,50,ErrorMessage = "{0} alanı en az {1}, en fazla {2} olmalıdır.")]
        public int TakeSize { get; set; }
        [DisplayName("Kategoriya")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int CategoryId { get; set; }
        [DisplayName("Filtirləmə Növü")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public FilterBy FilterBy { get; set; }
        [DisplayName("Sıralama Növü")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public OrderBy OrderBy { get; set; }
        [DisplayName("Sıralama Ölçüsü")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public bool IsAscending { get; set; }
        [DisplayName("Başlanğıc Tarixi")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [DataType(DataType.Date,ErrorMessage = "{0} alanı tarih formatında olmalıdır.")]
        public DateTime StartAt { get; set; }
        [DisplayName("Bitiş Tarixi")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        [DataType(DataType.Date, ErrorMessage = "{0} alanı tarih formatında olmalıdır.")]
        public DateTime EndAt { get; set; }
        [DisplayName("Maksimum Oxunma Sayı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int MaxViewCount { get; set; }
        [DisplayName("Minimum Oxunma Sayı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int MinViewCount { get; set; }
        [DisplayName("Maksimum Şərh Sayı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int MaxCommentCount { get; set; }
        [DisplayName("Minimum Şərh Sayı")]
        [Required(ErrorMessage = "{0} alanı zorunludur.")]
        public int MinCommentCount { get; set; }
    }
}
