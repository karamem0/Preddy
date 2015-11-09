using Preddy.Services;
using Preddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace Preddy.Controllers {

    /// <summary>
    /// ツイートのデータを取得する API コントローラーを表します。
    /// </summary>
    public class TweetController : ApiController {

        /// <summary>
        /// ツイートのデータをを返します。
        /// </summary>
        /// <returns>検索結果を示す <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        public IEnumerable<TweetViewModel> GetTweet(DateTime date) {
            using (var tweetService = new TweetService()) {
                return tweetService.Search(date.Date);
            }
        }

    }

}
