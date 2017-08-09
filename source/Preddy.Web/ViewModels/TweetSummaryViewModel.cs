using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy.ViewModels {

    /// <summary>
    /// ツイートの実績を表します。
    /// </summary>
    [DataContract()]
    public class TweetSummaryViewModel {

        /// <summary>
        /// 開始日を取得または設定します。
        /// </summary>
        [DataMember(Name = "minDate")]
        public virtual string MinDate { get; set; }

        /// <summary>
        /// 終了日を取得または設定します。
        /// </summary>
        [DataMember(Name = "maxDate")]
        public virtual string MaxDate { get; set; }

        /// <summary>
        /// 日付と件数のコレクションを取得または設定します。
        /// </summary>
        [DataMember(Name = "items")]
        public virtual List<TweetSummaryItemViewModel> Items { get; set; }

        /// <summary>
        /// <see cref="Karemem0.Preddy.ViewModels.TweetSummaryViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetSummaryViewModel() { }

    }

}
