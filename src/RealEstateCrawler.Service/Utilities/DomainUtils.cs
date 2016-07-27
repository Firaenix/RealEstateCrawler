namespace RealEstateCrawler.Service.Utilities
{
    public static class DomainUtils
    {
        /// <summary>
        /// - Positive look behind for single space
        /// - Find No = 13, 13A 13/27, etc.
        /// - Anything after until a comma followed by a space is a Street
        /// - Always followed by Suburb, State, Postcode
        /// </summary>
        public static string AddressRegex = @"(?i)(?<=\s)(?<No>(\d)+([A-Z])?(\/((\d)+([A-Z])?))?)\s*(?<Street>.+),\s*(?<Suburb>.+),\s*(?<State>.+)\s*(&n\s*bsp;)(?<PostCode>\w+)\s+";
        public static string PriceRegex = @"(?<Price>\$.+)\s*";
        public static string BedRegex = @"(?<Bed>\w+)\s+beds";
        public static string BathRegex = @"(?<Bath>\w+)\s+bathrooms";
        public static string ParkingRegex = @"(?<Park>\d+)\s*parking";
        public static string LinkRegex = @"href='(?<Link>.+)'\s*data-listing-id";
    }
}
