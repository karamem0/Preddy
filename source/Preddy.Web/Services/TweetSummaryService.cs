using Karemem0.Preddy.Models;
using Karemem0.Preddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy.Services {

    /// <summary>
    /// ツイートの予測と実績を操作するサービスを表します。
    /// </summary>
    public class TweetSummaryService : IDisposable {

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnectionContext dbContext;

        /// <summary>
        /// <see cref="Karemem0.Preddy.Services.TweetSummaryService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetSummaryService() {
            this.dbContext = new DefaultConnectionContext();
        }

        /// <summary>
        /// 指定した期間の予測と実績を検索します。
        /// </summary>
        /// <param name="maxDate">開始日を示す <see cref="System.DateTime"/>。</param>
        /// <param name="minDate">終了日を示す <see cref="System.DateTime"/>。</param>
        /// <returns><see cref="Karemem0.Preddy.Models.ResultViewModel"/>。</returns>
        public TweetSummaryViewModel Search(DateTime maxDate, DateTime minDate) {
            var tweetForecasts = this.dbContext.TweetForecasts
                .Where(item => item.Date >= minDate)
                .Where(item => item.Date <= maxDate)
                .OrderBy(item => item.Date)
                .ToList();
            var tweetResults = this.dbContext.TweetResults
                .Where(item => item.Date >= minDate)
                .Where(item => item.Date <= maxDate)
                .OrderBy(item => item.Date)
                .ToList();
            return new TweetSummaryViewModel() {
                MaxDate = maxDate.ToString("s"),
                MinDate = minDate.ToString("s"),
                Items = Enumerable.Range(0, maxDate.Subtract(minDate).Days)
                    .Select(x => minDate.AddDays(x))
                    .Select(date => new TweetSummaryItemViewModel() {
                        Date = date.ToString("s"),
                        Forecast = tweetForecasts
                            .Where(item => item.Date == date)
                            .Select(item => item.Count < 0.0 ? 0.0 : item.Count)
                            .SingleOrDefault(),
                        Result = tweetResults
                            .Where(item => item.Date == date)
                            .Select(item => item.Count)
                            .SingleOrDefault(),
                    })
                    .ToList(),
            };
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