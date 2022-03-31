using DAL;
using Model;

namespace Demo;
public class Program
{
    public static void Main(string[] args)
    {
        try
        {
            Console.WriteLine("*********常规单体查询*********");

            SqlHelper sqlHelper = new SqlHelper();
            Company company = sqlHelper.Find<Company>(1);
            Console.WriteLine(company.Id +":"+company.Name);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.Read();
    }
}

