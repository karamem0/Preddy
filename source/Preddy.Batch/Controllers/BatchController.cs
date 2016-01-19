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
            using (var searchService = new SearchService())
            using (var tweetLogService = new TweetLogService()) {
                var maxId = default(ulong);
                var sinceId = tweetLogService.GetMaxId().GetValueOrDefault();
                while (sinceId == 0 || maxId == 0 || maxId >= sinceId) {
                    var tweetLogs = searchService.SearchByMaxId(maxId);
                    if (tweetLogs.Length <= 1) {
                        break;
                    }
                    foreach (var tweetLog in tweetLogs) {
                        tweetLogService.AddOrUpdate(tweetLog);
                    }
                    maxId = ulong.Parse(tweetLogs.Min(x => x.StatusId));
                }
            }
        }

        /// <summary>
        /// 新しいツイートの統計を取得してデータベースに保存します。
        /// </summary>
        public void InsertTweetSummary() {
            using (var tweetSummaryService = new TweetSummaryService()) {
                foreach (var tweetSummary in tweetSummaryService.Summarize()) {
                    tweetSummaryService.AddOrUpdate(tweetSummary);
                }
            }
        }

        /// <summary>
        /// 古いツイートのログを削除します。
        /// </summary>
        public void DeleteTweetLog() {
            using (var tweetLogService = new TweetLogService()) {
                tweetLogService.Shrink();
            }
        }

        /// <summary>
        /// 現在のインスタンスで使用されているリソースを解放します。
        /// </summary>
        public void Dispose() { }

    }

}
