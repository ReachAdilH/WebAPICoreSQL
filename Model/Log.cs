namespace WebAPICoreSQL.Model
{
    public class Log
    {
        public int Id { get; set; }
        public Guid CorrelationID { get; set; }
        public string ApplicationName { get; set; }
        public string MethodName { get; set; }
        public string Message { get; set; }
        public string Exception { get; set; }
        public DateTime LoggedAt { get; set; } = DateTime.Now;
    }
}
