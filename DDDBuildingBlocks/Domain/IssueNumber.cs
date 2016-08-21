using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DDDBuildingBlocks.Domain
{
    class IssueNumber : ValueObject<string>
    {
        public IssueNumber(string number) : base(number)
        {

        }
        protected IssueNumber() : base(null)
        {

        }
    }
}
