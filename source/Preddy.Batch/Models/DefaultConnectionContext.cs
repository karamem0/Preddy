using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preddy.Models {

    /// <summary>
    /// 既定のデータベースに接続するデータベース コンテキストを表します。
    /// </summary>
    public class DefaultConnectionContext : DbContext {

        /// <summary>
        /// <see cref="Preddy.Models.TweetLog"/> クラスのコレクションを取得します。
        /// </summary>
        public virtual DbSet<TweetLog> TweetLogs { get; set; }

        /// <summary>
        /// <see cref="Preddy.Models.TweetSummary"/> クラスのコレクションを取得します。
        /// </summary>
        public virtual DbSet<TweetSummary> TweetSummaries { get; set; }

        /// <summary>
        /// <see cref="Preddy.Models.DefaultConnectionContext"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public DefaultConnectionContext() : base("DefaultConnection") { }

    }

}
