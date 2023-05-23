using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class ColumnDTO : DTO
    {

        public const string BoardIdCol = "Related_Board_Id";
        public const string TaskLimCol = "Task_Limit";


        private int _boardId;
        public int BoardID { get { return _boardId; } }
        public int ColIndex { get { return Id; } }
        private int _taskLimit;
        public int TaskLimit { 
            get { return _taskLimit; } 
            set { _taskLimit = value; _controller.Update(Id,BoardIdCol,BoardID, TaskLimCol, value); } 
        }

        public ColumnDTO(int BoardID, int ColIndex, int TaskLimit) : base(new ColumnMapper())
        {
            _boardId = BoardID;
            Id = ColIndex;
            _taskLimit = TaskLimit;
        }
    }
}
