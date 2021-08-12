using System;
using Xunit;
using Watcher;
using System.IO.Abstractions;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using LibaryCore;


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
            var action = new FakeActionsProceses();
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

            var sut = new AutorunTrack(newMockFileSystem, action);

            //act & assert 
            Assert.Throws<NotImplementedException>(() => sut.Traced(new List<ShortProcess>(), config));
        }
    }
}
