using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RepconWeb
{
	public class LoginModel : PageModel
	{
		#region Variables and Constants

		private readonly RepconContext _context;
		public string ResultMessage { get; set; } = string.Empty;

		private const string ErrorMessageTemplate = @"
			<div class=""alert alert-danger"" role=""alert"">{0}</div>";
		private const string IncorrectUandPErrorMessageTemplate = @"Неверный логин или пароль.";
		private const string UNFMessageTemplate = @"Пользователь не найден!";

		#endregion Variables and Constants

		#region Public Methods

		public LoginModel(RepconContext context) => _context = context;
		
		public IActionResult OnPost(string login, string password)
		{
			password = ConvertToSHA256(password);
			Admn admn = _context.Admn.Where(v => v.Login == login).FirstOrDefault();
			if (admn != null)
			{
				if (admn.Pwd == password)
				{
					ResultMessage = string.Empty;
					return RedirectToPage("Main");
				}
				else
				    ResultMessage = string.Format(ErrorMessageTemplate, IncorrectUandPErrorMessageTemplate);
			}
			else 
			    ResultMessage = string.Format(ErrorMessageTemplate, UNFMessageTemplate);
			return Page();
		}

		#endregion On Events

		#region Private Methods

		private static string ConvertToSHA256(string randomString)
		{
			var crypt = new SHA256Managed();
			var hash = new StringBuilder();
			byte[] crypto = crypt.ComputeHash(Encoding.UTF8.GetBytes(randomString));
			foreach (byte theByte in crypto)
				hash.Append(theByte.ToString("x2"));
			return hash.ToString();
		}

		#endregion Private Methods
	}
}