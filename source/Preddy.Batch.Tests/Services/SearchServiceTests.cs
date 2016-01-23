using Microsoft.VisualStudio.TestTools.UnitTesting;
using Karemem0.Preddy.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy.Services.Tests {

    /// <summary>
    /// <see cref="Karemem0.Preddy.Services.SearchService"/> クラスをテストします。
    /// </summary>
    [TestClass()]
    public class SearchServiceTests {

        /// <summary>
        /// <see cref="Karemem0.Preddy.Services.SearchService.SearchByMaxId"/> メソッドをテストします。
        /// </summary>
        [TestMethod()]
        public void SearchByMaxIdTest1() {
            var target = new SearchService();
            var actual = target.SearchByMaxId();
            Assert.IsNotNull(actual);
            foreach (var item in actual) {
                Debug.WriteLine(item);
            }
        }

    }

}
