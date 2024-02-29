namespace ReadyTechTeaPotRESTAPI.Util
{
    public class SystemTime : IDateProvider
    {
        public DateTime Now()
        {
            return DateTime.Now;
        }

    }
}
