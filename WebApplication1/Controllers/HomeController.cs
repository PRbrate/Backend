using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using OngTDE.BackEnd.Dtos;
using OngTDE.BackEnd.Models;
using OngTDE.BackEnd.Repositories.Inteface;
using OngTDE.BackEnd.Services;
using System.Text.RegularExpressions;

namespace OngTDE.BackEnd.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
 
        private readonly IUserRepository _repository;

        public HomeController(IUserRepository repository)
        {
            _repository = repository;
            
        }


        [HttpGet("todosUsuarios")]
        [Authorize(Roles = "Admin")]
        public async Task<List<UserDto>> GetUsers()
        {
            var users = await _repository.GetAllUsers();
            return users;
        }

        [HttpPost]
        [Route("login")]
        [AllowAnonymous]
        public async Task<ActionResult<dynamic>> Authenticate(string email, string password)
        {
            var usuario = new User { email = email, Password = password };


            User user = await _repository.UserAuthenticator(usuario);



            if (user == null)
            {
                return NotFound(new { message = "Usuário ou Senha invalidos" });
            }

            var token = TokenService.GenerateToken(user);
            user.Password = "";
            return new
            {
                user = user,
                token = token
            };
        }

        [HttpPost]
        [Route("Cadastro")]
        [AllowAnonymous]
        public async Task<UserDto> Cadastro(User user)
        {

            if (!string.Equals(user.Roles, "Admin"))
            {
                user.Roles = "usuario";
            }            
            if (!System.Text.RegularExpressions.Regex.IsMatch(user.email, "^([0-9a-zA-Z]([-.\\w]*[0-9a-zA-Z])*@([0-9a-zA-Z][-\\w]*[0-9a-zA-Z]\\.)+[a-zA-Z]{2,9})$"))
            {
               throw new Exception("e-mail inválido, por favor colocar um e-mail válido");
            }
            var usuario = await _repository.CreateUser(user);
            return new UserDto(usuario.Id, usuario.Nome, usuario.email, usuario.Telefone, usuario.Roles);
        }

        [HttpPost]
        [Route("Deletar")]
        [Authorize(Roles ="Admin")]
        public async Task<IActionResult> Delete([FromQuery]string? email, int? id)
        {
            User user;

            if (id == null) 
            {
                 user = await _repository.DeleteUser(email);

                if (user == null)
                {
                    BadRequest("usuário não encontrato");
                }

                return Ok($"Usuário deletado com Sucesso = {user.email}");
            }

            user = await _repository.DeleteById(id);
            
            if (user == null) 
            {
                BadRequest("usuário não encontrato");
            }
            return Ok($"Usuário deletado com Sucesso{user.email}" );

        }

        [HttpPut]
        [Route("Atualizar")]
        [Authorize]
        public async Task<User> AtualizaUser(User user)
        {
            var usuario = await _repository.UpdateUser(user);

            return usuario;
        }

    }
}