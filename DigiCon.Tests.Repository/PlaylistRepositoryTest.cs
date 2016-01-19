using DigiCon.Domain.Entities;
using DigiCon.Service.Repositories;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace DigiCon.Test.Repositories
{
    /// <summary>
    /// PlaylistRepository is a service for dataaccess on database for playlists.
    /// This class is testing that that service. 
    /// </summary>
    [TestClass]
    public class PlaylistRepositoryTest
    {
        public PlaylistRepositoryTest()
        {
            playlistrepo = new PlaylistRepository();
        }

        private TestContext testContextInstance;
        private IPlaylistRepository playlistrepo;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        //
        // You can use the following additional attributes as you write your tests:
        //
        // Use ClassInitialize to run code before running the first test in the class
        // [ClassInitialize()]
        // public static void MyClassInitialize(TestContext testContext) { }
        //
        // Use ClassCleanup to run code after all tests in a class have run
        // [ClassCleanup()]
        // public static void MyClassCleanup() { }
        //
        // Use TestInitialize to run code before running each test 
        // [TestInitialize()]
        // public void MyTestInitialize() { }
        //
        // Use TestCleanup to run code after each test has run
        // [TestCleanup()]
        // public void MyTestCleanup() { }
        //
        #endregion

        [ClassInitialize]
        public static void TestClassInitialize(TestContext testContext)
        {

        }

        [ClassCleanup]
        public static void TestClassCleanup()
        {

        }

        [TestInitialize]
        public void TestMethodInitialize()
        {

        }

        [TestCleanup]
        public void TestMethodCleanup()
        {

        }

        [TestMethod]
        public void PlaylistDoesNotExistReturnsNull()
        {
            Playlist playlist = new Playlist() { Name = "Test" };
            playlistrepo.InsertPlaylist(playlist);
            Assert.AreEqual(null, playlistrepo.GetPlaylistByID(playlist.PlaylistID+1));
        }
        
        [TestMethod]
        public void PlaylistExistReturnsPlaylist()
        {
            Playlist playlist = new Playlist() { Name = "Test" };
            playlistrepo.InsertPlaylist(playlist);
            Assert.AreSame(playlist, playlistrepo.GetPlaylistByID(playlist.PlaylistID));
        }

       
    }
}
