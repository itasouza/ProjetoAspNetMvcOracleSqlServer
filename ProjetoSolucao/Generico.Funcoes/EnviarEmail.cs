using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Generico.Funcoes
{
    public class EnviarEmail
    {

        public string Email(string nome, string email, string mensagem)
        {

            System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient();
            client.Host = "mail.top3host.com.br";
            client.EnableSsl = false;
            client.Credentials = new System.Net.NetworkCredential("Contatos@tasdigital.com.br", "tascontato123##");
            MailMessage mail = new MailMessage();
            mail.Sender = new System.Net.Mail.MailAddress("Contatos@tasdigital.com.br", "Mensagem Enviada Pelo Site Piccnurse");
            mail.From = new MailAddress("Contatos@tasdigital.com.br", "Mensagem Enviada Pelo Site Piccnurse");
            mail.To.Add(new MailAddress("itasouza@yahoo.com.br", "Mensagem Enviada Pelo Site Piccnurse"));
            mail.CC.Add("itasouza@yahoo.com.br");
            mail.Subject = "Recuperação de Senha do site!";
            mail.Body = mensagem;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                return "Ocorreram problemas no envio do e-mail. Erro = " + ex.Message;
            }
            finally
            {
                mail = null;
            }

            return "OK";
        }



    }
}
