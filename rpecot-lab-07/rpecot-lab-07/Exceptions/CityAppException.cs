namespace rpecot_lab_07.Exceptions
{
    public class CityAppException : Exception
    {
    }

    public class CityAppException_UserError : Exception
    {
        public CityAppException_UserError(string message) : base(message)
        {

        }
    }

    public class CityAppException_InternalError : Exception
    {
        CityAppException_InternalError(string message) : base(message)
        {

        }
    }
}
