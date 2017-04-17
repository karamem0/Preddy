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
    /// ツイートの実績を取得する API コントローラーを表します。
    /// </summary>
    public class TweetResultController : ApiController {

        /// <summary>
        /// ツイートの実績を返します。
        /// </summary>
        /// <returns>検索結果を示す <see cref="Karemem0.Preddy.ViewModels.TweetResultViewModel"/>。</returns>
        public TweetResultViewModel GetTweetResult(DateTime maxDate, DateTime minDate) {
            using (var tweetResultService = new TweetResultService()) {
                return tweetResultService.Search(maxDate.Date, minDate.Date);
            }
        }

    }

}
