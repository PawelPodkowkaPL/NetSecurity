using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Chinook.Core.DataAccess;
using Chinook.Core.Domain;
using Microsoft.EntityFrameworkCore;

namespace Chinook.Web.Pages
{
    public class NewCustomerModel : PageModel
    {
        private readonly Chinook.Core.DataAccess.ChinookContext _context;

        public NewCustomerModel(Chinook.Core.DataAccess.ChinookContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Customer Customer { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            var isEmployee = CheckIfEmployee(Customer.FirstName, Customer.LastName);
            if (isEmployee)
            {
                throw new ArgumentException("Sorry Dude this guy is already an Employee");
            }

            _context.Customers.Add(Customer);
            await _context.SaveChangesAsync();

            return Redirect($"./CustomerEmployee?Name={Customer.LastName}");
        }

        private bool CheckIfEmployee(string firstName, string lastName)
        {
            using var command = _context.Database.GetDbConnection().CreateCommand();
            command.Connection.Open();
            
            command.CommandText = $"SELECT * FROM Employees WHERE LastName = '{lastName}' AND FirstNAme = '{firstName}' LIMIT 1";
            
            var reader = command.ExecuteReader();
            var result = reader.Read();
            command.Connection.Close();
            return result;
        }
    }
}
