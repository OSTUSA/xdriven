using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace Core.Domain.Models
{
	public interface IRepository<T> where T : ModelBase<T>, new()
	{
		void Save (T item);
		void SaveAll (List<T> items);
		void Delete (int id);
		void Delete (List<T> itemsToDelete);

		List<T> GetAll ();
		T GetByID (int id);
		T Get (Expression<Func<T, bool>> where);
		List<T> GetMany (Expression<Func<T, bool>> where);
		List<T> GetTop (Expression<Func<T, bool>> where, int numberOfRowsToReturn);
	}
}

