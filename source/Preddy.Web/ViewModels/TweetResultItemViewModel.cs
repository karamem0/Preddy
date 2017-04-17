using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Karemem0.Preddy.ViewModels {

    [DataContract()]
    public class TweetResultItemViewModel {

        /// <summary>
        /// 日付を取得または設定します。
        /// </summary>
        [DataMember(Name = "date")]
        public virtual string Date { get; set; }

        /// <summary>
        /// 件数を取得または設定します。
        /// </summary>
        [DataMember(Name = "count")]
        public virtual int Count { get; set; }

        public TweetResultItemViewModel() { }

    }
}
