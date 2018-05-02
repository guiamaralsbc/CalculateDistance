using GAB.DistanceCalculation.Data;
using GAB.DistanceCalculation.Model;
using GeoCoordinatePortable;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GAB.DistanceCalculation.Business
{
    public class Calculate
    {
        FriendData fData;
        HistoryData hData;

        public List<Friend> ListCloser(string friendName)
        {
            try
            {
                fData = new FriendData();
                var friendList = fData.ListFriends();

                Friend principal = friendList.FirstOrDefault(x => x.Name == friendName);
                friendList.Remove(principal);
                friendList.ForEach(x => x.DistanceFromPrincipal = CalculateDistance(principal, x));

                // Insere o Histórico
                var history = new History()
                {
                    FriendId = principal.FriendId,
                    FriendsDistances = JsonConvert.SerializeObject(friendList)
                };
                InsertHistory(history);

                return friendList;
            }
            catch (Exception)
            {
                return null;
            }
        }

        private string CalculateDistance(Friend principal, Friend visit)
        {
            var locationA = new GeoCoordinate((double)principal.Latitude, (double)principal.Longitude);
            var locationB = new GeoCoordinate((double)visit.Latitude, (double)visit.Longitude);
            string distance = (locationA.GetDistanceTo(locationB) / 1000).ToString("N2");

            return distance;
        }

        private void InsertHistory(History entity)
        {
            hData = new HistoryData();
            hData.InsertHistory(entity);
        }
    }
}
