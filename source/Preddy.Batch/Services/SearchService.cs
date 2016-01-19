using LinqToTwitter;
using Karemem0.Preddy.Configuration;
using Karemem0.Preddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy.Services {

    /// <summary>
    /// Twitter API からツイートを検索するサービスを表します。
    /// </summary>
    public class SearchService : IDisposable {

        /// <summary>
        /// Twitter API のコンテキストを表します。
        /// </summary>
        private TwitterContext twitterContext;

        /// <summary>
        /// <see cref="Karemem0.Preddy.Services.SearchService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public SearchService() {
            this.twitterContext = new TwitterContext(
                new SingleUserAuthorizer() {
                    CredentialStore = new SingleUserInMemoryCredentialStore() {
                        ConsumerKey = AppSettings.ConsumerKey,
                        ConsumerSecret = AppSettings.ConsumerSecret,
                        AccessToken = AppSettings.AccessToken,
                        AccessTokenSecret = AppSettings.AccessTokenSecret
                    }
                });
        }

        /// <summary>
        /// 指定したツイート ID を含む過去のツイートを検索します。
        /// </summary>
        /// <param name="maxId">ツイート ID を示す <see cref="System.UInt64"/>。</param>
        /// <returns><see cref="Karemem0.Preddy.Models.TweetLog"/> の配列。</returns>
        public TweetLog[] SearchByMaxId(ulong? maxId = null) {
            var searchQuery = Uri.EscapeUriString(AppSettings.SearchQuery);
            var searchCount = AppSettings.SearchCount.GetValueOrDefault();
            var searchResult = twitterContext.Search
                .Where(x => x.Type == SearchType.Search)
                .Where(x => x.ResultType == ResultType.Recent)
                .Where(x => x.Query == searchQuery)
                .Where(x => x.Count == searchCount)
                .Where(x => x.MaxID == maxId.GetValueOrDefault())
                .FirstOrDefault();
            var excludeUsers = AppSettings.ExcludeUsers;
            return searchResult.Statuses
                .Where(x => x.RetweetedStatus.StatusID == 0)
                .Where(x => excludeUsers.Contains(x.User.ScreenNameResponse) != true)
                .Select(x => new TweetLog() {
                    Id = Guid.NewGuid(),
                    StatusId = x.StatusID.ToString(),
                    UserId = x.User.UserIDResponse.ToString(),
                    UserName = x.User.Name,
                    ScreenName = x.User.ScreenNameResponse,
                    ProfileImageUrl = x.User.ProfileImageUrlHttps,
                    Text = x.Text,
                    TweetedAt = x.CreatedAt,
                })
                .OrderByDescending(x => x.TweetedAt)
                .ToArray();
        }

        /// <summary>
        /// 現在のインスタンスで使用されているリソースを解放します。
        /// </summary>
        public void Dispose() {
            if (this.twitterContext != null) {
                this.twitterContext.Dispose();
            }
        }

    }

}
