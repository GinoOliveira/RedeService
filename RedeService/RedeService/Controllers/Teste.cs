using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text;

namespace RedeService.Controllers
{
    public class Teste : Controller
    {
        [HttpGet, Route("Home/Teste")]
        public ActionResult CriarJson()
        {
            List<clsTeste> lista = new List<clsTeste>();

            for (int i = 1; i <= 100; i++)
            {
                DateTime dataHoraAtual = DateTime.Now;
                string descricao = dataHoraAtual.ToString("yyyy/MM/dd HH:mm:ss.fff");

                clsTeste item = new clsTeste
                {
                    codigo = i,
                    descricao = descricao
                };

                lista.Add(item);
            }

            string json = JsonConvert.SerializeObject(lista);
            string pastaDestino = "C:\\Users\\Anonymous\\Desktop\\RedeService\\RedeService\\RedeService\\wwwroot\\Arquivos";
            string nomeArquivo = "data.json";
            string caminhoArquivo = Path.Combine(pastaDestino, nomeArquivo);
            System.IO.File.WriteAllText(caminhoArquivo, json, Encoding.UTF8);

            return Content("Arquivo JSON gerado e salvo com sucesso!");
        }
        [HttpGet, Route("Teste/LerArquivoJson")]
        public IActionResult LerArquivoJson()
        {
            try
            {
                string caminhoArquivo = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot\\Arquivos", "data.json");

                if (!System.IO.File.Exists(caminhoArquivo))
                {
                    return NotFound("O arquivo JSON não foi encontrado.");
                }

                string json = System.IO.File.ReadAllText(caminhoArquivo);
                return Json(json);
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro: {ex.Message}");
            }

        }
        [HttpGet, Route("Teste/BuscarImagem")]
        public async Task<IActionResult> BuscarImagem()
        {
            try
            {

                string imageUrl = "https://redeservice.com.br/wp-content/uploads/2020/07/redeservice-logo.png";


                using (HttpClient httpClient = new HttpClient())
                {

                    HttpResponseMessage response = await httpClient.GetAsync(new Uri(imageUrl));


                    if (response.IsSuccessStatusCode)
                    {

                        byte[] imageBytes = await response.Content.ReadAsByteArrayAsync();
                        string base64Image = Convert.ToBase64String(imageBytes);
                        return Content(base64Image);
                    }
                    else
                    {
                        return BadRequest("Falha ao buscar a imagem.");
                    }
                }
            }
            catch (Exception ex)
            {
                return BadRequest($"Ocorreu um erro: {ex.Message}");
            }
        }




    }
}
