using System;
using System.Threading.Tasks;
using AdoFrameWork.Core.Service;
using Moq;
using NUnit.Framework;

namespace Ado.Test;
[TestFixture]
public class ExecScalerFunctionality
{

    [Test]
    public async Task TryInsertReturnTrue()
    {
        var connection = new Mock<ConnectionImpl>();
        connection.Setup((x) => x.BuildConnection(It.IsAny<string>()))
        .ReturnsAsync(true);

        connection.Setup(x => x.InsertParser(It.IsAny<string[]>(), It.IsAny<string[]>(), It.IsAny<string>()))
        .ReturnsAsync(true);

        Assert.That(true, Is.EqualTo(connection.Object.InsertParser(new string[] { }, new string[] { }, "").Result));
    }


    [Test]
    public async Task TryInsertReturnFalse()
    {
        var connection = new Mock<ConnectionImpl>();

        connection.Setup(x => x.BuildConnection("Server=.;Database=master;Trusted_Connection=True;"))
       .Returns(Task.FromResult(true));

        connection.Setup(x => x.InsertParser(new string[] { }, new string[] { }, ""))
        .Returns(Task.FromResult(false));
        Assert.That(false, Is.EqualTo(connection.Object.InsertParser(new string[] { }, new string[] { }, "").Result));
    }

}