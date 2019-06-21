using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;


namespace REPO.Interfaces
{
    public interface IRepository<T> where T : class
    {
         T Find(object id);
         IEnumerable<T> Where(Expression<Func<T, bool>> Filter = null);

         bool RemoveById(object id);

         bool Remove(T entity);

         int Remove(Expression<Func<T, bool>> Filter = null);
         int RemoveRange(Expression<Func<T, bool>> Filter = null);
         T RemoveByIdandGet(object id);

         bool Update(T entity);
         bool updateRange();
         bool Add(T entity, Expression<Func<T, bool>> InsertControl = null);

         List<T> ToList();


         int CountByWhere(Expression<Func<T, bool>> Filter = null);
         T FirstOrDefault(Expression<Func<T, bool>> Filter = null);



         List<T> SqlGetModelList(string query = "");

         object SqlGetList(string query = "");

         int ExecuteSqlCommand(string query = "");





    }
}
