using Dapper;
using Employees.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Employees.Data
{
    public class UsersData : BaseRepo
    {
        readonly IPasswordHasher<User> _passwordHasher;
        public UsersData(IConfiguration config, IPasswordHasher<User> passwordHasher) : base(config)
        {
            _passwordHasher = passwordHasher;
        }
        public User CreateUser(User user)
        {
            var storedProcedure = "dbo.spInsUser";
            user.Password = _passwordHasher.HashPassword(user, user.Password);
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            parameters.Add("@FirstName", user.FirstName);
            parameters.Add("@LastName", user.LastName);
            parameters.Add("@Email", user.Email);
            parameters.Add("@PhoneNumber", user.PhoneNumber);
            parameters.Add("@Password", user.Password);
            var r = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            User usr = r.Read<User>().First();
            return usr;
        }
        public User UpdateUser(User user)
        {
            var storedProcedure = "dbo.spUpdUser";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            parameters.Add("@UserId", user.UserId);
            parameters.Add("@FirstName", user.FirstName);
            parameters.Add("@LastName", user.LastName);
            parameters.Add("@Email", user.Email);
            parameters.Add("@PhoneNumber", user.PhoneNumber);
            var r = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            User usr = r.Read<User>().First();
            return usr;
        }
        public bool DeleteUser(int userId)
        {
            var storedProcedure = "dbo.spDelUser";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            parameters.Add("@UserId", userId);
            var r = connection.Execute(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            bool result = r == 1;
            return result;
        }
        public User GetUser(int userId)
        {
            var storedProcedure = "dbo.spSelUser";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            parameters.Add("@UserId", userId);
            var r = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            User user = r.Read<User>().First();
            return user;
        }
        public List<User> GetUsersList()
        {
            var storedProcedure = "dbo.spSelUsersList";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            var userList = connection.Query<User>(storedProcedure, parameters, commandType: CommandType.StoredProcedure).ToList();

            return userList;
        }
        public User GetUserByEmail(string email)
        {
            User user = new();
            var storedProcedure = "dbo.spSelUserByEmail";
            using var connection = GetOpenConnection();
            DynamicParameters parameters = new();
            parameters.Add("@Email", email);
            var r = connection.QueryMultiple(storedProcedure, parameters, commandType: CommandType.StoredProcedure);
            return r?.Read<User>().First();
        }
    }
}
