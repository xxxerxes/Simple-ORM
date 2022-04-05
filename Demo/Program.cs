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
            Company company = sqlHelper.Find<Company>(4);
            //Console.WriteLine(company.Id + ":" + company.Name);

            //bool flag = sqlHelper.Insert<Company>(company);
            //Console.WriteLine(flag);

            //company.LastModifyTime = DateTime.Now;
            //bool updateFlag = sqlHelper.Update<Company>(company);
            //Console.WriteLine(updateFlag);
            //bool deleteFlag = sqlHelper.Delete<Company>(company);
            //Console.WriteLine(deleteFlag);
            //Company company = sqlHelper.FindByCondition<Company>(x => x.Name.StartsWith("t") && x.Name.Contains("es") && x.Name.EndsWith("t"));

        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.Message);
        }
        Console.Read();
    }
}

