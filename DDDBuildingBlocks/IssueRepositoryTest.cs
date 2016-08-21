using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NHibernate.Cfg;
using NHibernate;
using DDDBuildingBlocks.Domain;
using DDDBuildingBlocks.Infrastructure;

namespace DDDBuildingBlocks
{
    [TestClass]
    public class IssueRepositoryTest
    {
        ISession session;
        IssueRepository repository;

        [TestInitialize]
        public void StartDb()
        {
            repository = new NHibernateIssueRepository(session = OpenSession());
        }

        [TestMethod]
        public void ShouldStoreAndLoadSimpleIssueFromDB()
        {
            // given:
            var issue = new Issue(new IssueNumber("BUG-42"));
            issue.RenameTo("NHibernate test does not work");

            // when:
            When(() =>
            {
                repository.Store(issue);
            });

            // then:
            var loaded = repository.Load(new IssueNumber("BUG-42"));
            Assert.AreEqual(loaded.Title, "NHibernate test does not work");
            Assert.AreEqual(loaded.Status, IssueStatus.NEW);
        }

        [TestMethod]
        public void ShouldStoreAndLoadComplexIssueFromDB()
        {
            // given:
            IssueWithNumberAleadyExist(new IssueNumber("BUG-24"));
            var issue = new Issue(new IssueNumber("BUG-42"));
            issue.ReffersTo(new IssueNumber("BUG-24"));

            // when:
            When(() =>
            {
                repository.Store(issue);
            });

            // then:
            var loaded = repository.Load(new IssueNumber("BUG-42"));
            Assert.IsTrue(loaded.IsRefferingTo(new IssueNumber("BUG-24")));
        }

        [TestMethod]
        public void ShouldUpdateDBAfterIssueHaveBeenLoad()
        {
            // given:
            IssueWithNumberAleadyExist(new IssueNumber("BUG-42"));
            
            // when:
            When(() => 
            {
                var issue = repository.Load(new IssueNumber("BUG-42"));
                issue.RenameTo("New Title");
            });

            // then:
            var loaded = repository.Load(new IssueNumber("BUG-42"));
            Assert.AreEqual(loaded.Title, "New Title");
        }

        // --

        private void When(Action lambda)
        {
            
            using(ITransaction tx = session.BeginTransaction())
            {
                lambda.Invoke();    
                tx.Commit();
                session.Clear();
            }
        }

        private void IssueWithNumberAleadyExist(IssueNumber id)
        {
            var issue = new Issue(id);
            issue.RenameTo("Old Title");
            When(() => repository.Store(issue));
        }

        private ISession OpenSession()
        {
            var cfg = new Configuration();
            cfg.Configure();
            cfg.AddAssembly(typeof(Issue).Assembly);
            return cfg.BuildSessionFactory().OpenSession();
        }
    }
}
