using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy.Models {

    /// <summary>
    /// ツイートのログを格納します。
    /// </summary>
    [Table(nameof(TweetLog))]
    public class TweetLog {

        /// <summary>
        /// ID を取得または設定します。
        /// </summary>
        [Key()]
        public virtual Guid Id { get; set; }

        /// <summary>
        /// ツイート ID を取得または設定します。
        /// </summary>
        [Index(IsUnique = true)]
        [StringLength(40)]
        public virtual string StatusId { get; set; }

        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        [StringLength(40)]
        public virtual string UserId { get; set; }

        /// <summary>
        /// ユーザー名を取得または設定します。
        /// </summary>
        [StringLength(20)]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 表示名を取得または設定します。
        /// </summary>
        [StringLength(40)]
        public virtual string ScreenName { get; set; }

        /// <summary>
        /// プロフィール画像の URL を取得または設定します。
        /// </summary>
        [StringLength(1024)]
        public virtual string ProfileImageUrl { get; set; }

        /// <summary>
        /// 本文を取得または設定します。
        /// </summary>
        [StringLength(200)]
        public virtual string Text { get; set; }

        /// <summary>
        /// 投稿日時を取得または設定します。
        /// </summary>
        [Column(TypeName = "datetime2")]
        public virtual DateTime TweetedAt { get; set; }

        /// <summary>
        /// 作成日時を取得または設定します。
        /// </summary>
        [Column(TypeName = "datetime2")]
        public virtual DateTime CreatedAt { get; set; }

        /// <summary>
        /// 更新日時を取得または設定します。
        /// </summary>
        [Column(TypeName = "datetime2")]
        public virtual DateTime UpdatedAt { get; set; }

        /// <summary>
        /// <see cref="Karemem0.Preddy.Models.TweetLog"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetLog() { }

        /// <summary>
        /// 現在のインスタンスの文字列表現を返します。
        /// </summary>
        /// <returns>現在のインスタンスの文字列表現を示す <see cref="System.String"/>。</returns>
        public override string ToString() {
            var serializer = new JsonSerializer();
            using (var stream = new MemoryStream())
            using (var writer = new StreamWriter(stream)) {
                serializer.Serialize(writer, this);
                var buffer = stream.ToArray();
                return Encoding.UTF8.GetString(buffer, 0, buffer.Length);
            }
        }

    }

}
