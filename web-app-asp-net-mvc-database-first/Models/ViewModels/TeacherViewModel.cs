using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_app_asp_net_mvc_database_first.Extensions;
using web_app_asp_net_mvc_database_first.Models;
using web_app_asp_net_mvc_database_first.Models.Attributes;
using web_app_asp_net_mvc_database_first.Models.Entities;

namespace web_app_asp_net_mvc_database_first.Models
{
    public class TeacherViewModel
    {
        ///<summary>
        /// Id
        /// </summary> 
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        ///<summary>
        /// Имя преподавателя
        /// </summary> 
        [Required]
        [Display(Name = "Преподаватель", Order = 5)]
        public string Name { get; set; }

        /// <summary>
        /// Пол
        /// </summary> 
        [ScaffoldColumn(false)]
        public Sex Sex { get; set; }

        [Display(Name = "Пол", Order = 50)]
        [UIHint("DropDownList")]
        [TargetProperty("Sex")]
        [NotMapped]
        public IEnumerable<SelectListItem> SexDictionary
        {
            get
            {
                var dictionary = new List<SelectListItem>();

                foreach (Sex type in Enum.GetValues(typeof(Sex)))
                {
                    dictionary.Add(new SelectListItem
                    {
                        Value = ((int)type).ToString(),
                        Text = type.GetDisplayValue(),
                        Selected = type == Sex
                    });
                }

                return dictionary;
            }
        }

        /// <summary>
        /// Фото преподавателя
        /// </summary> 
        [ScaffoldColumn(false)]
        public virtual TeacherImages TeacherImage { get; set; }

        [Display(Name = "Фото преподавателя", Order = 60)]
        [NotMapped]
        public HttpPostedFileBase TeacherImageFile { get; set; }

        }
}