using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy.Services.Tests {

    /// <summary>
    /// <see cref="Karemem0.Preddy.Services.AzureMachineLearningService"/> クラスをテストします。
    /// </summary>
    [TestClass()]
    public class AzureMachineLearningServiceTests {

        /// <summary>
        /// <see cref="Karemem0.Preddy.Services.AzureMachineLearningService.SearchAsync"/> メソッドをテストします。
        /// </summary>
        [TestMethod()]
        public void SearchTest1() {
            var target = new AzureMachineLearningService();
            var actual = target.Search();
            Assert.IsNotNull(actual);
            foreach (var item in actual) {
                Debug.WriteLine(item);
            }
        }

    }

}
