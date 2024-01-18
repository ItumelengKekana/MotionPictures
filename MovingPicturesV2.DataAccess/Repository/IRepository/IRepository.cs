using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MovingPicturesV2.DataAccess.Repository.IRepository
{
	public interface IRepository<T> where T : class
	{
		//T - Category

		T GetFirstOrDefault(Expression<Func<T, bool>> filter, string? includeProperties = null, bool tracked = true); //receives our class (Category/T) and returns a boolean (whether the record was found or not)
		IEnumerable<T> GetAll(Expression<Func<T, bool>>? filter=null, string? includeProperties = null);

		void Add(T entity);
		void Remove(T entity); //receiving and removing one entity
		void RemoveRange(IEnumerable<T> entity); //receiving and removing more than one entity
	}
}
