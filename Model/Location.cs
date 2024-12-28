namespace WebAPICoreSQL.Model
{
    public struct Location
    {

        public string City { get; set; }
        public string State { get; set; }

        public Location(string city, string state)
        {

        City = city; State = state; 
        }

        public override string ToString() {
            return $"{City} : {State}";
                }

    }
}
