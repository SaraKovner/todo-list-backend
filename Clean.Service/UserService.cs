using AutoMapper;
using Clean.Core.DTOs;
using Clean.Core.Entities;
using Clean.Core.Repositories;
using Clean.Core.Services;

namespace Clean.Service
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public List<UserResponseDTO> GetUsers()
        {
            var users = _userRepository.GetUsers();
            return _mapper.Map<List<UserResponseDTO>>(users);
        }

        public UserResponseDTO? GetById(int id)
        {
            var user = _userRepository.GetById(id);
            return user != null ? _mapper.Map<UserResponseDTO>(user) : null;
        }

        public UserResponseDTO? GetByUsername(string username)
        {
            var user = _userRepository.GetByUsername(username);
            return user != null ? _mapper.Map<UserResponseDTO>(user) : null;
        }

        public User? GetByUsernameForAuth(string username)
        {
            return _userRepository.GetByUsername(username);
        }

        public int? GetUserIdByUsername(string username)
        {
            var user = _userRepository.GetByUsername(username);
            return user?.Id;
        }

        public UserResponseDTO Add(RegisterUserDTO registerUserDto)
        {
            var user = _mapper.Map<User>(registerUserDto);
            var newUser = _userRepository.Add(user);
            return _mapper.Map<UserResponseDTO>(newUser);
        }

        public UserResponseDTO Update(UserResponseDTO userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var updatedUser = _userRepository.Update(user);
            return _mapper.Map<UserResponseDTO>(updatedUser);
        }

        public void Delete(int id)
        {
            _userRepository.Delete(id);
        }
    }
}