using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MB.Util.Test
{
    [TestClass]
    public class VolPlayHelpTester
    {
        [TestMethod]
        public void TestPlayVol()
        {
            VolumPlayHelper.NewInstance.PlayVol("Cloud");
        }
    }
}
