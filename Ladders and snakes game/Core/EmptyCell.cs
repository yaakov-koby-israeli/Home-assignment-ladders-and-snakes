using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Players;

namespace Ladders_and_snakes_game.Core
{
    internal sealed class EmptyCell: Cell
    {
        public EmptyCell(int index, enumCellType cellType): base(index,cellType) { }
        
    }
}
