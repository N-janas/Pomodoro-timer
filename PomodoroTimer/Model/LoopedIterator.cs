using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace PomodoroTimer.Model
{
    class LoopedIterator : Iterator
    {
        private BreaksCollection _breaksCollection;
        private static int _position = -1;

        public LoopedIterator(BreaksCollection breaksCollection)
        {
            _breaksCollection = breaksCollection;
        }

        public override object Current()
        {
            return _breaksCollection.GetItems()[_position];
        }

        public override int Key()
        {
            return _position;
        }

        public override bool MoveNext()
        {
            int updatedPosition = _position + 1;

            // Loop over
            if (updatedPosition >= _breaksCollection.GetItems().Count)
            {
                Reset();
                return true;
            }

            if (updatedPosition >= 0)
            {
                _position = updatedPosition;
                return true;
            }
            else
                return false;
        }

        public override void Reset()
        {
            _position = 0;
        }
    }
}
