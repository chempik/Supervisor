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
    public class AutorunTest
    {
        [Fact]
        public void OneFileAutorunTest()
        {
            // arrange
            var config = new Config();
            string directoryName = @"C:\Target";
            config.Folder = directoryName;

            var newMockFileSystem = new MockFileSystem();
            newMockFileSystem.AddDirectory(@"C:\Target");

            newMockFileSystem.AddFile(@"C:\Target\msedge.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <СompositionProc xmlns: xsi = ""http://www.w3.org/2001/XMLSchema-instance"" xmlns: xsd = ""http://www.w3.org/2001/XMLSchema"" >
                  <Name>msedge</Name>
                  <Link>C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe</Link>
                  <AuTorunProc>
                  <Autorun>true</Autorun>
                  </AuTorunProc>
                  </СompositionProc>"));

            var action = Substitute.For<IActionsProceses>();

            var sut = new AutorunTrack(newMockFileSystem, action);

            // act 
            sut.Traced(new List<ShortProcess>(), config);

            //assert
            action.Received(1).Start(@" C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe ");
        }

        [Fact]
        public void TwoAutorunTest()
        {
            // arrange
            var config = new Config();
            string directoryName = @"C:\Target";
            config.Folder = directoryName;

            var newMockFileSystem = new MockFileSystem();
            newMockFileSystem.AddDirectory(@"C:\Target");

            newMockFileSystem.AddFile(@"C:\Target\msedge.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <СompositionProc xmlns: xsi = ""http://www.w3.org/2001/XMLSchema-instance"" xmlns: xsd = ""http://www.w3.org/2001/XMLSchema"" >
                  <Name>msedge</Name>
                  <Link>C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe</Link>
                  <AuTorunProc>
                  <Autorun>true</Autorun>
                  </AuTorunProc>
                  </СompositionProc>"));

            newMockFileSystem.AddFile(@"C:\Target\opera.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <СompositionProc xmlns: xsi = ""http://www.w3.org/2001/XMLSchema-instance"" xmlns: xsd = ""http://www.w3.org/2001/XMLSchema"" >
                  <Name>opera</Name>
                  <Link>C:\Program Files(x86)\opera\Application\opera.exe</Link>
                  <AuTorunProc>
                  <Autorun>true</Autorun>
                  </AuTorunProc>
                  </СompositionProc>"));

            var action = Substitute.For<IActionsProceses>();

            var sut = new AutorunTrack(newMockFileSystem, action);

            // act 
            sut.Traced(new List<ShortProcess>(), config);

            //assert
            action.Received(1).Start(@" C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe ");
            action.Received(1).Start(@" C:\Program Files(x86)\opera\Application\opera.exe ");
        }

        [Fact]
        public void ReAutorunTest()
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
                  <Link> C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <AuTorunProc>
                   <Autorun> true </Autorun >
                 </AuTorunProc>
                </СompositionProc>"));

            var action = Substitute.For<IActionsProceses>();

            var sut = new AutorunTrack(newMockFileSystem, action);

            // act 
            sut.Traced(new List<ShortProcess>(), config);
            sut.Traced(new List<ShortProcess>(), config);
            sut.Traced(new List<ShortProcess>(), config);

            //assert
            action.Received(1).Start(@" C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe ");
        }
    }
}
