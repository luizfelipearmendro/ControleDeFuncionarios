namespace ControleDeFuncionarios.Helper
{
    public interface IEmail
    {
        bool Enviar(string para, string assunto, string mensagemCorpo);
    }
}
