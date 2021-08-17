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
    public class RuleTest
    {
        [Fact]
        public void AutorunTest()
        {
            // arrange
            var config = new Config();
            string directoryName = @"C:\Target";
            //var action = new FakeActionsProceses();
            config.Folder = directoryName;

            var newMockFileSystem = new MockFileSystem();
            newMockFileSystem.AddDirectory(@"C:\Target");
            newMockFileSystem.AddFile(@"C:\Target\msedge.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <ÑompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <AuTorunProc>
                   <Autorun> true </Autorun >
                 </AuTorunProc>
                </ÑompositionProc>"));
            var action = Substitute.For<IActionsProceses>();
            action.Start(default).ReturnsForAnyArgs(x => new ShortProcess());
            var sut = new AutorunTrack(newMockFileSystem, action);
            sut.Traced(new List<ShortProcess>(), config);
            
            //act & assert 
            Assert.Equal(new ShortProcess(), action.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"));
        }

        [Fact]
        public void AutorestartTest()
        {
            // arrange
            var config = new Config();
            string directoryName = @"C:\Target";
            config.Folder = directoryName;

            var newMockFileSystem = new MockFileSystem();
            newMockFileSystem.AddDirectory(@"C:\Target");
            newMockFileSystem.AddFile(@"C:\Target\msedge.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <ÑompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <AutorestartProc>
                   <Restart> true </Restart >
                 </AutorestartProc>
                </ÑompositionProc>"));
            var action = Substitute.For<IActionsProceses>();
            action.Start(default).ReturnsForAnyArgs(x => new ShortProcess());
            var sut = new AutorestartTrack(newMockFileSystem, action);

            //act & assert 
            Assert.Equal(new ShortProcess(), action.Start(@"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"));
        }

        [Fact]
        public void NumerosityTest()
        {
            // arrange
            var config = new Config();
            string directoryName = @"C:\Target";
            config.Folder = directoryName;
            var proof = false;

            var newMockFileSystem = new MockFileSystem();
            newMockFileSystem.AddDirectory(@"C:\Target");
            newMockFileSystem.AddFile(@"C:\Target\msedge.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <ÑompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <TrackProc>
                   <Track> 1 </Track >
                 </TrackProc>
                </ÑompositionProc>"));
            var action = Substitute.For<IActionsProceses>();
            action.KillOneProcess(default).ReturnsForAnyArgs(x => true).AndDoes(x => proof = true);
            var sut = new NumerosityTrack(newMockFileSystem, action);
            var proc = new List<ShortProcess>();

            //act 
            proc.Add(new ShortProcess ("msedge",00,00, @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"));
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe"));
            sut.Traced(proc, config);

            //assert 
            Assert.True(proof);
        }
    }
}
