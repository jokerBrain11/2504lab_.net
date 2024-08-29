using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using System;
using WebApiForController.DTOs;
using WebApiForController.Models;

namespace WebApiForController.Services
{
    public class UserServices : IUserServices
    {
        private static List<User> _user = new List<User>();
        public GetUserResponse? GetUser(GetUserRequest request)
        {
            if (request == null)
            {
                return null;
            }
            var user = _user.FirstOrDefault(u => u.Id == request.Id);
            if (user == null) return null;

            var result = new GetUserResponse() {
                Name = user.Name,
                Email = user.Email,
                Age = user.Age,
                // 將DateTime的資料格式轉乘YYYY/MM/DD
                Birthday = user.Birthday.ToString("d"),
            };
            return result;
        }

        public IEnumerable<GetAllUsersResponse> GetAllUsers()
        {
            var result = _user.Select(u => new GetAllUsersResponse()
            {
                Id = u.Id,
                Name = u.Name,
                Age = u.Age,
                Birthday = u.Birthday.ToString("yyyy/MM/dd"),
                Email = u.Email,
            });
            return result;
        }

        public IEnumerable<GetAllUsersResponse> InserUsers(InsertUserRequest request)
        {
            if (request == null)
            {
                return null;
            }

            var user = new User()
            {
                Id = _user.Count,
                Name = request.Name,
                Email = request.Email,
                Age = request.Age,
                Birthday = DateTime.Parse(request.Birthday),
            };

            _user.Add(user);

            var result = _user.Select(u => new GetAllUsersResponse()
            {
                Id = u.Id,
                Name = u.Name,
                Age = u.Age,
                Birthday = u.Birthday.ToString("yyyy/MM/dd"),
                Email = u.Email,
            });
            return result;
        }
    }
}
