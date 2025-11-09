using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladders_and_snakes_game.Core
{
    internal abstract class Cell
    {
        protected int _index;
        protected Cell(int index)
        {
            this._index = index;
        }

        public abstract int GetIndex();
    }
}
