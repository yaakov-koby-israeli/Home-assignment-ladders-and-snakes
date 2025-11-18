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

        public virtual enumCellType GetCellType() => _cellType;

        public virtual int GetIndex() => _index;
    }
}
