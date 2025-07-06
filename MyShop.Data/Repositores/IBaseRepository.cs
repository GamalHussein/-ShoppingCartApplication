using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyShop.Entities.Repositores
{
	public interface IBaseRepository<T> where T : class
	{
		IEnumerable<T> GetAll(Expression<Func<T,bool>>?Predicate = null,string? IncludeWord = null);
		T GetById(int id);
		T GetFirstOrDefult(Expression<Func<T, bool>>? Predicate=null, string? IncludeWord = null);

		void Add(T entity);
		void Remove(T entity);
		void RemoveRange(IEnumerable<T> entities);
	}
	
	
}
