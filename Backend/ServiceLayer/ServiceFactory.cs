using IntroSE.Kanban.Backend.BusinessLayer;


namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class ServiceFactory
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public static BoardService MakeBoardService(BoardController bc, UserController uc)
        {
            log.Info("Creating new BoardService....");
            BoardController.UC = uc;
            return new BoardService(bc);

        }
        public static UserService MakeUserService(UserController uc)
        {
            log.Info("Creating new UserService...");
            return new UserService(uc);
        }

        public static DataService MakeDataService()
        {
            log.Info("Creating new DataService...");
            return new DataService();
        }

        public static ServiceController MakeServiceController()
        {
            log.Info("Creating new ServiceController...");
            return new ServiceController();
        }
    }
}
