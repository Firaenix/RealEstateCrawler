namespace RealEstateCrawler.Service.Utilities
{
    public static class DomainUtils
    {
        public static string PropertyRegex = @"(?i)(?<Price>\$.+)\s*(?<No>(\d)+([A-Z])?(\/((\d)+([A-Z])?))?)\s*(?<Street>.+),\s*(?<Suburb>.+),\s*(?<State>.+)\s*(&n\s*bsp;)(?<PostCode>\w+)\s+(?<Bed>\w+)\s+beds\s+(?<Bath>\w+)\s+bathrooms\s+(?<Park>\w+)";
        public static string LinkRegex = @"href='(?<Link>.+)'\s*data-listing-id";
    }
}
