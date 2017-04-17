using Karemem0.Preddy.Services;
using Karemem0.Preddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Karemem0.Preddy.Controllers {

    public class TweetForecastController : ApiController {

        /// <summary>
        /// ツイートの予測を返します。
        /// </summary>
        /// <returns>検索結果を示す <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        public TweetForecastViewModel GetTweetForecast(DateTime maxDate, DateTime minDate) {
            using (var tweetForecastService = new TweetForecastService()) {
                return tweetForecastService.Search(maxDate.Date, minDate.Date);
            }
        }

    }

}
