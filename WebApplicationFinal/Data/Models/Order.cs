using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.ComponentModel.DataAnnotations;

namespace WebApplicationFinal.Data.Models
{
    public class Order
    {
        [BindNever]//не показывать поле на странице
        public int id { get; set; }

        [Display(Name = "Имя")]//отображаем подпись
        [StringLength(20)]//проверка максимального числа символов
        [Required(ErrorMessage = "Длина имени более 20 символов")]//вывод ошибки
        public string name { get; set; }

        [Display(Name = "Фамилия")]
        [StringLength(20)]
        [Required(ErrorMessage = "Длина фамилии более 20 символов")]
        public string surname { get; set; }

        [Display(Name = "Адрес")]
        [StringLength(20)]
        [Required(ErrorMessage = "Длина адресса более 20 символов")]
        public string adress { get; set; }

        [Display(Name = "Номер телефона")]
        [DataType(DataType.PhoneNumber)]
        [StringLength(8)]
        [Required(ErrorMessage = "Длина телефона более 7 знаков")]
        public string phone { get; set; }

        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        [StringLength(50)]
        [Required(ErrorMessage = "Длина email более 20 символов")]
        public string email { get; set; }

        [BindNever]
        [ScaffoldColumn(false)]//не отображать в исходном коде
        public DateTime orderTime { get; set; }

        public List<OrderDetail> orderDetails { get; set; }
    }
}
