using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Model
{
    using Modes;
    public class BreaksCollection : IteratorAggregate
    {
        private List<TimeMode> _breaksOrder = new List<TimeMode>();

        public List<TimeMode> GetItems()
        {
            return _breaksOrder;
        }

        public void AddItem(TimeMode breakType)
        {
            _breaksOrder.Add(breakType);
        }

        public override IEnumerator GetEnumerator()
        {
            return new LoopedIterator(this);
        }
    }
}
