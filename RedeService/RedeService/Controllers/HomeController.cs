using Grpc.Core;
using Microsoft.AspNetCore.Mvc;
using RedeService.Models;
using System.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using System.Text;

namespace RedeService.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private object _hostingEnvironment;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Page()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet, Route("Home/CriarArquivo/{dados}")]
        public ActionResult CriarArquivo(string dados)
        {
            string caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Arquivos", "numeros_ordenar.txt");

            try
            {

                string textoSubstituido = dados.Replace(",", "\n");
                string[] valores = { textoSubstituido };

                
                using (StreamWriter arquivo = new StreamWriter(caminhoArquivo, false, Encoding.UTF8))
                {

                    foreach (string valor in valores)
                    {
                        arquivo.WriteLine(valor + "\t");
                    }
                    arquivo.WriteLine(); 
                }

                ViewBag.Mensagem = "Os valores foram escritos no arquivo em colunas no formato de tabulação.";
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = $"Ocorreu um erro ao escrever os valores no arquivo: {ex.Message}";
            }
            return View("Index");
        }


        [HttpGet, Route("Home/LerArquivo")]
        public IActionResult LerArquivo()
        {
            string caminhoArquivo = "C:\\Users\\Anonymous\\Desktop\\RedeService\\RedeService\\RedeService\\wwwroot\\Arquivos\\numeros_ordenar.txt"; 

            try
            {
                
                if (System.IO.File.Exists(caminhoArquivo))
                {
                    
                    Process.Start("notepad.exe", caminhoArquivo);
                    ViewBag.Mensagem = "O arquivo foi aberto no Bloco de Notas.";
                }
                else
                {
                    ViewBag.Mensagem = "O arquivo não foi encontrado.";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Mensagem = $"Ocorreu um erro ao abrir o arquivo: {ex.Message}";
            }

            return View("Index");
        }
    }
}