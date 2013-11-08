using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Infrastructure.SQLite;
using Core.Domain.Models;
using Core.Application.Services;
using Core.Domain.Extensions;


namespace Infrastructure.SQLite.Repositories
{
	public class SQLiteRepository<T> : IRepository<T> where T : ModelBase<T>, new()
	{
		public SQLiteRepository (IConfigService configService) {

		}

		public virtual string DatabasePath
		{
			get 
			{
				string documentsPath = Environment.GetFolderPath (Environment.SpecialFolder.Personal); 
				string libraryPath = Path.Combine (documentsPath, "../Library/");
				var path = Path.Combine (libraryPath, "database.db3");

				return path;
			}
		}

		public SQLiteRepository ()
		{

		}

		/// <summary>
		/// Get database connection. Also ensures database schema is setup.
		/// </summary>
		protected SQLiteConnection GetConnection() 
		{
			SQLiteConnection returnValue = new SQLiteConnection(DatabasePath);

			returnValue.CreateTable<T> ();

			return returnValue;
		}

		public T GetByID(int id) {
			using (var connection = GetConnection()) {
				return connection.Get<T> (id);
			}
		}

		public void Save(T item) {
			if (item == null)
				throw new ArgumentException ("item");

			if (item.IsNew()) {
				Insert (item);
			} else {
				Update (item);
			}
		}

		private void Update(T item) {
			if (item == default(T))
				throw new ArgumentException ("item");

			using (var connection = GetConnection()) {
				connection.Update (item);
			}
		}

		private void Insert(T item) {
			if (item == default(T))
				throw new ArgumentException ("item");

			using (var connection = GetConnection()) {
				connection.Insert (item);
			}
		}

		public void SaveAll(List<T> items) {
			if (items == null || !items.Any())
				throw new ArgumentException ("items");

			if (items.Any (x => x.IsNew()))
				InsertAll (items.Where (x => x.IsNew()).ToList ());

			if (items.Any (x => !x.IsNew()))
				UpdateAll (items.Where (x => !x.IsNew()).ToList ());
		}

		private void UpdateAll(List<T> items) {
			if (items == null || !items.Any())
				throw new ArgumentException ("items");

			using (var connection = GetConnection()) {
				connection.UpdateAll (items);
			}
		}

		private void InsertAll(List<T> items) {
			if (items == null || !items.Any())
				throw new ArgumentException ("items");

			using (var connection = GetConnection()) {
				connection.InsertAll (items);
			}
		}

		public void DeleteAll()
		{
			using (var connection = GetConnection()) {
				connection.DeleteAll<T>();
			}
		}

		public void Delete(int id)
		{
			using (var connection = GetConnection()) {
				var item = connection.Get<T> (id);

				if (item == null)
					throw new ApplicationException ("ID did not yeild an item.");

				connection.Delete (item);
			}
		}

		public void Delete(List<T> itemsToDelete)
		{
			if (itemsToDelete != null) {
				var ids = itemsToDelete.Select (x => x.Id).ToList ();

				string itemString = ids.ToArrayString ();

				using (var connection = GetConnection()) {
					connection.DeleteBatch<T> (itemString);
				}
			}
		}

		public List<T> GetAll()
		{
			using (var connection = GetConnection()) {
				return connection.Table<T> ().ToList ();
			}
		}

		public virtual T Get(Expression<Func<T, bool>> where)
		{
			using (var connection = GetConnection()) {
				return connection.Table<T>().Where(where).FirstOrDefault<T>();
			}
		}

		public virtual List<T> GetMany(Expression<Func<T, bool>> where)
		{
			using (var connection = GetConnection()) {
				return connection.Table<T>().Where(where).ToList();
			}
		}

		public virtual List<T> GetTop(Expression<Func<T, bool>> where, int numberOfRowsToReturn)
		{
			using (var connection = GetConnection()) {
				return connection.Table<T>().Where(where).OrderBy(x => x.Id).Take(numberOfRowsToReturn).ToList();
			}
		}
	}
}

