namespace IntroSE.Kanban.Backend.ServiceLayer
{
    public class Response
    {
        public string ErrorMessage { get; set; }
        public object ReturnValue { get; set; }

        private Response() { }

        public static Response Empty = new Response();

        public static Response ResponseError(string msg)
        {
            Response res = new Response();
            res.ErrorMessage = msg;
            return res;
        }

        public static Response ResponseValue(object val)
        {
            Response res = new Response();
            res.ReturnValue = val;
            return res;
        }

        public bool IsValid()
        {
            if (this.ErrorMessage != null && this.ReturnValue != null)
            {
                return false;
            }
            return true;
        }

        public bool IsError()
        {
            if (this.ErrorMessage != null)
            {
                return true;
            }
            return false;
        }
    }
}
