using System;
using sqlcicd.Repository.Entity;

namespace sqlcicd.Database.Entity
{
    /// <summary>
    /// Sql version entity
    /// </summary>
    public class SqlVersion
    {
        /// <summary>
        /// Id
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Repository type
        /// </summary>
        public RepositoryType RepositoryType { get; set; }

        /// <summary>
        /// Version
        /// </summary>
        public string Version { get; set; }

        /// <summary>
        /// Delivery time
        /// </summary>
        public DateTime DeliveryTime { get; set; }

        /// <summary>
        /// Transaction time cost, total seconds
        /// </summary>
        public int TransactionCost { get; set; }

        /// <summary>
        /// The last version
        /// </summary>
        public int? LastVersion { get; set; }

        /// <summary>
        /// Is this the latest version
        /// </summary>
        public bool IsLatest { get; set; }

        /// <summary>
        /// Has this version been roll backed
        /// </summary>
        public bool IsRollBacked { get; set; }

        /// <summary>
        /// Can this version be roll backed
        /// </summary>
        public bool CanRollback => LastVersion != null;
    }
}