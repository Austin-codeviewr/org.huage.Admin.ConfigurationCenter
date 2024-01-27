using org.huage.Admin.Configuration.Entity.Service;

namespace org.huage.Admin.Configuration.Manager.Service;

public class TestServices : ITestServices,ITransient
{
    public int GetValue()
    {
        return 100;
    }
}