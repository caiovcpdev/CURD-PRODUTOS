using Microsoft.AspNetCore.Mvc;
using WEBCRUDMVCSQL.Models;

namespace WEBCRUDMVCSQL.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly Contexto _context;

        public UsuarioController(Contexto context)
        {
            _context = context;
        }

        public IActionResult Login()
        {     
            return View();
        }
        public IActionResult Registrar()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public IActionResult Login (string Email, string Senha)
        {

            var user = _context.Usuario.SingleOrDefault(u => u.Email == Email && u.Senha == Senha);
            if (user != null)
            {
                return RedirectToAction("Index", "Produtos");
            }
            else
            {
                //ModelState.AddModelError(string.Empty, "Credenciais inválidas. Tente novamente.");
                return View();
            }
        }
        public async Task<IActionResult> RegistrarUsuario([Bind("Id,Nome,Email,Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                _context.Add(usuario);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Home");
            }
            return View(usuario);
        }
    }
}
