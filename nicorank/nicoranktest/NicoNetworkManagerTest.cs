using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using NicoTools;
using IJLib;
using NUnit.Framework.SyntaxHelpers;
using System.IO;

namespace nicoranktest
{
    [TestFixture]
    public class NicoNetworkManagerTest
    {
        NicoNetwork network_;
        TestMessageOut msgout_;
        CancelObject cancel_object_;
        NicoNetworkManager network_manager_;

        [SetUp]
        public void Setup()
        {
            network_ = new NicoNetwork();
            msgout_ = new TestMessageOut();
            cancel_object_ = new CancelObject();
            network_manager_ = new NicoNetworkManager(network_, msgout_, cancel_object_);
            NicoTools.NicoNetworkManager.StringDelegate string_delegate = delegate(string str) 
            {
                Console.WriteLine(str);
            };
            network_manager_.SetDelegateSetDonwloadInfo(string_delegate);
        }

        [Test]
        public void CheckLoginTest()
        {
            string ok_mail = TestUtility.TestData[TestUtility.KEY_OK_MAIL];
            string ok_pass = TestUtility.TestData[TestUtility.KEY_OK_PASS];

            TestUtility.Message("Running CheckLoginTest");
            network_.SetCookieKind(NicoNetwork.CookieKind.None);
            network_.ClearCookie();

            msgout_.OnWriteLine = delegate(string str)
            {
                Assert.Fail("CheckLoginTest1");
            };

            msgout_.OnWrite = delegate(string str)
            {
                Assert.That(str, Is.EqualTo("ログインされていません。\r\n"), "CheckLoginTest2");
            };

            network_manager_.CheckLogin();

            network_.LoginNiconico(ok_mail, ok_pass);

            msgout_.OnWrite = delegate(string str)
            {
                Assert.That(str, Is.EqualTo("ログインされています。\r\n"), "CheckLoginTest3");
            };

            network_manager_.CheckLogin();
        }

        [Test]
        public void DownloadRankingTest()
        {
            string temp_directory_path = TestUtility.TestData[TestUtility.KEY_TEMP_DIRECTORY];

            TestUtility.EnsureLogin(network_);

            DirectoryInfo temp_directory = new DirectoryInfo(temp_directory_path);

            DownloadKind kind = new DownloadKind();
            kind.SetDuration(true, false, false, false, false);
            CategoryItem categoryItem = new CategoryItem();
            categoryItem.id = "music";
            categoryItem.short_name = "mus";
            categoryItem.name = "音楽";
            categoryItem.page = new int[] { 3, 1, 1, 1, 0 };
            List<CategoryItem> categoryList = new List<CategoryItem>();
            categoryList.Add(categoryItem);
            kind.CategoryList = categoryList;
            kind.SetTarget(true, true, true);
            kind.SetFormat(DownloadKind.FormatKind.Html);

            bool completed = false;

            msgout_.OnWrite = delegate(string str)
            {
                if (str == "すべてのランキングのDLが完了しました。\r\n")
                {
                    completed = true;
                }
            };

            Assert.That(TestUtility.InitDirectory(temp_directory), Is.True, "DownloadRankingTest1");

            TestUtility.Message("Running DownloadRankingTest - Download HTML");
            network_manager_.DownloadRanking(kind, temp_directory_path);

            Assert.That(completed, Is.True, "DownloadRankingTest2-1");
            int html_count = Directory.GetFiles(temp_directory_path, "*.html", SearchOption.AllDirectories).Length;
            Assert.That(html_count, Is.EqualTo(9), "DownloadRankingTest2-2");


            Assert.That(TestUtility.InitDirectory(temp_directory), Is.True, "DownloadRankingTest3");
            kind.SetFormatRss();

            TestUtility.Message("Running DownloadRankingTest - Download RSS");
            network_manager_.DownloadRanking(kind, temp_directory_path);

            Assert.That(completed, Is.True, "DownloadRankingTest4-1");
            html_count = Directory.GetFiles(temp_directory_path, "*.xml", SearchOption.AllDirectories).Length;
            Assert.That(html_count, Is.EqualTo(9), "DownloadRankingTest4-2");
        }
    }
}
