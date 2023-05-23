using IntroSE.Kanban.Backend.BusinessLayer;
/*using IntroSE.Kanban.Backend.ServiceLayer.ServiceClasses;*/
using System;
using System.Text.Json;

namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class DataService
    {

        Response response;
        private BusinessInitialize BusinessInitialize = new();
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);



        public string LoadData(BoardController bc, UserController uc)
        {
            log.Info("Loading all Data...");
            try
            {
                BusinessInitialize.LoadData(uc, bc);
                log.Info("Data Loaded succesfully");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Fatal("Failed loading data");
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }

        public string DeleteData(BoardController bc, UserController uc)
        {
            log.Info("Deleting all Data...");
            try
            {
                BusinessInitialize.DeleteData(bc, uc);
                log.Info("Data deleted succesfully");
                return JsonSerializer.Serialize(Response.Empty);
            }
            catch (Exception ex)
            {
                log.Error(ex.Message);
                log.Fatal("Failed deleted data");
                return JsonSerializer.Serialize(Response.ResponseError(ex.Message));
            }
        }
    }
}
