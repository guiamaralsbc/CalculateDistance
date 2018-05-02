using GAB.DistanceCalculation.Model;
using System.Collections.Generic;
using System.Linq;

namespace GAB.DistanceCalculation.Data
{
    public class FriendData
    {
        DCContext dc = new DCContext();

        public List<Friend> ListFriends()
        {
            return dc.Friend.ToList();            
        }
    }
}
