namespace TMS.Core.Endpoints;

public class Location
{
    public static class LocationGroup
    {
        public const string Group = "location";
    }
    
    public const string GetCountries = "countries";
    
    public const string GetStates = "states/{country}";
    
    public const string GetCities = "cities/{state}";
}