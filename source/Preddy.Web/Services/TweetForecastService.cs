using Karemem0.Preddy.Models;
using Karemem0.Preddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Karemem0.Preddy.Services {

    /// <summary>
    /// ツイートの予測を操作するサービスを表します。
    /// </summary>
    public class TweetForecastService : IDisposable {

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnectionContext dbContext;

        /// <summary>
        /// <see cref="Karemem0.Preddy.Services.TweetForecastService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetForecastService() {
            this.dbContext = new DefaultConnectionContext();
        }

        /// <summary>
        /// 指定した期間の実績を検索します。
        /// </summary>
        /// <param name="maxDate">開始日を示す <see cref="System.DateTime"/>。</param>
        /// <param name="minDate">終了日を示す <see cref="System.DateTime"/>。</param>
        /// <returns><see cref="Karemem0.Preddy.Models.ResultViewModel"/>。</returns>
        public TweetForecastViewModel Search(DateTime maxDate, DateTime minDate) {
            return new TweetForecastViewModel() {
                MaxDate = maxDate.ToString("s"),
                MinDate = minDate.ToString("s"),
                Items = this.dbContext.TweetForecasts
                    .Where(x => x.Date >= minDate)
                    .Where(x => x.Date <= maxDate)
                    .OrderBy(x => x.Date)
                    .ToList()
                    .Select(x => new TweetForecastItemViewModel() {
                        Date = x.Date.ToString("s"),
                        Count = x.Count
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