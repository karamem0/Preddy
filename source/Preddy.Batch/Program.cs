using Karemem0.Preddy.Controllers;
using Karemem0.Preddy.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy {

    /// <summary>
    /// アプリケーションのエントリ ポイントを定義します。
    /// </summary>
    public static class Program {

        /// <summary>
        /// プログラムを開始します。
        /// </summary>
        /// <param name="args">プログラムの引数を示す <see cref="System.String"/> 配列。</param>
        private static void Main(string[] args) {
            using (var controller = new BatchController()) {
                controller.InsertTweetLog();
                controller.InsertTweetSummary();
                controller.DeleteTweetLog();
            }
        }

    }

}
