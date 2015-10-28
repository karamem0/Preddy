using Preddy.Services;
using Preddy.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.ModelBinding;

namespace Preddy.Controllers {

    public class ChartController : ApiController {

        public ChartViewModel GetChart(DateTime maxDate, DateTime minDate) {
            using (var chartService = new ChartService()) {
                return chartService.Search(maxDate.Date, minDate.Date);
            }
        }

    }

}
