using Karemem0.Preddy.Configuration;
using Karemem0.Preddy.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy.Services {

    /// <summary>
    /// Azure Machine Learning から予測データを取得するサービスを表します。
    /// </summary>
    public class AzureMachineLearningService : IDisposable {

        private HttpClient httpClient;

        /// <summary>
        /// <see cref="Karemem0.Preddy.Services.AzureMachineLearningService"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public AzureMachineLearningService() {
            this.httpClient = new HttpClient();
        }

        /// <summary>
        /// 予測データを検索します。
        /// </summary>
        /// <returns><see cref="Karemem0.Preddy.Models.TweetForecast"/> の配列。</returns>
        public TweetForecast[] Search() {
            var requestContent = new {
                Inputs = new Dictionary<string, List<Dictionary<string, string>>>() { },
                GlobalParameters = new Dictionary<string, string>() { }
            };
            var requestMessage = new HttpRequestMessage();
            requestMessage.Content = new StringContent(JsonConvert.SerializeObject(requestContent), Encoding.UTF8, "application/json");
            requestMessage.Headers.Authorization = new AuthenticationHeaderValue("Bearer", AppSettings.AzureMachineLearningApiKey);
            requestMessage.Method = HttpMethod.Post;
            requestMessage.RequestUri = new Uri(AppSettings.AzureMachineLearningRequestUrl);
            var responseMessage = this.httpClient.SendAsync(requestMessage).GetAwaiter().GetResult();
            var responseString = responseMessage.Content.ReadAsStringAsync().GetAwaiter().GetResult();
            var responseJson = (JObject)JsonConvert.DeserializeObject(responseString);
            return responseJson["Results"]["output1"]
                .Select(item => new {
                    Date = (DateTime)item["date"],
                    Mean = (double)item["mean"]
                })
                .Select(item => new TweetForecast() {
                    Id = Guid.NewGuid(),
                    Date = item.Date,
                    Year = item.Date.Year,
                    Month = item.Date.Month,
                    Day = item.Date.Day,
                    Count = item.Mean,
                })
                .ToArray();
        }

        /// <summary>
        /// 現在のインスタンスで使用されているリソースを解放します。
        /// </summary>
        public void Dispose() {
            if (this.httpClient != null) {
                this.httpClient.Dispose();
            }
        }

    }

}
