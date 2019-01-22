using Microsoft.VisualStudio.TestTools.UnitTesting;
using SimpleChallenge;
using System;

namespace ChallengeTestProject
{
    [TestClass]
    public class OOPTests
    {
        [TestMethod]
        public void TestInstantiation()
        {
            Challenge.User user = new Challenge.User("User 1");
            Assert.AreEqual("User 1", user.GetName(), "User name is set correctly:");
            Challenge.Moderator mod = new Challenge.Moderator("Moderator");
            Assert.IsInstanceOfType(mod, typeof(Challenge.Moderator), "Moderator is a User:");
        }

        [TestMethod]
        public void TestLogin()
        {
            Challenge.User user = new Challenge.User("User 1");
            Assert.AreEqual("User 1", user.GetName(), "User name is set correctly:");
            Assert.AreEqual(user.IsLoggedIn(), false);
            user.LogIn();
            Assert.AreEqual(user.IsLoggedIn(), true);
            Assert.AreEqual(user.GetLastLoggedInAt().ToString(@"yyyy-MM-dd"), DateTime.Now.ToString(@"yyyy-MM-dd"));
            user.LogOut();
            Assert.AreEqual(user.IsLoggedIn(), false);
            Assert.AreEqual(user.GetLastLoggedInAt().ToString(@"yyyy-MM-dd"), DateTime.Now.ToString(@"yyyy-MM-dd"));
        }

        [TestMethod]
        public void TestCanAdd()
        {
            Challenge.User user = new Challenge.User("User 1");
            var comment = new Challenge.Comment(user, "hi", new Challenge.Comment(user, "It's me", null) );
            Assert.AreEqual(user.CanAdd(comment), true);
            Challenge.Moderator mod = new Challenge.Moderator("Moderator");
            Assert.AreEqual(mod.CanAdd(comment), false);
            Challenge.Admin adm = new Challenge.Admin("Moderator");
            Assert.AreEqual(adm.CanAdd(comment), true);
        }

        [TestMethod]
        public void TestCanEdit()
        {
            Challenge.User user = new Challenge.User("User 1");
            var comment = new Challenge.Comment(user, "hi", new Challenge.Comment(user, "It's me", null));
            Assert.AreEqual(user.CanEdit(comment), true);
            Challenge.Moderator mod = new Challenge.Moderator("Moderator");
            Assert.AreEqual(mod.CanEdit(comment), false);
            Challenge.Admin adm = new Challenge.Admin("Moderator");
            Assert.AreEqual(adm.CanEdit(comment), true);
        }

        [TestMethod]
        public void TestCanEditDifferentUser()
        {
            Challenge.User user = new Challenge.User("User 1");
            Challenge.User user2 = new Challenge.User("User 2");
            var comment = new Challenge.Comment(user2, "hi", new Challenge.Comment(user, "It's me", null));
            Assert.AreEqual(user.CanEdit(comment), false);
        }

        [TestMethod]
        public void TestCanDelete()
        {
            Challenge.User user = new Challenge.User("User 1");
            var comment = new Challenge.Comment(user, "hi", new Challenge.Comment(user, "It's me", null));
            Assert.AreEqual(user.CanDelete(comment), false);
            Challenge.Moderator mod = new Challenge.Moderator("Moderator");
            Assert.AreEqual(mod.CanDelete(comment), true);
            Challenge.Admin adm = new Challenge.Admin("Moderator");
            Assert.AreEqual(adm.CanDelete(comment), true);
        }
    }
}
