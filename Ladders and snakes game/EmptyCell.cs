using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladders_and_snakes_game
{
    internal class EmptyCell: Cell
    {
        public EmptyCell(int index): base(index) { }

        public override int GetIndex()
        {
            return _index;
        }
    }
}
