using WebApiForController.DTOs;

namespace WebApiForController.Services
{
    public interface IUserServices
    {
        public GetUserResponse? GetUser(GetUserRequest request);
        public IEnumerable<GetAllUsersResponse> GetAllUsers();
        public IEnumerable<GetAllUsersResponse> InserUsers(InsertUserRequest request);
    }
}
