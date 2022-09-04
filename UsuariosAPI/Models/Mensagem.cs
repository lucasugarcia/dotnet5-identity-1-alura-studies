using MimeKit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UsuariosAPI.Models
{
    public class Mensagem
    {
        public List<MailboxAddress> Destinarios { get; set; }
        public string Assunto { get; set; }
        public string Conteudo { get; set; }

        public Mensagem(IEnumerable<string> destinarios, string assunto, int usuarioId, string codigo)
        {
            Destinarios = new List<MailboxAddress>();
            Destinarios.AddRange(destinarios.Select(d => MailboxAddress.Parse(d)));
            Assunto = assunto;
            this.Conteudo = $"http://localhost:6000/Ativar?UsuarioId={usuarioId}&CodigoDeAtivacao={codigo}";
        }
    }
}
