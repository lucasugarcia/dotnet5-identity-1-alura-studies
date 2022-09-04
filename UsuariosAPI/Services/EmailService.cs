using MailKit.Net.Smtp;
using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using UsuariosAPI.Models;

namespace UsuariosAPI.Services
{
    public class EmailService
    {
        public void EnviarEmail(string[] destinarios, string assunto, int usuarioId, string code)
        {
            Mensagem mensagem = new Mensagem(destinarios, assunto, usuarioId, code);

            var mensagemEmail = CriaCorpoDoEmail(mensagem);

            Enviar(mensagemEmail);
        }

        private void Enviar(MimeMessage mensagemEmail)
        {
            using(var client = new SmtpClient())
            {
                try
                {
                    client.Connect("Conexão a ser realizada com provedor de e-mail");

                    //TODO: Autenticação de email

                    client.Send(mensagemEmail);
                }
                catch 
                {
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        private MimeMessage CriaCorpoDoEmail(Mensagem mensagem)
        {
            var mensagemDeEmail = new MimeMessage();

            mensagemDeEmail.From.Add(MailboxAddress.Parse("ADICIONAR O REMETENTE"));
            mensagemDeEmail.To.AddRange(mensagem.Destinarios);
            mensagemDeEmail.Subject = mensagem.Assunto;
            mensagemDeEmail.Body = new TextPart(MimeKit.Text.TextFormat.Text)
            {
                Text = mensagem.Conteudo
            };

            return mensagemDeEmail;
        }
    }
}
