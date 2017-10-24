using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MyClasses;
using System.Configuration;
using System.IO;

namespace MyClassesTest
{
    [TestClass]
    public class FileProcessTest
    {
        private const string _badFileName = @"C:\RandomFileToForceError";
        private string _goodFileName;

        public TestContext TestContext { get; set; }

        /// <summary>
        /// Stub methods to fill later
        /// </summary>
        [TestMethod]
        public void FileNameDoesExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            SetGoodFileName();
            TestContext.WriteLine("Creating the File " + _goodFileName);
            File.AppendAllText(_goodFileName, "test string");

            TestContext.WriteLine("Creating the File " + _goodFileName);
            fromCall = fp.FileExists(_goodFileName);
            Assert.IsTrue(fromCall);

            TestContext.WriteLine("Deleting the File " + _goodFileName);
            File.Delete(_goodFileName);
        }

        [TestMethod]
        public void FileNameDoesNotExist()
        {
            FileProcess fp = new FileProcess();
            bool fromCall;

            fromCall = fp.FileExists(_badFileName);

            Assert.IsFalse(fromCall);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void FileNameIsNullOrEmpty_ThrowArgumentNullException()
        {
            FileProcess fp = new FileProcess();

            fp.FileExists("");
        }

        [TestMethod]
        public void FileNameIsNullOrEmpty_ThrowArgumentNullException_UsingTryCatch()
        {
            FileProcess fp = new FileProcess();

            try
            {
                fp.FileExists("");
            }
            catch(ArgumentNullException)
            {
                //The test was a success
                return;
            }

            Assert.Fail("Call to FileExists did not throw the expected exception");
        }

        private void SetGoodFileName()
        {
            _goodFileName = ConfigurationManager.AppSettings["GoodFileName"];
            if (_goodFileName.Contains("[AppPath]"))
            {
                _goodFileName = _goodFileName.Replace("[AppPath]",
                       Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData));
            }
        }
    }
}
