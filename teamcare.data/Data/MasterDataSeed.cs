using Microsoft.EntityFrameworkCore;
using teamcare.common.Enumerations;
using teamcare.data.Entities;

namespace teamcare.data.Data
{
	public class MasterDataSeed
	{
		public static void SeedMasterData(ModelBuilder modelBuilder)
		{
			//Seeding Database with Master data
			modelBuilder.Entity<User>().HasData(
				new User() { FirstName = "Teamcare", LastName = "Admin", Email = "hans.doyekee@gmail.com", UserRole = UserRoles.GlobalAdmin },
				new User() { FirstName = "Nishidh", LastName = "Kagathara", Email = "nish.kagathara0791@gmail.com", UserRole = UserRoles.GlobalAdmin }
				);
		}
	}
}
