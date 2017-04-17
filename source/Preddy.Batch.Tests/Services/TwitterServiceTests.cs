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
    /// <see cref="Karemem0.Preddy.Services.TwitterService"/> クラスをテストします。
    /// </summary>
    [TestClass()]
    public class TwitterServiceTests {

        /// <summary>
        /// <see cref="Karemem0.Preddy.Services.TwitterService.Search"/> メソッドをテストします。
        /// </summary>
        [TestMethod()]
        public void SearchTest1() {
            var target = new TwitterService();
            var actual = target.Search();
            Assert.IsNotNull(actual);
            foreach (var item in actual) {
                Debug.WriteLine(item);
            }
        }

    }

}
