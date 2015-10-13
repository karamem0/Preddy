using Microsoft.VisualStudio.TestTools.UnitTesting;
using Preddy.Services;
using Preddy.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace Preddy.Services.Tests {

    /// <summary>
    /// <see cref="Preddy.Services.TweetLogService"/> クラスをテストします。
    /// </summary>
    [TestClass()]
    public class TweetLogServiceTests {

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
                dbContext.TweetLogs.AddOrUpdate(new[] {
                    new TweetLog() { Id = Guid.NewGuid(), StatusId = "1", TweetedAt = new DateTime(2015, 1, 1, 03, 0, 0), },
                    new TweetLog() { Id = Guid.NewGuid(), StatusId = "2", TweetedAt = new DateTime(2015, 1, 1, 10, 0, 0), },
                    new TweetLog() { Id = Guid.NewGuid(), StatusId = "3", TweetedAt = new DateTime(2015, 1, 2, 19, 0, 0), },
                });
                dbContext.SaveChanges();
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
        /// <see cref="Preddy.Services.TweetLogService.GetMaxId"/> メソッドをテストします。
        /// </summary>

        [TestMethod()]
        public void GetMaxIdTest1() {
            var target = new TweetLogService();
            var actual = target.GetMaxId();
            Debug.WriteLine(actual);
            Assert.AreEqual(actual, (ulong)3);
        }

        /// <summary>
        /// <see cref="Preddy.Services.TweetLogService.AddOrUpdate"/> メソッドをテストします。
        /// </summary>
        [TestMethod()]
        public void AddOrUpdateTest1() {
            var target = new TweetLogService();
            var actual = target.AddOrUpdate(new TweetLog() { Id = Guid.NewGuid(), StatusId = "4", });
            Assert.AreEqual(actual, true);
            using (var dbContext = new DefaultConnectionContext()) {
                foreach (var item in dbContext.TweetLogs) {
                    Debug.WriteLine(item);
                }
            }
        }

        /// <summary>
        /// <see cref="Preddy.Services.TweetLogService.AddOrUpdate"/> メソッドをテストします。
        /// </summary>
        [TestMethod()]
        public void AddOrUpdateTest2() {
            var target = new TweetLogService();
            var actual = target.AddOrUpdate(new TweetLog() { Id = Guid.NewGuid(), StatusId = "3", });
            Assert.AreEqual(actual, true);
            using (var dbContext = new DefaultConnectionContext()) {
                foreach (var item in dbContext.TweetLogs) {
                    Debug.WriteLine(item);
                }
            }
        }

        /// <summary>
        /// <see cref="Preddy.Services.TweetLogService.GetCount"/> メソッドをテストします。
        /// </summary>
        [TestMethod()]
        public void GetCountTest1() {
            var target = new TweetLogService();
            var actual = target.GetCount();
            Debug.WriteLine(actual);
        }

        /// <summary>
        /// <see cref="Preddy.Services.TweetLogService.Shrink"/> メソッドをテストします。
        /// </summary>
        [TestMethod()]
        public void ShrinkTest1() {
            var target = new TweetLogService();
            target.Shrink();
            using (var dbContext = new DefaultConnectionContext()) {
                foreach (var item in dbContext.TweetLogs) {
                    Debug.WriteLine(item);
                }
            }
        }

    }

}