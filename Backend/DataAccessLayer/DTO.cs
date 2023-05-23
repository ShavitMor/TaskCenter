using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace IntroSE.Kanban.Backend.DataAccessLayer
{
    public abstract class DTO
    {
        public const string IDCollumnName = "id";
        protected DataMapper _controller;
        public int Id { get; set; } = -1;
        public DTO(DataMapper controller) { _controller = controller; }

    }
}
