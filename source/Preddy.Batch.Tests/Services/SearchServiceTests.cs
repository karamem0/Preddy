using Microsoft.VisualStudio.TestTools.UnitTesting;
using Preddy.Services;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Preddy.Services.Tests {

        /// <summary>
        /// <see cref="Preddy.Services.SearchService"/> クラスをテストします。
        /// </summary>
    [TestClass()]
    public class SearchServiceTests {

        /// <summary>
        /// <see cref="Preddy.Services.SearchService.SearchByMaxId"/> メソッドをテストします。
        /// </summary>
        [TestMethod()]
        public void SearchByMaxIdTest() {
            var target = new SearchService();
            var actual = target.SearchByMaxId();
            Assert.IsNotNull(actual);
            foreach (var item in actual) {
                Debug.WriteLine(item);
            }
        }

    }

}
