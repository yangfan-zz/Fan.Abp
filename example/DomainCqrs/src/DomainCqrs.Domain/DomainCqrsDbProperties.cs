namespace DomainCqrs
{
    public static class DomainCqrsDbProperties
    {
        public static string DbTablePrefix { get; set; } = "DomainCqrs";

        public static string DbSchema { get; set; } = null;

        public const string ConnectionStringName = "DomainCqrs";
    }
}
