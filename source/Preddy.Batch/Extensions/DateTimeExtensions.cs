using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy.Extensions {

    /// <summary>
    /// <see cref="System.DateTime"/> クラスの拡張メソッドを定義します。
    /// </summary>
    public static class DateTimeExtensions {

        /// <summary>
        /// 指定した世界協定時刻を指定したタイム ゾーンの時刻に変換します。
        /// </summary>
        /// <param name="target">時刻を示す <see cref="System.DateTime"/>。</param>
        /// <param name="timeZone">タイム ゾーンを示す <see cref="System.TimeZoneInfo"/>。</param>
        /// <returns></returns>
        public static DateTime ToLocalTime(this DateTime target, TimeZoneInfo timeZone) {
            return TimeZoneInfo.ConvertTimeFromUtc(target, timeZone);
        }

        /// <summary>
        /// 指定したタイム ゾーンの時刻を指定した世界協定時刻に変換します。
        /// </summary>
        /// <param name="target">時刻を示す <see cref="System.DateTime"/>。</param>
        /// <param name="timeZone">タイム ゾーンを示す <see cref="System.TimeZoneInfo"/>。</param>
        /// <returns></returns>
        public static DateTime ToUniversalTime(this DateTime target, TimeZoneInfo timeZone) {
            return TimeZoneInfo.ConvertTimeToUtc(target, timeZone);
        }

    }

}
