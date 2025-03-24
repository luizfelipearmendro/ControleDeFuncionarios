using System.Net;
using System.Net.Mail;
using System.Reflection.Metadata.Ecma335;
using MimeKit;

namespace ControleDeFuncionarios.Helper
{
    public class Email : IEmail
    {
        private readonly IConfiguration _configuration;

        public Email(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public bool Enviar(string para, string assunto, string mensagemCorpo)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("TchaWithBack", "luizarmendro@gmail.com"));
            message.To.Add(new MailboxAddress("luizarmendro@gmail.com", para));
            message.Subject = assunto;

            var bodyBuilder = new BodyBuilder
            {
                HtmlBody = $@"
                <html>
                    <body>
                        <img src='cid:logo' alt='Logo da MS Tendas' width='150' height='90' />
                        <p>{mensagemCorpo}</p>
                    </body>
                </html>"
            };

            var logoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "imagem_2024-06-07_142248480-removebg-preview (1).png");

            bodyBuilder.Attachments.Add(logoPath, new ContentType("image", "png") { Name = "imagem_2024-06-07_142248480-removebg-preview (1).png" })
                         .ContentId = "logo";

            message.Body = bodyBuilder.ToMessageBody();

            using (var client = new MailKit.Net.Smtp.SmtpClient())
            {
                try
                {
                    client.Connect("smtp.gmail.com", 587, MailKit.Security.SecureSocketOptions.StartTls);
                    client.Authenticate("luizarmendro@gmail.com", "gvgc hdiq ivuq fdso");
                    client.Send(message);
                    client.Disconnect(true);
                    return true; // Retorna true se o envio for bem-sucedido
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Erro ao enviar e-mail: " + ex.Message);
                    return false; // Retorna false se houver erro
                }
            }
        }
    }
}
