﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IntroSE.Kanban.Backend.BusinessLayer;
using Newtonsoft.Json;

namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public class BoardDTO:DTO
    {
        public const string BoardNameColumnName = "BoardName";
        public const string FreeTaskIdColumnName = "FreeTaskId";
        public const string OwnerColumnName = "Owner";
        private string _boardName;
        public string BoardName { get { return _boardName; } set { _boardName = value; _controller.Update(Id, BoardNameColumnName, value); } }
        private int _freeTaskId;
        public int FreeTaskId { get { return _freeTaskId; } set { _freeTaskId = value; _controller.Update(Id, FreeTaskIdColumnName,  value); } }
        private string _owner;
        public string Owner { get { return _owner; } set { _owner = value;_controller.Update(Id, "owner", value); } }




        public BoardDTO( string name, int boardId,int freeTaskId, string userMail) : base(new BoardMapper())
        {
            Id = boardId;
            _boardName = name;
            _owner = userMail;
            _freeTaskId = freeTaskId;
        }
    }


}

