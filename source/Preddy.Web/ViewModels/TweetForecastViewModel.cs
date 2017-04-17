using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Karemem0.Preddy.ViewModels {

    /// <summary>
    /// ツイートの予測を表します。
    /// </summary>
    [DataContract()]
    public class TweetForecastViewModel {

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
        public virtual List<TweetForecastItemViewModel> Items { get; set; }

        /// <summary>
        /// <see cref="Karemem0.Preddy.ViewModels.TweetForecastViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetForecastViewModel() { }

    }

}