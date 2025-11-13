using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladders_and_snakes_game.Core
{
    internal sealed class TopOrBottomCell: Cell
    {
        public TopOrBottomCell(int index , enumCellType cellType) : base(index, cellType) { }
       
    }
}
