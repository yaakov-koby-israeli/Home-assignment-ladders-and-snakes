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

        protected enumCellType _cellType;
        protected Cell(int index, enumCellType cellType)
        {
            this._index = index;
            _cellType = cellType;
        }
        public abstract enumCellType GetCellType();

        //public abstract int GetIndex();

        // add abstract method to be implemented by derived classes
        //protected abstract int OnLand();
    }
}
