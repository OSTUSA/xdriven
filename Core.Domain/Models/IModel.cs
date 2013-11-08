using System;

namespace Core.Domain.Models
{
	public interface IModel<T> : IModel where T : IModel<T>
	{
	}

	public interface IModel
	{
		bool IsNew();
	}
}

