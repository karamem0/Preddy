using Karemem0.Preddy.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        /// 指定したツイートの予測を追加または更新します。
        /// </summary>
        /// <param name="newValue">追加または更新する <see cref="Karemem0.Preddy.Models.TweetForecast"/>。</param>
        /// <returns>処理が正常に行われた場合は true。それ以外の場合は false。</returns>
        public bool AddOrUpdate(TweetForecast newValue) {
            var oldValue = this.dbContext.TweetForecasts.SingleOrDefault(x => x.Date == newValue.Date);
            if (oldValue == null) {
                newValue.CreatedAt = DateTime.UtcNow;
                newValue.UpdatedAt = DateTime.UtcNow;
                this.dbContext.TweetForecasts.Add(newValue);
            } else {
                oldValue.Date = newValue.Date;
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
