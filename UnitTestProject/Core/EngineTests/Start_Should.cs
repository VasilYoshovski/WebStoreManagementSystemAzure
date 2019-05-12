//using Microsoft.VisualStudio.TestTools.UnitTesting;
//using Moq;
//using StoreSystem.Commands.Contracts;
//using StoreSystem.Core;
//using StoreSystem.Core.Contracts;
//using StoreSystem.Core.Factories.Contracts;
//using StoreSystem.Core.Utils.Contracts;
//using StoreSystem.Menu.Contracts;

//namespace StoreSystem.Tests.Core.EngineTests
//{
//    [TestClass]
//    public class Start_Should
//    {
//        Mock<ICommandsFactory> commandsFactoryMock = new Mock<ICommandsFactory>();
//        Mock<IPrintReports> printReporstMock = new Mock<IPrintReports>();
//        Mock<IMainMenu> mainMenuMock = new Mock<IMainMenu>();
//        Mock<ICommandProcessor> parserMock = new Mock<ICommandProcessor>();
//        Mock<IReader> readerMock = new Mock<IReader>();

//        [TestMethod]
//        public void CallThreeMainMenuMethods_WhenShowLogoIsTrueAndInputTypeChooserReturnNull()
//        {
//            var sut = new Engine(
//                commandsFactoryMock.Object,
//                printReporstMock.Object,
//                mainMenuMock.Object,
//                parserMock.Object,
//                readerMock.Object
//                );
//            //Act
//            sut.Start();

//            //Assert
//            mainMenuMock.Verify(x => x.ShowLogo(), Times.Once);
//            mainMenuMock.Verify(x => x.InputTypeChooser(), Times.Once);
//            mainMenuMock.Verify(x => x.ShowCredits(), Times.Once);
//        }

//        [TestMethod]
//        public void CallTwoMainMenuMethods_WhenShowLogoIsFalseAndInputTypeChooserReturnNull()
//        {
//            var sut = new Engine(
//                commandsFactoryMock.Object,
//                printReporstMock.Object,
//                mainMenuMock.Object,
//                parserMock.Object,
//                readerMock.Object
//                );
//            //Act
//            sut.Start();

//            //Assert
//            mainMenuMock.Verify(x => x.InputTypeChooser(), Times.Once);
//            mainMenuMock.Verify(x => x.ShowCredits(), Times.Once);
//        }

//        [TestMethod]
//        public void CallProperMethods_WhenShowLogoIsTrueAndInputTypeChooserReturnReader()
//        {
               
//            var sut = new Engine(
//                commandsFactoryMock.Object,
//                printReporstMock.Object,
//                mainMenuMock.Object,
//                parserMock.Object,
//                readerMock.Object
//                );
//            //Act
//            sut.Start();

//            //Assert
//            mainMenuMock.Verify(x => x.ShowLogo(), Times.Once);
//            mainMenuMock.Verify(x => x.InputTypeChooser(), Times.Once);
//            mainMenuMock.Verify(x => x.ShowCredits(), Times.Once);
//        }
//    }
//}
