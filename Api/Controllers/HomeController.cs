using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Api.Controllers
{
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet("/")]
        public ContentResult Home() {
            string teste = @"<!DOCTYPE HTML><html><head><meta http-equiv='Content-Type' content='text/html; charset=UTF-8' /><title>UsuarioAPI</title><script>const redirecionamento = () => {const url = window.location.href;window.location.href = `${url}swagger`;}</script><style>* {padding: 0;margin: 0;}body {width: 100%;height: 100%;background-color: #f5f5f5;text-align: center;}#cabecalho {width: 100%;height: 20%;padding: 10px 0px;}#direcionamento__texto,#direcionamento__botao,#rodape,.titulo,.descricao {font-family: Arial, Helvetica, sans-serif;font-weight: bold;font-size: 32px;}.descricao {color: grey;font-size: 16px;}#direcionamento {width: 100%;height: 70vh;display: flex;align-items: center;justify-content: center;flex-direction: column;}#direcionamento__texto {color: #38b832;font-size: 16px;}#direcionamento__botao {width: 100px;height: 40px;margin: 50px;display: flex;align-items: center;justify-content: center;background-color: #77e54b;border-radius: 5px;border: 2px solid darkgreen;cursor: pointer;color: darkgreen;font-size: 16px;transition-duration: 0.2s;}#direcionamento__botao:hover {background-color: darkgreen;border: 2px solid #77e54b;color: #77e54b;font-size: 16px;}#rodape {width: 100%;height: 10vh;font-size: 16px;}</style></head><body><header id='cabecalho'><section class='titulo'>USUARIO API</section><section class='descricao'>Autenticação e permissionamento de usuários</section></header><section id='direcionamento'><section id='direcionamento__texto'>Clique no botão abaixo para ser redirecionado para o Swagger da aplicação.</section><section id='direcionamento__botao' onClick='redirecionamento()'>Swagger</section></section><footer id='rodape'>Desenvolvido por Marcus Vinicius Ruas de Andrade - Mandrade</footer></body></html>";
            return base.Content(teste, "text/html");
        } 
    }
}