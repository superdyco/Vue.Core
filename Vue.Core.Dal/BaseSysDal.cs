using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vue.Core.Data;
using Vue.Core.Data.Entities;

namespace Vue.Core.Dal
{
    public class BaseSysDal
    {
        private readonly ApplicationDbContext _db;

        public BaseSysDal(ApplicationDbContext db)
        {
            _db = db;
        }
        
          
        public string GenNav(int id)
        {
            var Appsid = _db.UsersRoles.Where(x => x.Users.Id == id).Select(x => new
            {
                Roles=x.Roles
            }).SelectMany(x=>x.Roles.RolesApps).Select((x=>x.Apps.Id)).ToList();
            
            
            var strBuilder = new StringBuilder();
            List<Apps> parentItems = (from a in _db.Apps where a.ParentId == null  select a).ToList();
            strBuilder.Append("{\"items\":[");
            foreach (var parentcat in parentItems)
            {
                strBuilder.Append($"{{\"icon\":\"{parentcat.IconClass}\",\"text\":\"{parentcat.AppName}\"");
                if (parentcat.RouteName!=null)
                    strBuilder.Append($",\"routename\":\"{parentcat.RouteName}\"");
                List<Apps> childItems = (from a in _db.Apps where a.ParentId == parentcat.Id select a).ToList();
                if (childItems.Count > 0)
                    AddChildMenuItem(parentcat, strBuilder,Appsid);
                strBuilder.Append("},");
            }
            strBuilder.Length--;
            strBuilder.Append("]}");
            return strBuilder.ToString();
        } 
        
        private void AddChildMenuItem(Apps childItem, StringBuilder strBuilder,List<int> Appsid)
        {
            List<Apps> childItems = (from a in _db.Apps where a.ParentId == childItem.Id  && Appsid.Contains(a.Id) select a).ToList();
            if (childItems.Any())
                strBuilder.Append(",\"children\":[");
            foreach (Apps cItem in childItems)
            {
                strBuilder.Append($"{{\"icon\":\"{cItem.IconClass}\",\"text\":\"{cItem.AppName}\",\"routename\":\"{cItem.RouteName}\"");
                List<Apps> subChilds = (from a in _db.Apps where a.ParentId == cItem.Id  && Appsid.Contains(a.Id) select a).ToList();
                if (subChilds.Count > 0)
                {
                    AddChildMenuItem(cItem, strBuilder,Appsid);
                }
                strBuilder.Append("},");
            }
            if (childItems.Any())
            {
                if (strBuilder.ToString().Substring(strBuilder.ToString().Length-1,1)==",")
                    strBuilder.Length--;            
                strBuilder.Append("]");
            }
        }
    }
}