using Preddy.Models;
using Preddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preddy.Services {

    /// <summary>
    /// グラフのデータを操作するサービスを表します。
    /// </summary>
    public class ChartService : IDisposable {

        /// <summary>
        /// データベース コンテキストを表します。
        /// </summary>
        private DefaultConnectionContext dbContext;

        /// <summary>
        /// <see cref="Preddy.Services.TweetService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ChartService() {
            this.dbContext = new DefaultConnectionContext();
        }

        public ChartViewModel Search(DateTime maxDate, DateTime minDate) {
            return new ChartViewModel() {
                MaxDate = maxDate.ToString("s"),
                MinDate = minDate.ToString("s"),
                Results = this.dbContext.TweetSummaries
                    .Where(x => x.Date >= minDate)
                    .Where(x => x.Date <= maxDate)
                    .OrderBy(x => x.Date)
                    .ToDictionary(x => x.Date.ToString("s"), x => x.Count)
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