using Everlasting.Core;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Everlasting.Tests
{
    [TestClass]
    public class ArgumentParserTests
    {
        private string _exePath;
        private string _workingDir;

        [TestInitialize]
        public void Initialize()
        {
            _workingDir = @"C:\Development\Github\Everlasting\src\Everlasting\bin\Debug\net461\win7-x64";
            _exePath = _workingDir + @"\Everlasting.exe";
        }

        [TestMethod]
        public void CanIdentifyNoArgs()
        {
            var args = new string[] { };
            var command = args.Parse();
            Assert.IsNull(command);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownCommandTypeException))]
        public void CanIdentifyInvalidCommandType()
        {
            var args = new string[] { "terminate" };
            var command = args.Parse();
        }

        [TestMethod]
        public void CanIdentifyValidCommand1()
        {
            var args = new string[] { "start", _exePath };
            var command = args.Parse();
            Assert.IsNotNull(command);
            Assert.AreEqual("start", command.Type);
            Assert.AreEqual(_exePath, command.ExecutablePath);
        }

        [TestMethod]
        public void CanIdentifyValidCommand2()
        {
            var args = new string[] { "start", _exePath, "-w", _workingDir, "-s" };
            var command = args.Parse();
            Assert.IsNotNull(command);
            Assert.AreEqual("start", command.Type);
            Assert.AreEqual(_exePath, command.ExecutablePath);
            Assert.AreEqual(_workingDir, command.WorkingDirectory);
            Assert.AreEqual(true, command.SpareChildProcesses);
        }

        [TestMethod]
        public void CanIdentifyValidCommand3()
        {
            var args = new string[] { "start", _exePath, "--working-dir", _workingDir, "-s" };
            var command = args.Parse();
            Assert.IsNotNull(command);
            Assert.AreEqual("start", command.Type);
            Assert.AreEqual(_exePath, command.ExecutablePath);
            Assert.AreEqual(_workingDir, command.WorkingDirectory);
            Assert.AreEqual(true, command.SpareChildProcesses);
        }

        [TestMethod]
        public void CanIdentifyValidCommand4()
        {
            var args = new string[] { "start", _exePath, "--working-dir", _workingDir, "--spare-child-processes", "--exe-args", "argument1" };
            var command = args.Parse();
            Assert.IsNotNull(command);
            Assert.AreEqual("start", command.Type);
            Assert.AreEqual(_exePath, command.ExecutablePath);
            Assert.AreEqual(_workingDir, command.WorkingDirectory);
            Assert.AreEqual(true, command.SpareChildProcesses);
            Assert.AreEqual("argument1", command.ExecutableArgs);
        }

        [TestMethod]
        public void CanAllowExcludedExesFromValidation1()
        {
            var args = new string[] { "start", "dotnet run", "--working-dir", _workingDir };
            var command = args.Parse();
            Assert.IsNotNull(command);
            Assert.AreEqual("start", command.Type);
            Assert.AreEqual("dotnet run", command.ExecutablePath);
            Assert.AreEqual(_workingDir, command.WorkingDirectory);
            Assert.AreEqual(false, command.SpareChildProcesses);
        }

        [TestMethod]
        public void CanAllowExcludedExesFromValidation2()
        {
            var args = new string[] { "start", "dotnet", "--working-dir", _workingDir, "--exe-args", "run" };
            var command = args.Parse();
            Assert.IsNotNull(command);
            Assert.AreEqual("start", command.Type);
            Assert.AreEqual("dotnet", command.ExecutablePath);
            Assert.AreEqual(_workingDir, command.WorkingDirectory);
            Assert.AreEqual("run", command.ExecutableArgs);
            Assert.AreEqual(false, command.SpareChildProcesses);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidExecutablePathException))]
        public void CanIdentifyInvalidExes()
        {
            var args = new string[] { "start", "app.exe" };
            var command = args.Parse();
        }
    }
}
