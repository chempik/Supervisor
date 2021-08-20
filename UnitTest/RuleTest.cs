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
                <СompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <AutorestartProc>
                   <Restart> true </Restart >
                 </AutorestartProc>
                  <AuTorunProc>
                  <Autorun> true </Autorun>
                  </AuTorunProc>
                </СompositionProc>"));

            var action = Substitute.For<IActionsProceses>();

            var sutAutorun = new AutorunTrack(newMockFileSystem, action);

            var sutAutorestart = new AutorestartTrack(newMockFileSystem, action);

            var list = new List<ShortProcess>();

            list.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));

            //act
            sutAutorun.Traced(list, config);
            sutAutorestart.Traced(list, config);
            list.Clear();
            sutAutorestart.Traced(list, config);

            //assert 
            action.Received(2).Start(@" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe ");
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
                <СompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <AutorestartProc>
                   <Restart> true </Restart >
                 </AutorestartProc>
                  <AuTorunProc>
                  <Autorun> true </Autorun>
                  </AuTorunProc>
                 <TrackProc>
                   <Track> 1 </Track >
                 </TrackProc>
                </СompositionProc>"));

            var action = Substitute.For<IActionsProceses>();

            var sutAutorun = new AutorunTrack(newMockFileSystem, action);

            var sutAutorestart = new AutorestartTrack(newMockFileSystem, action);

            var sutNumerosity = new NumerosityTrack(newMockFileSystem, action);

            var list = new List<ShortProcess>();

            list.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));
            list.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));

            //act
            sutAutorun.Traced(list, config);
            sutAutorestart.Traced(list, config);
            sutNumerosity.Traced(list, config);
            list.Clear();
            sutAutorestart.Traced(list, config);
            

            //assert 
            action.Received(2).Start(@" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe ");
            action.Received(1).KillOneProcess("msedge");
        }
    }
}
