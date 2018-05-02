using System.ComponentModel.DataAnnotations.Schema;

namespace GAB.DistanceCalculation.Model
{
    [Table("Friends")]
    public class Friend
    {        
        public int FriendId { get; set; }
        public string Name { get; set; }
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }

        [NotMapped]
        public string DistanceFromPrincipal { get; set; }
    }
}
