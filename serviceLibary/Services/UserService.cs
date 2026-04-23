using DAL.Repositories;
using serviceLibary.Models;
using System.Collections.Generic;

namespace serviceLibary.Services
{
    public class UserService
    {
        private readonly IUserRepository _repository;

        public UserService(IUserRepository repository)
        {
            _repository = repository;
        }

        public UserModel? GetById(int id)
        {
            var dto = _repository.GetById(id);
            if (dto == null)
                return null;
            return new UserModel(dto.Id, dto.Naam);
        }
    }
}