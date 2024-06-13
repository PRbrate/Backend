using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OngTDE.BackEnd.Models
{
    public class User
    {
        //Entidade Usuário

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Telefone { get; set; }
        public string email { get; set; }
        public string Password { get; set; }
        public string Roles { get; set; }
    }
}
