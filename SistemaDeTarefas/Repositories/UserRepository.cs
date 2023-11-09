using Microsoft.EntityFrameworkCore;
using SistemaDeTarefas.Data;
using SistemaDeTarefas.Models;
using SistemaDeTarefas.Repositories.Interfaces;

namespace SistemaDeTarefas.Repositories
{
    public class UserRepository : IUserRespository
    {
        private readonly SistemaDeTarefasDBContext _dbContext;
        public UserRepository(SistemaDeTarefasDBContext sistemaDeTarefasDBContext)
        {
            _dbContext = sistemaDeTarefasDBContext;
            
        }
        public async Task<UserModel> Add(UserModel user)
        {
            await _dbContext.Users.AddAsync(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }

        public async Task<List<UserModel>> GetAllUsers()
        {
            return await _dbContext.Users.ToListAsync();
        }

        public async Task<UserModel> GetUserById(int id)
        {
            return await _dbContext.Users.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<UserModel> Update(UserModel user, int id)
        {
            UserModel userDb = await GetUserById(id);

            if(userDb == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            user.Id = id;

            _dbContext.Users.Update(user);
            await _dbContext.SaveChangesAsync();

            return user;
        }
        public async Task<bool> Delete(int id)
        {
            UserModel userDb = await GetUserById(id);

            if(userDb == null)
            {
                throw new Exception($"Usuário para o ID: {id} não foi encontrado no banco de dados.");
            }

            _dbContext.Users.Remove(userDb);
            await _dbContext.SaveChangesAsync();

            return true;
        }
    }
}
