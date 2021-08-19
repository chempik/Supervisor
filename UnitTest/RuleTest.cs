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
                  <Link> C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <AutorestartProc>
                   <Restart> true </Restart >
                 </AutorestartProc>
                </ÑompositionProc>"));

            var action = Substitute.For<IActionsProceses>();

            var sut = new AutorestartTrack(newMockFileSystem, action);

            var list = new List<ShortProcess>();

            list.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe "));

            //act
            sut.Traced(list, config);
            list.Clear();
            sut.Traced(list, config);

            //assert 
            action.Received(1).Start(@" C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe ");
        }

        [Fact]
        public void NumerosityTest()
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
                  <Link> C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <TrackProc>
                   <Track> 1 </Track >
                 </TrackProc>
                </ÑompositionProc>"));
            var action = Substitute.For<IActionsProceses>();
            var sut = new NumerosityTrack(newMockFileSystem, action);
            var proc = new List<ShortProcess>();
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe"));
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files x86)\Microsoft\Edge\Application\msedge.exe"));
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe"));

            //act 
            sut.Traced(proc, config);

            //assert 
            action.Received(2).KillOneProcess("msedge");
        }

        [Fact]
        public void TwoNumerosityTest()
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
                  <Link> C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <TrackProc>
                   <Track> 1 </Track >
                 </TrackProc>
                </ÑompositionProc>"));
            newMockFileSystem.AddFile(@"C:\Target\opera.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <СompositionProc xmlns: xsi = ""http://www.w3.org/2001/XMLSchema-instance"" xmlns: xsd = ""http://www.w3.org/2001/XMLSchema"" >
                  <Name>opera</Name>
                  <Link>C:\Program Files(x86)\opera\Application\opera.exe</Link>
                 <TrackProc>
                   <Track> 1 </Track >
                 </TrackProc>
                  </СompositionProc>"));
            var action = Substitute.For<IActionsProceses>();
            var sut = new NumerosityTrack(newMockFileSystem, action);
            var proc = new List<ShortProcess>();
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe"));
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files x86)\Microsoft\Edge\Application\msedge.exe"));
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe"));
            proc.Add(new ShortProcess("opera", 00, 00, @"C:\Program Files(x86)\opera\Application\opera.exe"));
            proc.Add(new ShortProcess("opera", 00, 00, @"C:\Program Files(x86)\opera\Application\opera.exe"));
            proc.Add(new ShortProcess("opera", 00, 00, @"C:\Program Files(x86)\opera\Application\opera.exe"));

            //act 
            sut.Traced(proc, config);

            //assert 
            action.Received(2).KillOneProcess("msedge");
            action.Received(2).KillOneProcess("opera");
        }

        [Fact]
        public void MinusNumerosityTest()
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
                  <Link> C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <TrackProc>
                   <Track> -1 </Track >
                 </TrackProc>
                </ÑompositionProc>"));
            var action = Substitute.For<IActionsProceses>();
            var sut = new NumerosityTrack(newMockFileSystem, action);
            var proc = new List<ShortProcess>();
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe"));
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files x86)\Microsoft\Edge\Application\msedge.exe"));
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe"));


            //act 
            sut.Traced(proc, config);

            //assert 
            action.Received(3).KillOneProcess("msedge");
        }

        [Fact]
        public void ZeroNumerosityTest()
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
                  <Link> C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <TrackProc>
                   <Track> 0 </Track >
                 </TrackProc>
                </ÑompositionProc>"));
            var action = Substitute.For<IActionsProceses>();
            var sut = new NumerosityTrack(newMockFileSystem, action);
            var proc = new List<ShortProcess>();
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe"));
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files x86)\Microsoft\Edge\Application\msedge.exe"));
            proc.Add(new ShortProcess("msedge", 00, 00, @"C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe"));


            //act 
            sut.Traced(proc, config);

            //assert 
            action.Received(3).KillOneProcess("msedge");
        }

        [Fact]
        public void ComboAutorunAutorestartTest()
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
                  <Link> C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <AutorestartProc>
                   <Restart> true </Restart >
                 </AutorestartProc>
                  <AuTorunProc>
                  <Autorun>true</Autorun>
                  </AuTorunProc>
                </ÑompositionProc>"));

            var action = Substitute.For<IActionsProceses>();

            var sutAutorun = new AutorunTrack(newMockFileSystem, action);

            var sutAutorestart = new AutorestartTrack(newMockFileSystem, action);

            var list = new List<ShortProcess>();

            list.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe "));

            //act
            sutAutorun.Traced(list, config);
            sutAutorestart.Traced(list, config);
            list.Clear();
            sutAutorestart.Traced(list, config);

            //assert 
            action.Received(2).Start(@" C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe ");
        }

        [Fact]
        public void ComboAllTest()
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
                  <Link> C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <AutorestartProc>
                   <Restart> true </Restart >
                 </AutorestartProc>
                  <AuTorunProc>
                  <Autorun>true</Autorun>
                  </AuTorunProc>
                 <TrackProc>
                   <Track> 1 </Track >
                 </TrackProc>
                </ÑompositionProc>"));

            var action = Substitute.For<IActionsProceses>();

            var sutAutorun = new AutorunTrack(newMockFileSystem, action);

            var sutAutorestart = new AutorestartTrack(newMockFileSystem, action);

            var sutNumerosity = new NumerosityTrack(newMockFileSystem, action);

            var list = new List<ShortProcess>();

            list.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe "));
            list.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe "));

            //act
            sutAutorun.Traced(list, config);
            sutAutorestart.Traced(list, config);
            sutNumerosity.Traced(list, config);
            list.Clear();
            sutAutorestart.Traced(list, config);
            

            //assert 
            action.Received(2).Start(@" C:\Program Files(x86)\Microsoft\Edge\Application\msedge.exe ");
            action.Received(1).KillOneProcess("msedge");
        }
    }
}
