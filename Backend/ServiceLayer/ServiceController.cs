using System;
using IntroSE.Kanban.Backend.BusinessLayer;
using log4net.Config;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class ServiceController
    {

        private static UserController uc = new();
        private static BoardController bc = new();
        private DataService DS;
        public UserService US;
        public BoardService BS;
        log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public ServiceController()
        {
            XmlConfigurator.Configure(new System.IO.FileInfo("log4net.config"));
            DS = ServiceFactory.MakeDataService();
            US = ServiceFactory.MakeUserService(uc);
            BS = ServiceFactory.MakeBoardService(bc, uc);

        }


        ///<summary>This method loads all persisted data.
        ///<para>
        ///<b>IMPORTANT:</b> Method is called on launching the program.
        /// Loads all data currently on DB.
        ///</para>
        /// </summary>
        /// <returns>An empty response, unless an error occurs (see <see cref="DataService"/>)</returns>
        public string LoadData()
        {
            return DS.LoadData(bc, uc);
        }

        ///<summary>This method deletes all persisted data.
        ///<para>
        ///<b>IMPORTANT:</b> 
        ///Deletes all persistent data, as well as deletes all data from current UserCotroller and BoardController. Method is never called automatically.
        ///</para>
        /// </summary>
        ///<returns>An empty response, unless an error occurs (see <see cref="DataService"/>)</returns>
        public string DeleteData()
        {

            return DS.DeleteData(bc, uc);

        }
    }
}

