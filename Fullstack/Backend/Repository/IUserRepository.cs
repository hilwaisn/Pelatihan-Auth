using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Backend.Models;

namespace Backend.Repository{
    public interface IUserRepository{
        User Create (User user);
        User GetByEmail (string email);
        User GetById(int id);
    }
}