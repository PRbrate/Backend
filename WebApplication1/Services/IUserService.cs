namespace OngTDE.BackEnd.Services
{
    public interface IUserService
    {
        bool ValidaEmail(string email);
        bool ValidaTelefone(string telefone);
        bool validaRole(string role);
    }
}
