using System;
using System.Linq;
using Fakebook.Posts.DataAccess.Models;

namespace Fakebook.Posts.DataAccess.Mappers
{
    /// <summary>
    /// Mapper convert from data access layer models to domain models and back
    /// intended to be called only on posts but can be called on comments
    /// </summary>
    public static class DataMapper
    {
        public static Fakebook.Posts.Domain.Models.Post ToDomain(this Post post)
        {
            throw new NotImplementedException();
        }
        public static Fakebook.Posts.Domain.Models.Comment ToDomain(this Comment comment,
            Fakebook.Posts.Domain.Models.Post post)
        {
            throw new NotImplementedException();
        }

        public static Fakebook.Posts.Domain.Models.User ToDomain(this User user)
        {
            throw new NotImplementedException();
        }

        public static Post ToDataAccess(this Fakebook.Posts.Domain.Models.Post post)
        {
            throw new NotImplementedException();
        }
        public static Comment ToDataAccess(this Fakebook.Posts.Domain.Models.Comment comment, Post post)
        {
            throw new NotImplementedException();
        }
        public static User ToDataAccess(this Fakebook.Posts.Domain.Models.User user)
        {
            throw new NotImplementedException();
        }
    }
}
