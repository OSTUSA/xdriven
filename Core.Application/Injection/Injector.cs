using System;
using System.Collections.Generic;


namespace Core.Application.Injection
{
	public class Injector
	{
		static object _locker = new object ();
		static Injector _instance;

		private Injector ()
		{
			Services = new Dictionary<Type, Lazy<object>> ();
		}

		private Dictionary<Type, Lazy<object>> Services { get; set; }

		private static Injector Instance {
			get {
				lock (_locker) {
					if (_instance == null)
						_instance = new Injector ();
					return _instance;
				}
			}
		}

		public static void Register<T> (T service)
		{
			Instance.Services [typeof(T)] = new Lazy<object> (() => service);
		}

		public static void Register<T> ()
			where T : new()
		{
			Instance.Services [typeof(T)] = new Lazy<object> (() => new T ());
		}

		public static void Register<T> (Func<object> function)
		{
			Instance.Services [typeof(T)] = new Lazy<object> (function);
		}

		public static T Resolve<T> ()
		{
			Lazy<object> service;
			if (Instance.Services.TryGetValue (typeof(T), out service)) {
				return (T)service.Value;
			} else {
				throw new KeyNotFoundException (string.Format ("Implementation not found for type '{0}'", typeof(T)));
			}
		}
	}
}