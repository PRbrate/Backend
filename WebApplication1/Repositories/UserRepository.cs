using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using OngTDE.BackEnd.Dtos;
using OngTDE.BackEnd.Models;
using OngTDE.BackEnd.Repositories.Inteface;

namespace OngTDE.BackEnd.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly DataBaseContext _dbcontext;

        public UserRepository(DataBaseContext dbcontext)
        {
            _dbcontext = dbcontext;
        }

       //Criando usuário 
        public async Task<User> CreateUser(User user)
        {
            var contains = await GetByEmail(user.email);
            _dbcontext.Usuarios.Add(user);

            if (contains != null)
            {
                throw new Exception("Já contem um usuário com o e-mail digitado, por favor alterar o e-mail");
            }
            await _dbcontext.SaveChangesAsync();

            return user;
        }


        //retornando todos os usuários cadastrados
        public async Task<List<UserDto>> GetAllUsers()
        {

            List<User> user = await _dbcontext.Usuarios.ToListAsync();

            List<UserDto> users = new List<UserDto>();

            foreach (User i in user)
            {
                var userDto = new UserDto(i.Id, i.Nome, i.Telefone, i.email, i.Roles);

                users.Add(userDto);
            }

            return users;
        }


        //pegando usuário pelo e-mail
        public async Task<User> GetByEmail(string email)
        {
            var users = await _dbcontext.Usuarios.FirstOrDefaultAsync(x => x.email.ToLower() == email.ToLower());
            return users;
        }

        //pegando usuário pelo Id
        public async Task<User> GetById(int? id)
        {
            var user = await _dbcontext.Usuarios.FindAsync(id);

            return user;
        }

        //Atualizando Usuário
        public async Task<User> UpdateUser(User user)
        {
            var userBanco = await GetByEmail(user.email);

            if (userBanco != null)
            {
                _dbcontext.Usuarios.Update(userBanco);
                await _dbcontext.SaveChangesAsync();
                return userBanco;
            }
            throw new Exception("Usuário não encontrado para atualização");
            
        }

        //Excluindo Usuário pelo e-mail
        public async Task<User> DeleteUser(string email)
        {
            try
            {
                var user = await GetByEmail(email);

                var remove = _dbcontext.Usuarios.Remove(user);
                await _dbcontext.SaveChangesAsync();
                return user;
            }
            catch (Exception) 
            { 
                throw new Exception("Usuário não Encontrado, não pôde ser removido!");
            }

            
        }

        //Excluindo Usuário pelo Id
        public async Task<User> DeleteById(int? id)
        {
            try { 
            var user = await GetById(id) ;
            var remove = _dbcontext.Usuarios.Remove(user);
            await _dbcontext.SaveChangesAsync();
            return user;
            }
            catch (Exception) 
            {
                throw new Exception("Usuário não Encontrado, não pôde ser removido!");
            }
        }

       
        //authenticator
        public async Task<User> UserAuthenticator(User user)
        {

            var users = await _dbcontext.Usuarios.FirstOrDefaultAsync(x => x.email == user.email && x.Password == user.Password);

            return users;
        }
    }
}
