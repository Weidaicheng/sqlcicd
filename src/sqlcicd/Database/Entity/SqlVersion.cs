using System;
using sqlcicd.Repository;
using sqlcicd.Repository.Entity;
using sqlcicd.Utility;

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
        public Guid Id { get; set; }

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
        public Guid? LastVersion { get; set; }

        /// <summary>
        /// Is this the latest version
        /// </summary>
        public bool IsLatest { get; set; }

        /// <summary>
        /// Has this version been rollbacked
        /// </summary>
        public bool IsRollbacked { get; set; }

        /// <summary>
        /// Can this version be rollbacked
        /// </summary>
        public bool CanRollback
        {
            get
            {
                if (LastVersion == null)
                    return false;
                return true;
            }
        }
    }
}