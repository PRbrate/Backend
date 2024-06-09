namespace OngTDE.BackEnd.Dtos
{
    public class UserDto
    {
        public UserDto(int id, string nome, string telefone, string email, string role)
        {
            Id = id;
            Nome = nome;
            Telefone = telefone;
            Email = email;
            Role = role;
        }

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public string Password { get; } = string.Empty;
        public string Role { get; set; }
    }
}
