using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy.ViewModels {

    /// <summary>
    /// グラフのデータを表します。
    /// </summary>
    [DataContract()]
    public class ChartViewModel {

        /// <summary>
        /// 開始日を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string MinDate { get; set; }

        /// <summary>
        /// 終了日を取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual string MaxDate { get; set; }

        /// <summary>
        /// 日付と件数のコレクションを取得または設定します。
        /// </summary>
        [DataMember()]
        public virtual List<KeyValuePair<string, int>> Results { get; set; }

        /// <summary>
        /// <see cref="Karemem0.Preddy.ViewModels.ChartViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ChartViewModel() { }

    }

}
