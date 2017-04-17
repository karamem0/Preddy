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
    /// ツイートの実績を格納します。
    /// </summary>
    [Table(nameof(TweetResult))]
    public class TweetResult {

        /// <summary>
        /// ID を取得または設定します。
        /// </summary>
        [Key()]
        public virtual Guid Id { get; set; }

        /// <summary>
        /// 日付を取得または設定します。
        /// </summary>
        [Index(IsUnique = true)]
        public virtual DateTime Date { get; set; }

        /// <summary>
        /// 日付の年の部分を取得または設定します。
        /// </summary>
        public virtual int Year { get; set; }

        /// <summary>
        /// 日付の月の部分を取得または設定します。
        /// </summary>
        public virtual int Month { get; set; }

        /// <summary>
        /// 日付の日の部分を取得または設定します。
        /// </summary>
        public virtual int Day { get; set; }

        /// <summary>
        /// 件数を取得または設定します。
        /// </summary>
        public virtual int Count { get; set; }

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
        /// <see cref="Karemem0.Preddy.Models.TweetResult"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetResult() { }

        /// <summary>
        /// 現在のインスタンスの文字列表現を返します。
        /// </summary>
        /// <returns>現在のインスタンスの文字列表現を示す <see cref="System.String"/>。</returns>
        public override string ToString() {
            return JsonConvert.SerializeObject(this);
        }

    }

}
