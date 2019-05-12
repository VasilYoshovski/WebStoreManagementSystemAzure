//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using System.Collections.Generic;
//using System.Linq;
//using StoreSystem.Core.Contracts;
//using StoreSystem.Core.Factories.Contracts;
//using StoreSystem.Menu.Contracts;
//using StoreSystem.Core;
//using StoreSystem.Commands.Contracts;
//using StoreSystem.Core.Utils.Contracts;

//namespace StoreSystem.Tests.Core.EngineTests
//{

//    public class Run_Should
//    {
//        Mock<ICommandsFactory> commandsFactoryMock = new Mock<ICommandsFactory>();
//        Mock<IPrintReports> printReporstMock = new Mock<IPrintReports>();
//        Mock<IMainMenu> mainMenuMock = new Mock<IMainMenu>();
//        Mock<ICommandProcessor> parserMock = new Mock<ICommandProcessor>();
//        Mock<IReader> readerMock = new Mock<IReader>();

//        [TestMethod]
//        [Timeout(2000)]
//        public void ReturnProperMessage_WhenValidParameteraArePassed()
//        {
//            var validCommandName = "Valid Command";
//            var validParameters = new List<string>{ "ValidParameters"};
//            var validInputString = validCommandName + " " + string.Join(",", validParameters);
//            var inputsListFake = new List<string>()
//            {
//                validInputString,
//                "AppExit"
//            };
//            readerMock.Setup(x => x.Read()).Returns(inputsListFake);
//            var expextedOutput = "Success";


//            var commandMock = new Mock<ICommand>();
//            commandMock.SetupGet(x => x.Name).Returns(validCommandName);
//            commandMock.SetupGet(x => x.Parameters).Returns(validParameters);

//            parserMock.Setup(x => x.Parse(validInputString)).Returns(commandMock.Object);

//            var engineCommandMock = new Mock<IEngineCommand>();
//            commandsFactoryMock.Setup(x => x.GetCommand(validCommandName)).Returns(engineCommandMock.Object);

//            var reportListFake = new List<string>();
//            printReporstMock.Setup(x => x.Reports).Returns(reportListFake);

//            engineCommandMock.Setup(x => x.Execute(validParameters)).Returns(expextedOutput);

//            var sut = new Engine(
//            commandsFactoryMock.Object,
//            printReporstMock.Object,
//            mainMenuMock.Object,
//            parserMock.Object,
//            readerMock.Object
//            );

//            //Act
//            //sut.Run(readerMock.Object);

//            //Assert
//            Assert.AreEqual(expextedOutput, printReporstMock.Object.Reports.Single());
//        }
//    }
//}
