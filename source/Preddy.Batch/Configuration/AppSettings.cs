using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy.Configuration {

    /// <summary>
    /// アプリケーションの設定情報を取得します。
    /// </summary>
    public static class AppSettings {

        /// <summary>
        /// コンシューマー キーを取得します。
        /// </summary>
        public static string TwitterConsumerKey {
            get {
                return ConfigurationManager.AppSettings[nameof(TwitterConsumerKey)];
            }
        }

        /// <summary>
        /// コンシューマー シークレットを取得します。
        /// </summary>
        public static string TwitterConsumerSecret {
            get {
                return ConfigurationManager.AppSettings[nameof(TwitterConsumerSecret)];
            }
        }

        /// <summary>
        /// アクセス トークンを取得します。
        /// </summary>
        public static string TwitterAccessToken {
            get {
                return ConfigurationManager.AppSettings[nameof(TwitterAccessToken)];
            }
        }

        /// <summary>
        /// アクセス トークン シークレットを取得します。
        /// </summary>
        public static string TwitterAccessTokenSecret {
            get {
                return ConfigurationManager.AppSettings[nameof(TwitterAccessTokenSecret)];
            }
        }

        /// <summary>
        /// 検索クエリを取得します。
        /// </summary>
        public static string TwitterSearchQuery {
            get {
                return ConfigurationManager.AppSettings[nameof(TwitterSearchQuery)];
            }
        }

        /// <summary>
        /// 検索件数を取得します。
        /// </summary>
        public static int? TwitterSearchCount {
            get {
                var numValue = default(int);
                var strValue = ConfigurationManager.AppSettings[nameof(TwitterSearchCount)];
                if (int.TryParse(strValue, out numValue) == true) {
                    return numValue;
                }
                return null;
            }
        }

        /// <summary>
        /// 除外ユーザーを取得します。
        /// </summary>
        public static string[] TwitterExcludeUsers {
            get {
                return ConfigurationManager.AppSettings[nameof(TwitterExcludeUsers)].Split(',');
            }
        }

        /// <summary>
        /// Azure ML の API キーを取得します。
        /// </summary>
        public static string AzureMachineLearningApiKey {
            get {
                return ConfigurationManager.AppSettings[nameof(AzureMachineLearningApiKey)];
            }
        }

        /// <summary>
        /// Azure ML の要求 URL を取得します。
        /// </summary>
        public static string AzureMachineLearningRequestUrl {
            get {
                return ConfigurationManager.AppSettings[nameof(AzureMachineLearningRequestUrl)];
            }
        }

        /// <summary>
        /// ログの最大保存件数を取得します。
        /// </summary>
        public static int? LogMaxCount {
            get {
                var numValue = default(int);
                var strValue = ConfigurationManager.AppSettings[nameof(LogMaxCount)];
                if (int.TryParse(strValue, out numValue) == true) {
                    return numValue;
                }
                return null;
            }
        }

    }

}
