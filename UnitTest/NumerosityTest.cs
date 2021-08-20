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
    public class NumerosityTest
    {
        [Fact]
        public void OneFileNumerosityTest()
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
                 <TrackProc>
                   <Track> 1 </Track >
                 </TrackProc>
                </СompositionProc>"));
            var action = Substitute.For<IActionsProceses>();
            var sut = new NumerosityTrack(newMockFileSystem, action);
            var proc = new List<ShortProcess>();
            proc.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));
            proc.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));
            proc.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));

            //act 
            sut.Traced(proc, config);

            //assert 
            action.Received(2).KillOneProcess("msedge");
        }

        [Fact]
        public void TwoFileNumerosityTest()
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
                 <TrackProc>
                   <Track> 1 </Track >
                 </TrackProc>
                </СompositionProc>"));
            newMockFileSystem.AddFile(@"C:\Target\opera.xml", new MockFileData(
                @"<?xml version=""1.0""?>
                <СompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>opera</Name>
                  <Link> C:\Program Files (x86)\opera\Application\opera.exe </Link >
                 <TrackProc>
                   <Track> 1 </Track >
                 </TrackProc>
                </СompositionProc>"));
            var action = Substitute.For<IActionsProceses>();
            var sut = new NumerosityTrack(newMockFileSystem, action);
            var proc = new List<ShortProcess>();
            proc.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));
            proc.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));
            proc.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));
            proc.Add(new ShortProcess("opera", 00, 00, @" C:\Program Files (x86)\opera\Application\opera.exe "));
            proc.Add(new ShortProcess("opera", 00, 00, @" C:\Program Files (x86)\opera\Application\opera.exe "));
            proc.Add(new ShortProcess("opera", 00, 00, @" C:\Program Files (x86)\opera\Application\opera.exe "));

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
                <СompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <TrackProc>
                   <Track> -1 </Track >
                 </TrackProc>
                </СompositionProc>"));
            var action = Substitute.For<IActionsProceses>();
            var sut = new NumerosityTrack(newMockFileSystem, action);
            var proc = new List<ShortProcess>();
            proc.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));
            proc.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));
            proc.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));


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
                <СompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </Link >
                 <TrackProc>
                   <Track> 0 </Track >
                 </TrackProc>
                </СompositionProc>"));
            var action = Substitute.For<IActionsProceses>();
            var sut = new NumerosityTrack(newMockFileSystem, action);
            var proc = new List<ShortProcess>();
            proc.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));
            proc.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));
            proc.Add(new ShortProcess("msedge", 00, 00, @" C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe "));


            //act 
            sut.Traced(proc, config);

            //assert 
            action.Received(3).KillOneProcess("msedge");
        }
    }
}
