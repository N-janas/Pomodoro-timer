using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Model
{
    class BreaksCollection : IteratorAggregate
    {
        private List<Modes> _breaksOrder = new List<Modes>();

        public List<Modes> GetItems()
        {
            return _breaksOrder;
        }

        public void AddItem(Modes breakType)
        {
            _breaksOrder.Add(breakType);
        }

        public override IEnumerator GetEnumerator()
        {
            return new LoopedIterator(this);
        }
    }

    enum Modes
    {
        ShortB,
        LongB,
    }
}
