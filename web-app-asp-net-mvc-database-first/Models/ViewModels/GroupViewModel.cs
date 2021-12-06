using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using web_app_asp_net_mvc_database_first.Extensions;
using web_app_asp_net_mvc_database_first.Models.Attributes;

namespace web_app_asp_net_mvc_database_first.Models
{
    public class GroupViewModel
    {
        /// <summary>
        /// Id
        /// </summary> 
        [HiddenInput(DisplayValue = false)]
        public int Id { get; set; }

        /// <summary>
        /// Название группы
        /// </summary>    
        [Required]
        [Display(Name = "Название группы", Order = 5)]
        public string GroupName { get; set; }

        /// <summary>
        /// Количество студентов в группе
        /// </summary>  
        [Required]
        [Display(Name = "Количество студентов в группе", Order = 6)]
        public int NumberOfStudents { get; set; }



    }
}