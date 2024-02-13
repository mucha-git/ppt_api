using System.ComponentModel.DataAnnotations.Schema;

namespace WebApi.Entities;

public class Coordinates
{
    public int Id { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }

    //public int MapId { get; set; }
    //public Maps Map { get; set; }


}