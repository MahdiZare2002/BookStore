using DataAccess.Models;
using DataAccess.Repositories.AuthorRepo;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Services.AuthorService
{
    public class AuthorService
    {
        private readonly AuthorRepository _repository;
        public AuthorService(AuthorRepository authorRepository) 
        {
            _repository = authorRepository;
        }

        public async Task<IEnumerable<Author>> GetAllAuthors()
        {
            return await _repository.GetAll();
        }

        public async Task CreateAuthor(Author author)
        {
            await _repository.Create(author);
        }
    }
}
