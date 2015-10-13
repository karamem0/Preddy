﻿using Preddy.Extensions;
using Preddy.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preddy.Services {

    /// <summary>
    /// ツイートの統計を操作するサービスを表します。
    /// </summary>
    public class TweetSummaryService : IDisposable {

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnectionContext dbContext;

        /// <summary>
        /// <see cref="Preddy.Services.TweetLogService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetSummaryService() {
            this.dbContext = new DefaultConnectionContext();
        }

        /// <summary>
        /// ツイートのログから統計を作成します。
        /// </summary>
        /// <returns><see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        public IEnumerable<TweetSummary> Summarize() {
            var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
            var minDate = this.dbContext.TweetLogs.Min(x => x.TweetedAt).ToLocalTime(timeZone).Date;
            var maxDate = this.dbContext.TweetLogs.Max(x => x.TweetedAt).ToLocalTime(timeZone).Date;
            for (var date = minDate; date <= maxDate; date = date.AddDays(1)) {
                var minTweeted = date.ToUniversalTime(timeZone);
                var maxTweeted = date.AddDays(1).ToUniversalTime(timeZone);
                var tweetCount = this.dbContext.TweetLogs
                    .Where(x => x.TweetedAt >= minTweeted)
                    .Where(x => x.TweetedAt < maxTweeted)
                    .Count();
                yield return new TweetSummary() {
                    Id = Guid.NewGuid(),
                    Date = date,
                    Year = date.Year,
                    Month = date.Month,
                    Day = date.Day,
                    Count = tweetCount,
                };
            }
        }

        /// <summary>
        /// 指定したツイートの統計を追加または更新します。
        /// </summary>
        /// <param name="newValue">追加または更新する <see cref="Preddy.Models.TweetLog"/>。</param>
        /// <returns>処理が正常に行われた場合は true。それ以外の場合は false。</returns>
        public bool AddOrUpdate(TweetSummary newValue) {
            var oldValue = this.dbContext.TweetSummaries.SingleOrDefault(x => x.Date == newValue.Date);
            if (oldValue == null) {
                newValue.CreatedAt = DateTime.UtcNow;
                newValue.UpdatedAt = DateTime.UtcNow;
                this.dbContext.TweetSummaries.Add(newValue);
            } else {
                oldValue.Year = newValue.Year;
                oldValue.Month = newValue.Month;
                oldValue.Day = newValue.Day;
                oldValue.Count = newValue.Count;
                oldValue.UpdatedAt = DateTime.UtcNow;
            }
            return Convert.ToBoolean(this.dbContext.SaveChanges());
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
