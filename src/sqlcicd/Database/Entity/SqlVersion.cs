using System;
using sqlcicd.Repository.Entity;
using sqlcicd.Utility;

namespace sqlcicd.Database.Entity
{
    /// <summary>
    /// Sql version entity
    /// </summary>
    public class SqlVersion
    {
        public SqlVersion()
        {
        }

        public SqlVersion(
            RepositoryType repositoryType,
            string version,
            double transactionCost
        )
        {
            RepositoryType = repositoryType;
            Version = version;
            DeliveryTime = TimeUtility.Now;
            TransactionCost = transactionCost;
            LastVersion = null;
            IsLatest = true;
            IsRollBacked = false;
            IsDeleted = false;
        }

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
        /// Transaction time cost, total milli seconds
        /// </summary>
        public double TransactionCost { get; set; }

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
        /// Is deleted
        /// </summary>
        public bool IsDeleted { get; set; }

        /// <summary>
        /// Can this version be roll backed
        /// </summary>
        public bool CanRollback => LastVersion != null;
    }
}