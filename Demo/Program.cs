using DAL;
using Model;

namespace Demo;
public class Program
{
    public static void Main(string[] args)
    {
        try
        {

            SqlHelper sqlHelper = new SqlHelper();
            Company company = sqlHelper.Find<Company>(2);
            Console.WriteLine(company.Id +":"+company.Name);

            bool flag = sqlHelper.Insert<Company>(company);
            Console.WriteLine(flag);

            company.LastModifyTime = DateTime.Now;
            bool updateFlag = sqlHelper.Update<Company>(company);
            Console.WriteLine(updateFlag);

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.Read();
    }
}

