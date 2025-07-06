using Microsoft.EntityFrameworkCore;
using MyShop.DataAccess.Data;
using MyShop.Entities.Repositores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.DataAccess.Implementation
{
	public class BaseRepository<T> : IBaseRepository<T> where T : class
	{
		private readonly ApplicationDbContext _context;
		private readonly DbSet<T> _dbSet;
		public BaseRepository(ApplicationDbContext context)
		{
			_context = context;
			_dbSet = _context.Set<T>();

		}

		public IEnumerable<T> GetAll(Expression<Func<T, bool>>? Predicate, string? IncludeWord)
		{
			IQueryable<T> query = _dbSet;
			if (Predicate!=null)
			{
				query = query.Where(Predicate);
			}
			if (!string.IsNullOrEmpty(IncludeWord))
			{
				foreach (var includeProperty in IncludeWord.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty.Trim());
				}
				
			}
			return query.ToList();
		}

		public T GetById(int id)
		{
			return _dbSet.Find(id);
		}

		public T GetFirstOrDefult(Expression<Func<T, bool>>? Predicate=null, string? IncludeWord=null)
		{
			IQueryable<T> query = _dbSet;
			if (Predicate != null)
			{
				query = query.Where(Predicate);
			}
			if (!string.IsNullOrEmpty(IncludeWord))
			{
				foreach (var includeProperty in IncludeWord.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
				{
					query = query.Include(includeProperty.Trim());
				}

			}
			return query.SingleOrDefault();
		}
		public void Add(T entity)
		{
			_dbSet.Add(entity);
		}

		public void Remove(T entity)
		{
			_dbSet.Remove(entity);
		}

		public void RemoveRange(IEnumerable<T> entities)
		{
			throw new NotImplementedException();
		}
	}
}
