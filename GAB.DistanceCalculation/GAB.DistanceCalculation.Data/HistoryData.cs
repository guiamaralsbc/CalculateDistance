using GAB.DistanceCalculation.Model;

namespace GAB.DistanceCalculation.Data
{
    public class HistoryData
    {
        DCContext dc = new DCContext();

        public bool InsertHistory(History entity)
        {
            try
            {
                dc.History.Add(entity);
                dc.SaveChanges();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }
    }
}
