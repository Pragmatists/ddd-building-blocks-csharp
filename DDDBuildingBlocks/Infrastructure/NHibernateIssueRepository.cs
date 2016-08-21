using DDDBuildingBlocks.Domain;
using NHibernate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBuildingBlocks.Infrastructure
{
    class NHibernateIssueRepository : IssueRepository
    {
        private ISession session;

        public NHibernateIssueRepository(ISession session)
        {
            this.session = session;
        }

        public Issue Load(IssueNumber id)
        {
            return session.Get<Issue>(id);
        }

        public void Store(Issue issue)
        {
            session.Save(issue);
        }
    }
}
