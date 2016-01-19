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
    /// グラフのデータを取得する API コントローラーを表します。
    /// </summary>
    public class ChartController : ApiController {

        /// <summary>
        /// グラフのデータを返します。
        /// </summary>
        /// <returns>検索結果を示す <see cref="System.Collections.Generic.IEnumerable{T}"/>。</returns>
        public ChartViewModel GetChart(DateTime maxDate, DateTime minDate) {
            using (var chartService = new ChartService()) {
                return chartService.Search(maxDate.Date, minDate.Date);
            }
        }

    }

}
