using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Karemem0.Preddy.ViewModels {

    [DataContract()]
    public class TweetSummaryItemViewModel {

        /// <summary>
        /// 日付を取得または設定します。
        /// </summary>
        [DataMember(Name = "date")]
        public virtual string Date { get; set; }

        /// <summary>
        /// 予測件数を取得または設定します。
        /// </summary>
        [DataMember(Name = "forecast")]
        public virtual double Forecast { get; set; }

        /// <summary>
        /// 実績件数を取得または設定します。
        /// </summary>
        [DataMember(Name = "result")]
        public virtual int Result { get; set; }

        /// <summary>
        /// <see cref="Karemem0.Preddy.ViewModels.TweetSummaryItemViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetSummaryItemViewModel() { }

    }

}
