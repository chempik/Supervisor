using System;
using Xunit;
using Watcher;
using System.IO.Abstractions;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using LibaryCore;
using NSubstitute;


namespace UnitTest
{
    public class AutorestartTest
    {
        [Fact]
        public void OneFileAutorestartTest()
        {
            // arrange
            var config = new Config();
            string directoryName = @"C:\Target";
            config.Folder = directoryName;

            var newMockFileSystem = new MockFileSystem();
            newMockFileSystem.AddDirectory(@"C:\Target");
            newMockFileSystem.AddFile(@"C:\Target\msedge.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <СompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <AutorestartProc>
                   <Restart> true </Restart >
                 </AutorestartProc>
                </СompositionProc>"));

            var action = Substitute.For<IActionsProceses>();
            var sut = new AutorestartTrack(newMockFileSystem, action);
            var list = new List<ShortProcess>();
            list.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe "));

            //act
            sut.Traced(list, config);
            list.Clear();
            sut.Traced(list, config);

            //assert 
            action.Received(1).Start(@" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe ");
        }

        [Fact]
        public void TwoFileAutorestartTest()
        {
            // arrange
            var config = new Config();
            string directoryName = @"C:\Target";
            config.Folder = directoryName;

            var newMockFileSystem = new MockFileSystem();
            newMockFileSystem.AddDirectory(@"C:\Target");
            newMockFileSystem.AddFile(@"C:\Target\msedge.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <СompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <AutorestartProc>
                   <Restart> true </Restart >
                 </AutorestartProc>
                </СompositionProc>"));

            newMockFileSystem.AddFile(@"C:\Target\opera.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <СompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>opera</Name>
                  <Link> C:\Program Files (x86)\opera\Application\opera.exe </Link >
                 <AutorestartProc>
                   <Restart> true </Restart >
                 </AutorestartProc>
                </СompositionProc>"));

            var action = Substitute.For<IActionsProceses>();
            var sut = new AutorestartTrack(newMockFileSystem, action);
            var list = new List<ShortProcess>();
            list.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));
            list.Add(new ShortProcess("opera", 00, 00, @" C:\Program Files (x86)\opera\Application\opera.exe "));

            //act
            sut.Traced(list, config);
            list.Clear();
            sut.Traced(list, config);

            //assert 
            action.Received(1).Start(@" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe ");
            action.Received(1).Start(@" C:\Program Files (x86)\opera\Application\opera.exe ");
        }

        [Fact]
        public void OneProcesDontStartAutorestartTest()
        {
            // arrange
            var config = new Config();
            string directoryName = @"C:\Target";
            config.Folder = directoryName;

            var newMockFileSystem = new MockFileSystem();
            newMockFileSystem.AddDirectory(@"C:\Target");
            newMockFileSystem.AddFile(@"C:\Target\msedge.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <СompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <AutorestartProc>
                   <Restart> true </Restart >
                 </AutorestartProc>
                </СompositionProc>"));

            newMockFileSystem.AddFile(@"C:\Target\opera.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <СompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>opera</Name>
                  <Link> C:\Program Files (x86)\opera\Application\opera.exe </Link >
                 <AutorestartProc>
                   <Restart> true </Restart >
                 </AutorestartProc>
                </СompositionProc>"));

            var action = Substitute.For<IActionsProceses>();
            var sut = new AutorestartTrack(newMockFileSystem, action);
            var list = new List<ShortProcess>();
            list.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));

            //act
            sut.Traced(list, config);
            list.Clear();
            sut.Traced(list, config);

            //assert 
            action.Received(1).Start(@" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe ");
            action.DidNotReceive().Start(@" C:\Program Files (x86)\opera\Application\opera.exe ");
        }
    }
}
