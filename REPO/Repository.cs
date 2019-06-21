using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity;
using DAL;
using DAL.DAL;
using REPO.Interfaces;
using REPO;

namespace REPO
{
    public class Repository<T> : IRepository<T>  where T : class
    {
        /*      How To Use
          using (UnitOfWork work = new UnitOfWork())
            {
               work.Model1.find(5);
            }
        */


        private DBContextUsage context;

        private DbSet<T> dbset;

        public Repository(DBContextUsage db)
        {
            context = db;
            dbset = context.Set<T>();
        }

        
        public T Find(object id)
        {
            if (id == null)
                return null;
            return dbset.Find(id);
        }

      
        public  IEnumerable<T> Where(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter == null)
                return null;
            return dbset.Where(Filter).ToList();
           
        }
        
        public bool RemoveById(object id)
        {
            try
            {
                T entity = dbset.Find(id);
                dbset.Attach(entity);
                dbset.Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool Remove(T entity)
        {
            try
            {
                dbset.Remove(entity);
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public int Remove(Expression<Func<T, bool>> Filter = null)
        {
            try
            {
                T entity = dbset.FirstOrDefault(Filter);
                dbset.Attach(entity);
                dbset.Remove(entity);
                return context.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public int RemoveRange(Expression<Func<T, bool>> Filter = null)
        {
            try
            {
                if (Filter == null)
                    return 0;    
                dbset.RemoveRange(dbset.Where(Filter));
                return context.SaveChanges(); ;
            }
            catch (Exception)
            {
                return 0;
            }
        }
        public T RemoveByIdandGet(object id)
        {
            try
            {
                T entity = dbset.Find(id);
                dbset.Attach(entity);
                dbset.Remove(entity);
                context.SaveChanges();
                return entity;
            }
            catch (Exception)
            {
                return null;
            }
        }
      
        public bool Update(T entity)
        {
            try
            {
                dbset.Attach(entity);
                context.Entry(entity).State = EntityState.Modified;
                context.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    
         public bool  updateRange()
        {
            //foreach (var item in work.CityRepository.listByWhere(x => x.CityName == "Istanbul"))   Use this
            //{
            //    item.CityName = "New Istanbul";                   
            //    item.CityId = 34;
            //}
            //work.Save();
            return true;
        }

        /// <summary>
        /// Insert data also you can control database if data invalid.[ (usermodel,x=>x.UserName==textBoxUserName)] if username valid returned false..
        /// </summary>
        /// <param name="entity">Model Class</param>
        /// <param name="InsertControl">Database Data valid control</param>
        /// <returns>Bool</returns>
        public bool Add(T entity, Expression<Func<T, bool>> InsertControl = null)
        {
            try
            {
                if(InsertControl!=null)
                     if (dbset.FirstOrDefault(InsertControl) != null)
                          return false;
                dbset.Add(entity);
                context.SaveChanges();
                          return true;
               
            }
            catch (Exception)
            {
                return false;
            }

        }
       
        public List<T> ToList()
        {
                return dbset.ToList();
        }

      
     
        public int CountByWhere(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter == null)
                return 0;
            return dbset.Count(Filter);
        }

        public T FirstOrDefault(Expression<Func<T, bool>> Filter = null)
        {
            if (Filter == null)
                return dbset.FirstOrDefault();
            return dbset.FirstOrDefault(Filter);
        }
   
     
   
        public List<T> SqlGetModelList(string query = "")
        {
            try
            {
                if (query == "")
                    return null;
                List<T> list = context.Database.SqlQuery<T>(query).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public object SqlGetList(string query = "")
        {
            try
            {
                if (query == "")
                    return null;
                var list = context.Database.SqlQuery<object>(query).ToList();
                return list;
            }
            catch (Exception)
            {
                return null;
            }
        }

        public int ExecuteSqlCommand(string query = "")
        {
            try
            {
                if (query == "")
                    return 0;
                return context.Database.ExecuteSqlCommand(query);
            }
            catch (Exception)
            {
                return 0;
            }
        }

      
    }
}
