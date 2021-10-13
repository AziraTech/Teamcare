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
				new User() { Title=NameTitle.Mr,FirstName = "Teamcare", LastName = "Admin", Email = "hans.doyekee@gmail.com", UserRole = UserRoles.GlobalAdmin, IsActive = true},
				new User() { Title=NameTitle.Er,FirstName = "Nishidh", LastName = "Kagathara", Email = "nish.kagathara0791@gmail.com", UserRole = UserRoles.GlobalAdmin, IsActive = true}
				);
		}
	}
}
