using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy.ViewModels {

    /// <summary>
    /// ツイートのデータを表します。
    /// </summary>
    [DataContract()]
    public class TweetLogViewModel {

        /// <summary>
        /// ツイート ID を取得または設定します。
        /// </summary>
        [DataMember(Name = "statusId")]
        public virtual string StatusId { get; set; }

        /// <summary>
        /// ユーザー ID を取得または設定します。
        /// </summary>
        [DataMember(Name = "userId")]
        public virtual string UserId { get; set; }

        /// <summary>
        /// ユーザー名を取得または設定します。
        /// </summary>
        [DataMember(Name = "userName")]
        public virtual string UserName { get; set; }

        /// <summary>
        /// 表示名を取得または設定します。
        /// </summary>
        [DataMember(Name = "screenName")]
        public virtual string ScreenName { get; set; }

        /// <summary>
        /// 本文を取得または設定します。
        /// </summary>
        [DataMember(Name = "text")]
        public virtual string Text { get; set; }

        /// <summary>
        /// 投稿日時を取得または設定します。
        /// </summary>
        [DataMember(Name = "tweetedAt")]
        public virtual string TweetedAt { get; set; }

        /// <summary>
        /// プロフィール画像の URL を取得または設定します。
        /// </summary>
        [DataMember(Name = "profileImageUrl")]
        public virtual string ProfileImageUrl { get; set; }

        /// <summary>
        /// 投稿 URL を取得または設定します。
        /// </summary>
        [DataMember(Name = "statusUrl")]
        public virtual string StatusUrl { get; set; }

        /// <summary>
        /// ユーザーの URL を取得または設定します。
        /// </summary>
        [DataMember(Name = "userUrl")]
        public virtual string UserUrl { get; set; }

        /// <summary>
        /// メディアの URL を取得または設定します。
        /// </summary>
        [DataMember(Name = "mediaUrl")]
        public virtual string MediaUrl { get; set; }

        /// <summary>
        /// <see cref="Karemem0.Preddy.ViewModels.TweetLogViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetLogViewModel() { }

    }

}