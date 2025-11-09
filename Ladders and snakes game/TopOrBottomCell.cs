using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ladders_and_snakes_game
{
    internal class TopOrBottomCell: Cell
    {
        private enumCellType _cellType;
        public TopOrBottomCell(int index , enumCellType cellType) : base(index) 
        {
            this._cellType = cellType;
        }
        public override int GetIndex()
        {
            return _index;
        }
        public enumCellType GetCellType()
        {
            return _cellType;
        }
    }
}
