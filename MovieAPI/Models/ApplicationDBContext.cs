using Microsoft.EntityFrameworkCore;
using MovieAPI.Models;
namespace MovieAPI.Models
{
    public class ApplicationDBContext :DbContext
    {

        public ApplicationDBContext( DbContextOptions<ApplicationDBContext> options) :
            base(options)
        {


        }

      public  DbSet<MovieModel> Movies { get; set; }=null!;
    }
}
