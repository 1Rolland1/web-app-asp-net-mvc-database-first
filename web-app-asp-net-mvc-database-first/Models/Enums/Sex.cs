using System.ComponentModel.DataAnnotations;
using System.Web;
using System.Web.Mvc;

namespace web_app_asp_net_mvc_database_first.Models
{
	public enum Sex
	{
		[Display(Name = "Женский")]
		Female = 1,
		
		[Display(Name = "Мужской")]
		Male = 2,
	}
}