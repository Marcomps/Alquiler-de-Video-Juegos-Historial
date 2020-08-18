using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace AlquilerDeVideoJuegos_History.Models
{
    public class MyDBContext : DbContext
    {
        public MyDBContext(DbContextOptions<MyDBContext> options) : base(options)
        {

        }

        public DbSet<VideoGame> VideoGame { get; set; }
        public DbSet<Client> Client { get; set; }
        public  DbSet<LoanHistory> LoanHistory { get; set; }
    }


}

//Creando las tablas
public class VideoGame
{
    public int Id { get; set; }
    public string TypeGame { get; set; }


}

public class Client
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string Adress { get; set; }
    public  string Telephone { get; set; }
    public  bool  IsActive { get; set; }
    public  DateTime CreationDate { get; set; }


    /***********************************Creando FK indicando que un cliente pueda tener muchos juegos asignados***************************************************************/
    public ICollection<LoanHistory> LoanHistories { get; set; }
}

public class LoanHistory
{
    public  int Id { get; set; }

    public  int LondDate { get; set; }
    public  DateTime LondDateTime { get; set; }

    public DateTime CreationDate { get; set; }

    /*****************************************Generando FK*********************************************************/
    //Para tener referencias de quien es el historial y el tipo de video juegos
    public int ClientId { get; set; }
    public int VideoGameId { get; set; }

    //Para tener una referencia a nivel de objetos

    public  VideoGame VideoGame { get; set; }
    public Client Client { get; set; }

    /**************************************************************************************************/
     
}