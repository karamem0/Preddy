using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preddy.Configuration {

    /// <summary>
    /// アプリケーションの設定情報を取得します。
    /// </summary>
    public static class AppSettings {

        /// <summary>
        /// コンシューマー キーを取得します。
        /// </summary>
        public static string ConsumerKey {
            get {
                return ConfigurationManager.AppSettings[nameof(ConsumerKey)];
            }
        }

        /// <summary>
        /// コンシューマー シークレットを取得します。
        /// </summary>
        public static string ConsumerSecret {
            get {
                return ConfigurationManager.AppSettings[nameof(ConsumerSecret)];
            }
        }

        /// <summary>
        /// アクセス トークンを取得します。
        /// </summary>
        public static string AccessToken {
            get {
                return ConfigurationManager.AppSettings[nameof(AccessToken)];
            }
        }

        /// <summary>
        /// アクセス トークン シークレットを取得します。
        /// </summary>
        public static string AccessTokenSecret {
            get {
                return ConfigurationManager.AppSettings[nameof(AccessTokenSecret)];
            }
        }

        /// <summary>
        /// 検索クエリを取得します。
        /// </summary>
        public static string SearchQuery {
            get {
                return ConfigurationManager.AppSettings[nameof(SearchQuery)];
            }
        }

        /// <summary>
        /// 検索件数を取得します。
        /// </summary>
        public static int? SearchCount {
            get {
                var numValue = default(int);
                var strValue = ConfigurationManager.AppSettings[nameof(SearchCount)];
                if (int.TryParse(strValue, out numValue) == true) {
                    return numValue;
                }
                return null;
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

        /// <summary>
        /// 除外ユーザーを取得します。
        /// </summary>
        public static string[] ExcludeUsers {
            get {
                return ConfigurationManager.AppSettings[nameof(ExcludeUsers)].Split(',');
            }
        }

    }

}
