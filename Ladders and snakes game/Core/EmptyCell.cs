using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ladders_and_snakes_game.Players;

namespace Ladders_and_snakes_game.Core
{
    internal class EmptyCell: Cell
    {
        public EmptyCell(int index, enumCellType cellType): base(index,cellType) { }

        public int GetIndex()
        {
            return _index;
        }

        public override enumCellType GetCellType()
        {
            return _cellType;
        }

        //protected override int OnLand()
        //{
        //    return _index;
        //}
    }
}
