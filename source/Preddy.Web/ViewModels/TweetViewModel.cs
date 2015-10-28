using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Preddy.ViewModels {

    [DataContract()]
    public class TweetViewModel {

        /// <summary>
        /// ツイート ID を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string StatusId { get; set; }

        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string UserId { get; set; }

        /// <summary>
        /// ユーザー名を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 表示名を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string ScreenName { get; set; }

        /// <summary>
        /// プロフィール画像の URL を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string ProfileImageUrl { get; set; }

        /// <summary>
        /// 本文を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string Text { get; set; }

        /// <summary>
        /// 投稿日時を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string TweetedAt { get; set; }

        [DataMember()]
        public virtual string StatusUrl { get; set; }

        [DataMember()]
        public virtual string UserUrl { get; set; }

        /// <summary>
        /// <see cref="Preddy.ViewModels.TweetViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetViewModel() { }

    }

}