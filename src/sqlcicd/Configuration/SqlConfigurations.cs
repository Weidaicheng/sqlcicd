namespace sqlcicd.Configuration
{
    public class SqlConfigurations
    {
        private static SqlIgnoreConfiguration sqlIgnore;
        private static SqlOrderConfiguration sqlOrder;

        public static SqlIgnoreConfiguration SqlIgnore
        {
            get
            {
                return sqlIgnore;
            }
        }

        public static SqlOrderConfiguration SqlOrder
        {
            get
            {
                return sqlOrder;
            }
        }
    }
}