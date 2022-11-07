using System;
using System.Threading.Tasks;
using AdoFrameWork.Core.Service;
using NUnit.Framework;

namespace Ado.Test;
[TestFixture]
public class ExecScalerFunctionality
{

    [Test]
    public async Task TryInsertReturnTrue()
    {
        var connection = new ConnectionImpl("");
       await connection.BuildConnection("Server=.;Database=master;Trusted_Connection=True;");
        var values = new[]{"15","sidkk"};
        var result = await connection.InsertParser(values,new[]{"id","name"},"t2");
        Assert.That(true, Is.EqualTo(result));
    }


     [Test]
    public async Task TryInsertReturnFalse()
    {
        var connection = new ConnectionImpl("");
       await connection.BuildConnection("Server=.;Database=master;Trusted_Connection=True;");
        var values = new[]{"15","sidkk"};
        var result = await connection.InsertParser(values,new[]{"id","name"},"t3");
        Assert.That(false, Is.EqualTo(result));
    }
   
}