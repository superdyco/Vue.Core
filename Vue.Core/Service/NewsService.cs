using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using Microsoft.EntityFrameworkCore;
using Vue.Core.Common;
using Vue.Core.Data;
using Vue.Core.Data.Entities;
using Vue.Core.Model;
using Vue.Core.Service.Fitlers;
using Vue.Core.Service.Interface;

namespace Vue.Core.Service
{
    public class NewsService : INewsService<News>
    {
        private ApplicationDbContext _db;
        public NewsService(ApplicationDbContext db) => _db = db;

        public PagingModel<News> GetAll(Fitlers.NewsFilter filter)
        {
            var query = filter.Query(_db.News.Where(x=>!x.IsDeleted));
            return query;
        }

        public News GetById(int id)
        {
            return _db.News.Find(id);
        }
        
        public News GetByGId(Guid Gid)
        {
            return _db.News.FirstOrDefault(x => x.Gid==Gid);
        }

        public News Create(News news)
        {
            news.Id = default;
            _db.News.Add(news);
            _db.SaveChanges();

            return news;
        }

        public void Update(News t)
        {
            var u = _db.News.FirstOrDefault(x => x.Gid == t.Gid);
            if (u != null)
            {
                u.Content = t.Content;
                u.Title = t.Title;
                u.StartDate = t.StartDate;
                u.EndDate = t.EndDate;
                _db.SaveChanges();
            }
        }

        public void Delete(int it)
        {
            throw new System.NotImplementedException();
        }
    }
}