using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Vue.Core.Extensions;

namespace Vue.Core.Service.Fitlers
{
    using M = Data.Entities.News;

    public class NewsFilter
    {
        public string[] sortBy { get; set; }
        public bool[] sortDesc { get; set; }
        public int currentPage { get; set; }
        public int pageSize { get; set; }
        public string keyword { get; set; }

        public Model.PagingModel<M> Query(IQueryable<M> query)
        {
            var data = query;

            if (!string.IsNullOrWhiteSpace(keyword))
            {
                keyword = keyword.ToLower();
                data = data.Where(i => EF.Functions.Like(i.Title.ToLower(),($"%{keyword}%"))
                                    || EF.Functions.Like(i.Content.ToLower(),($"%{keyword}%"))
                );
            }

            return SortPaging(data);
        }

        private Model.PagingModel<M> SortPaging(IQueryable<M> q)
        {
            if (sortBy.Length > 0)
            {
                q = sortDesc[0] ? q.OrderByDescending(sortBy[0]) : q.OrderBy(sortBy[0]);
            }
            else
            {
                q = q.OrderByDescending(a => a.CreatedAt);
            }

            return new Model.PagingModel<M>(q.Skip((this.currentPage - 1) * this.pageSize).Take(this.pageSize)
                , q.Count());
        }
    }
}