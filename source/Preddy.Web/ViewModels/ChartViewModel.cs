using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Preddy.ViewModels {

    [DataContract()]
    public class ChartViewModel {

        [DataMember()]
        public virtual string MinDate { get; set; }

        [DataMember()]
        public virtual string MaxDate { get; set; }

        [DataMember()]
        public virtual List<KeyValuePair<string, int>> Results { get; set; }

        /// <summary>
        /// <see cref="Preddy.ViewModels.ChartViewModel"/> クラスの新しいインスタンスを初期化します。
        /// </summary>
        public ChartViewModel() { }

    }

}
