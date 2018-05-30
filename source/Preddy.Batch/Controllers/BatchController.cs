using Karemem0.Preddy.Configuration;
using Karemem0.Preddy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Karemem0.Preddy.Controllers {

    /// <summary>
    /// バッチ処理を実行するコントローラーを表します。
    /// </summary>
    public class BatchController : IDisposable {

        /// <summary>
        /// <see cref="Karemem0.Preddy.Controllers.BatchController"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public BatchController() { }

        /// <summary>
        /// 新しいツイートのログを取得してデータベースに保存します。
        /// </summary>
        public void InsertTweetLog() {
            try {
                using (var twitterService = new TwitterService())
                using (var tweetLogService = new TweetLogService()) {
                    var maxId = default(long);
                    var sinceId = tweetLogService.GetMaxId().GetValueOrDefault();
                    while (sinceId == 0 || maxId == 0 || maxId >= sinceId) {
                        var tweetLogs = twitterService.Search(maxId);
                        if (tweetLogs.Length <= 1) {
                            break;
                        }
                        foreach (var tweetLog in tweetLogs) {
                            tweetLogService.AddOrUpdate(tweetLog);
                        }
                        maxId = long.Parse(tweetLogs.Min(x => x.StatusId));
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 新しいツイートの統計を取得してデータベースに保存します。
        /// </summary>
        public void InsertTweetSummary() {
            try {
                using (var tweetSummaryService = new TweetResultService()) {
                    foreach (var tweetSummary in tweetSummaryService.Summarize()) {
                        tweetSummaryService.AddOrUpdate(tweetSummary);
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// ツイートの予測を取得してデータベースに保存します。
        /// </summary>
        public void InsertTweetForecast() {
            try {
                using (var azuremlService = new AzureMachineLearningService())
                using (var tweetForecastService = new TweetForecastService()) {
                    foreach (var tweetForecast in azuremlService.Search()) {
                        tweetForecastService.AddOrUpdate(tweetForecast);
                    }
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 古いツイートのログを削除します。
        /// </summary>
        public void DeleteTweetLog() {
            try {
                using (var tweetLogService = new TweetLogService()) {
                    tweetLogService.Shrink();
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// 現在のインスタンスで使用されているリソースを解放します。
        /// </summary>
        public void Dispose() { }

    }

}
