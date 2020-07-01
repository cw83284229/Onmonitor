using System;
using System.Collections.Generic;
using System.Text;

namespace OnMonitor
{
  public  class PagedSortedRequestDto
    {

        public virtual string Sorting { get; set; }

        public virtual int SkipCount { get; set; } = 0;

        public virtual int MaxResultCount { get; set; } = 20;

    }
}
