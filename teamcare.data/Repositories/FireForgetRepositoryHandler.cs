using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using teamcare.data.Entities;

namespace teamcare.data.Repositories
{
	public class FireForgetRepositoryHandler<T> : IFireForgetRepositoryHandler<T> where T : BaseEntity
    {
		private readonly IServiceScopeFactory _serviceScopeFactory;
		public FireForgetRepositoryHandler(IServiceScopeFactory serviceScopeFactory)
		{
			_serviceScopeFactory = serviceScopeFactory;
		}

        public void Execute(Func<IRepository<T>, Task> databaseWork)
        {
            // Fire off the task, but don't await the result
            Task.Run(async () =>
            {
                // Exceptions must be caught.
                try
                {
                    using var scope = _serviceScopeFactory.CreateScope();
                    var repository = scope.ServiceProvider.GetRequiredService<IRepository<T>>();
                    await databaseWork(repository);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            });
        }
    }
}
