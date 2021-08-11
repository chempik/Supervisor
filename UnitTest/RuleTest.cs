using System;
using Xunit;
using Watcher;
using System.IO.Abstractions;
using System.Collections.Generic;
using System.IO.Abstractions.TestingHelpers;
using LibaryCore;
using Moq;
using System.IO;

namespace UnitTest
{
    public class RuleTest
    {
        [Fact]
        public void Test1()
        {
            var config = new Config();
            string directoryName = @"C:\Target";

            var newMockFileSystem = new MockFileSystem();
            newMockFileSystem.AddDirectory(@"C:\Target");
            newMockFileSystem.AddFile(@"C:\Target\msedge.xml", new MockFileData(@"
                <?xml version=""1.0""?>
                <ÑompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </ Link >
                 <AuTorunProc>
                   < Autorun > true </ Autorun >
                 </ AuTorunProc >
                </ ÑompositionProc > "));

            config.Folder = directoryName;
            var autorun = new AutorunTrack(newMockFileSystem);
            // var cut = new MockFileStream(newMockFileSystem, directoryName, FileMode.Open);
            autorun.Traced(new List<ShortProcess>(), config);

        }
    }
}
