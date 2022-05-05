using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Chinook.Web.Pages
{
    public class CustomerEmployeeModel : PageModel
    {
        [FromQuery(Name ="name")]
        public string CustomerName { get; set; }

        public void OnGet()
        {

        }
    }
}
