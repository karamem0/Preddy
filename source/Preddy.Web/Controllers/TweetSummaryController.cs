using Karemem0.Preddy.Services;
using Karemem0.Preddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Karemem0.Preddy.Controllers {

    /// <summary>
    /// ツイートの予測と実績を取得する API コントローラーを表します。
    /// </summary>
    public class TweetSummaryController : ApiController {

        /// <summary>
        /// ツイートの予測と実績を返します。
        /// </summary>
        /// <returns>検索結果を示す <see cref="Karemem0.Preddy.ViewModels.TweetSummaryViewModel"/>。</returns>
        public TweetSummaryViewModel GetTweetSummary(DateTime maxDate, DateTime minDate) {
            using (var tweetSummaryService = new TweetSummaryService()) {
                return tweetSummaryService.Search(maxDate.Date, minDate.Date);
            }
        }

    }

}
