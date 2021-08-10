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
        // ìîê íà ä³ð³êòîð³.
        [Fact]
        public void Test1()
        {
            var config = new Config();
            string directoryName = "MyFolder";

            //var mockDirectory = new MockDirectory();

            var mockFileSystem = new MockFileSystem(new Dictionary<string, MockFileData>
            {
                { @"c:\myfile.xml", new MockFileData(@"
                <?xml version=""1.0""?>
                <ÑompositionProc xmlns:xsi=""http://www.w3.org/2001/XMLSchema-instance"" xmlns:xsd=""http://www.w3.org/2001/XMLSchema"">
                  <Name>msedge</Name>
                  <Link> C:\Program Files (x86)\Microsoft\Edge\Application\msedge.exe </ Link >
                 < AutorunTrack >
                   < Track > true </ Track >
                 </ AutorunTrack >
                </ ÑompositionProc > ") },
            });

            var directoryInfo = new MockDirectoryInfo(mockFileSystem, directoryName);
            config.Folder = directoryName;
            var autorun = new AutorunTrack(mockFileSystem);
            autorun.Traced(new List<ShortProcess>(), config);
            var list = new List<ShortProcess>();

        }
    }
}
