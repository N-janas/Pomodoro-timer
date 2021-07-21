using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PomodoroTimer.Model
{
    using Modes;
    public class BreaksCollection
    {
        private List<TimeMode> _breaksOrder = new List<TimeMode>();
        public int Position { get; set; } = -1;

        public object Current()
        {
            return _breaksOrder[Position];
        }

        public List<TimeMode> GetItems()
        {
            return _breaksOrder;
        }

        public void AddItem(TimeMode breakType)
        {
            _breaksOrder.Add(breakType);
        }

        public bool MoveNext()
        {
            int updatedPosition = Position + 1;

            // Loop over
            if (updatedPosition >= _breaksOrder.Count)
            {
                Reset();
                return true;
            }

            if (updatedPosition >= 0)
            {
                Position = updatedPosition;
                return true;
            }
            else
                return false;
        }

        public void Reset()
        {
            Position = 0;
        }
    }
}
