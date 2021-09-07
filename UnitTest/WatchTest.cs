using System;
using Xunit;
using Watcher;
using System.IO.Abstractions;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using LibaryCore;
using NSubstitute;
using Watcher.Interfece;

namespace UnitTest
{
    public class WatchTest
    {
        private Config MockConfig()
        {
            var config = new Config();
            config.Folder = @"C:\Target";
            return config;
        }

        private MockFileSystem Mock()
        {
            var newMockFileSystem = new MockFileSystem();
            newMockFileSystem.AddDirectory(@"C:\Target");

            newMockFileSystem.AddFile(@"C:\Target\msedge.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <СompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <TrackProc>
                   <Track> 3 </Track >
                 </TrackProc>
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

            return newMockFileSystem;
        }

        [Fact]
        public void StartWatchTest()
        {
            // arrange
            bool start = false;
            var foo = Substitute.For<IDeserializeComposition>();
            foo.CheckProceses(@"C:\Target").Returns(
            x => new List<ShortProcess>
            {
                new ShortProcess("msedge", 1, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "),
            });

            var action = Substitute.For<IActionsProceses>();
            var sut = new Watch(MockConfig(), foo, Mock(), action);

            sut.Started += delegate (object sender, ProcesesEventArgs e)
            {
                start = true;
            };

            //act
            sut.Start();

            //assert
            Assert.True(start);
        }

        [Fact]
        public void StartProcTest()
        {
            // arrange
            bool open = false;
            var foo = Substitute.For<IDeserializeComposition>();
            foo.CheckProceses(@"C:\Target").Returns(
            x => new List<ShortProcess>
            {
                new ShortProcess("msedge", 1, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "),
                new ShortProcess("msedge", 2, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "),
                new ShortProcess("opera", 3, 00, @" C:\Program Files (x86)\opera\Application\opera.exe "),
            },
            x => new List<ShortProcess>
            {
                new ShortProcess("msedge", 1, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "),
                new ShortProcess("msedge", 2, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "),
                new ShortProcess("opera", 3, 00, @" C:\Program Files (x86)\opera\Application\opera.exe "),
                new ShortProcess("opera", 4, 00, @" C:\Program Files (x86)\opera\Application\opera.exe "),
            });

            var action = Substitute.For<IActionsProceses>();
            var sut = new Watch(MockConfig(), foo, Mock(), action);

            sut.Opened += delegate (object sender, ProcesesEventArgs e)
            {
                open = true;
            };

            //act
            sut.Start();
            sut.Start();

            //assert
            Assert.True(open);
        }
        
        [Fact]
        public void EndProcTest()
        {
            // arrange
            bool end = false;
            var foo = Substitute.For<IDeserializeComposition>();
            foo.CheckProceses(@"C:\Target").Returns(
            x => new List<ShortProcess>
            {
                new ShortProcess("msedge", 1, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "),
                new ShortProcess("msedge", 2, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "),
                new ShortProcess("opera", 3, 00, @" C:\Program Files (x86)\opera\Application\opera.exe "),
            },
            x => new List<ShortProcess>
            {
                new ShortProcess("msedge", 1, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe ")
            });

            var action = Substitute.For<IActionsProceses>();
            var sut = new Watch(MockConfig(), foo, Mock(), action);

            sut.Ended += delegate (object sender, ProcesesEventArgs e)
            {
                end = true;
            };

            //act
            sut.Start();
            sut.Start();

            //assert
            Assert.True(end);
        }
    }
}
