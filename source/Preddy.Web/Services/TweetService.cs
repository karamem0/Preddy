using Preddy.Extensions;
using Preddy.Models;
using Preddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preddy.Services {

    /// <summary>
    /// ツイートのデータを操作するサービスを表します。
    /// </summary>
    public class TweetService : IDisposable {

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnectionContext dbContext;

        /// <summary>
        /// <see cref="Preddy.Services.TweetService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetService() {
            this.dbContext = new DefaultConnectionContext();
        }

        public IEnumerable<TweetViewModel> Search(DateTime date) {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            var cultureInfo = CultureInfo.GetCultureInfo("ja-JP");
            var minDate = date.ToUniversalTime(timeZone);
            var maxDate = date.AddDays(1).ToUniversalTime(timeZone);
            return this.dbContext.TweetLogs
                .Where(x => x.TweetedAt >= minDate)
                .Where(x => x.TweetedAt < maxDate)
                .OrderBy(x => x.TweetedAt)
                .ToList()
                .Select(x => new TweetViewModel() {
                    StatusId = x.StatusId,
                    UserId = x.UserId,
                    UserName = x.UserName,
                    ScreenName = "@" + x.ScreenName,
                    ProfileImageUrl = x.ProfileImageUrl,
                    Text = x.Text.Replace("\n", "<br>"),
                    TweetedAt = x.TweetedAt.ToLocalTime(timeZone).ToString(cultureInfo.DateTimeFormat),
                    StatusUrl = string.Format("https://twitter.com/{0}/status/{1}", x.ScreenName, x.StatusId),
                    UserUrl = string.Format("https://twitter.com/{0}", x.ScreenName),
                });
        }

        /// <summary>
        /// 現在のインスタンスで使用されているリソースを解放します。
        /// </summary>
        public void Dispose() {
            if (this.dbContext != null) {
                this.dbContext.Dispose();
            }
        }

    }

}