using Generico.Aplicacao;
using Generico.Dominio;
using Generico.Funcoes;
using System;
using System.Web.Mvc;

namespace ProjetoWebSite.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(TB_USUARIO tabela)
        {

            // - aqui vai pesquisar o login é senha
            try
            {
                var tbuscar = new UsuarioAplicacao();
                var validadados = new ValidaCaracter();
                var retorno = tbuscar.ListarPorLoginSenha(validadados.ValidaDados(tabela.LoginUsuario), validadados.ValidaDados(tabela.SenhaUsuario));

                if (retorno != null)
                {

                    Session["idusuario"] = retorno.IdUsuario;
                    Session["NomeUsuario"] = retorno.NomeUsuario;
                    Session["Administrador"] = retorno.UsuarioAdmin;
                    Session["EmailUsuario"] = retorno.EmailUsuario;


                    //redireciona para o painel de controle
                    return RedirectToAction("index", "PaginaInicial");
                    //exemplo  return RedirectToAction("Index", "Home", new { id = @Session["idpossibilidadde"] });
                }
                else
                {
                    TempData["erro"] = "...Ops.. acredito que você tenha errado a senha/login, tente novamente, ou use a opção recuperar a sua senha!";
                }


            }
            catch (Exception ex)
            {
                TempData["erro"] = ex.ToString(); //  "...Ops.. acredito que você tenha errado a senha/login, tente novamente, ou use a opção recuperar a sua senha!";
            }

            ModelState.Clear();
            return RedirectToAction("index", "Home");
        }

        [HttpPost]
        public ActionResult RecuperaSenha(TB_USUARIO tabela)
        {
            try
            {
                var tbuscar = new UsuarioAplicacao();
                var validadados = new ValidaCaracter();
                var retorno = tbuscar.ListarPorEmailLoginSenha(validadados.ValidaDados(tabela.EmailUsuario));

                if (retorno != null)
                {

                    string NomeContato = retorno.NomeUsuario;
                    string EmailContato = retorno.EmailUsuario;
                    string Login = retorno.LoginUsuario;
                    string Senha = retorno.SenhaUsuario;

                    var enviaremail = new EnviarEmail();
                    string Mensagem = " Mensagem do site:<br/> Nome:  " + NomeContato + "<br/> Login : " + Login + " <br/> Senha : " + Senha;
                    string resposta = enviaremail.Email(Mensagem, NomeContato, EmailContato);

                    if (resposta == "OK")
                    {
                        TempData["msg"] = "E-mail enviado com sucesso!";
                    }
                    else
                    {
                        TempData["erro"] = resposta;
                    }

                    return RedirectToAction("index", "Home");
                }
                else
                {
                    TempData["erro"] = "Não foi possivel o envio da senha!";
                }


            }
            catch (Exception)
            {
                TempData["erro"] = "Não foi possivel o envio da senha!";
            }

            return RedirectToAction("index", "Home");
        }
    }
}