using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBuildingBlocks.Domain
{
    interface IssueRepository
    {
        void Store(Issue issue);
        Issue Load(IssueNumber id);
    }
}
