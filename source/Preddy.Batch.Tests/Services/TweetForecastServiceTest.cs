using Karemem0.Preddy.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Data.Entity.Migrations;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Karemem0.Preddy.Services.Tests {

    [TestClass()]
    public class TweetForecastServiceTest {

        /// <summary>
        /// テスト クラスを初期化します。
        /// </summary>
        /// <param name="testContext"></param>
        [ClassInitialize()]
        public static void ClassInitialize(TestContext testContext) {
            AppDomain.CurrentDomain.SetData("DataDirectory", testContext.TestDeploymentDir);
            using (var dbContext = new DefaultConnectionContext()) {
                dbContext.Database.Delete();
                dbContext.Database.Create();
                dbContext.TweetForecasts.AddOrUpdate(new[] {
                    new TweetForecast() { Id = Guid.NewGuid(), Date = new DateTime(2015, 1, 1), },
                    new TweetForecast() { Id = Guid.NewGuid(), Date = new DateTime(2015, 1, 2), },
                    new TweetForecast() { Id = Guid.NewGuid(), Date = new DateTime(2015, 1, 3), },
                });
            }
        }

        /// <summary>
        /// テスト クラスで使用されたリソースを解放します。
        /// </summary>
        [ClassCleanup()]
        public static void ClassCleanup() {
            using (var dbContext = new DefaultConnectionContext()) {
                dbContext.Database.Delete();
            }
        }

        /// <summary>
        /// <see cref="Karemem0.Preddy.Services.TweetForecastService.AddOrUpdate"/> メソッドをテストします。
        /// </summary>
        [TestMethod()]
        public void AddOrUpdateTest1() {
            var target = new TweetForecastService();
            var actual = target.AddOrUpdate(new TweetForecast() { Id = Guid.NewGuid(), Date = new DateTime(2015, 1, 4), });
            Assert.AreEqual(actual, true);
            using (var dbContext = new DefaultConnectionContext()) {
                foreach (var item in dbContext.TweetForecasts) {
                    Debug.WriteLine(item);
                }
            }
        }

        /// <summary>
        /// <see cref="Karemem0.Preddy.Services.TweetForecastService.AddOrUpdate"/> メソッドをテストします。
        /// </summary>
        [TestMethod()]
        public void AddOrUpdateTest2() {
            var target = new TweetForecastService();
            var actual = target.AddOrUpdate(new TweetForecast() { Id = Guid.NewGuid(), Date = new DateTime(2015, 1, 3), });
            Assert.AreEqual(actual, true);
            using (var dbContext = new DefaultConnectionContext()) {
                foreach (var item in dbContext.TweetForecasts) {
                    Debug.WriteLine(item);
                }
            }
        }

    }

}
