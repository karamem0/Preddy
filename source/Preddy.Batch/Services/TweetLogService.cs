using Preddy.Configuration;
using Preddy.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preddy.Services {

    /// <summary>
    /// ツイートのログを操作するサービスを表します。
    /// </summary>
    public class TweetLogService : IDisposable {

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnectionContext dbContext;

        /// <summary>
        /// <see cref="Preddy.Services.TweetLogService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public TweetLogService() {
            this.dbContext = new DefaultConnectionContext();
        }

        /// <summary>
        /// ツイート ID の最大値を返します。
        /// </summary>
        /// <returns>最大値を示す <see cref="System.UInt64"/>。</returns>
        public ulong? GetMaxId() {
            var numValue = default(ulong);
            var strValue = this.dbContext.TweetLogs.Max(x => x.StatusId);
            if (ulong.TryParse(strValue, out numValue) == true) {
                return numValue;
            }
            return null;
        }

        /// <summary>
        /// 指定したツイートのログを追加または更新します。
        /// </summary>
        /// <param name="newValue">追加または更新する <see cref="Preddy.Models.TweetLog"/>。</param>
        /// <returns>処理が正常に行われた場合は true。それ以外の場合は false。</returns>
        public bool AddOrUpdate(TweetLog newValue) {
            var oldValue = this.dbContext.TweetLogs.SingleOrDefault(x => x.StatusId == newValue.StatusId);
            if (oldValue == null) {
                newValue.CreatedAt = DateTime.UtcNow;
                newValue.UpdatedAt = DateTime.UtcNow;
                this.dbContext.TweetLogs.Add(newValue);
            } else {
                oldValue.UserId = newValue.UserId;
                oldValue.UserName = newValue.UserName;
                oldValue.ScreenName = newValue.ScreenName;
                oldValue.ProfileImageUrl = newValue.ProfileImageUrl;
                oldValue.Text = newValue.Text;
                oldValue.TweetedAt = newValue.TweetedAt;
                oldValue.UpdatedAt = DateTime.UtcNow;
            }
            return Convert.ToBoolean(this.dbContext.SaveChanges());
        }

        /// <summary>
        /// データベースに格納されているツイートのログの件数を返します。
        /// </summary>
        /// <returns></returns>
        public int GetCount() {
            return this.dbContext.TweetLogs.Count();
        }

        /// <summary>
        /// ツイートのログを切り詰めます。
        /// </summary>
        public void Shrink() {
            var logMaxCount = AppSettings.LogMaxCount;
            if (logMaxCount != null) {
                var tweetLog = this.dbContext.TweetLogs
                    .OrderBy(x => x.TweetedAt)
                    .Skip(logMaxCount.Value)
                    .FirstOrDefault();
                if (tweetLog != null) {
                    var timeZone = TimeZoneInfo.FindSystemTimeZoneById("Tokyo Standard Time");
                    var tweetDate = TimeZoneInfo.ConvertTimeToUtc(tweetLog.TweetedAt.AddDays(1).Date, timeZone);
                    this.dbContext.Database.ExecuteSqlCommand(
                        " DELETE FROM [dbo].[TweetLog]" +
                        " WHERE [TweetedAt] < @TweetedAt ",
                        new SqlParameter() {
                            DbType = DbType.DateTime2,
                            ParameterName = "@TweetedAt",
                            Value = tweetDate,
                        }
                    );
                }
            }
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
