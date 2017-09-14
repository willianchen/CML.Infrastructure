using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using CML.Infrastructure.Extension;
using CML.Infrastructure.Result;

namespace CML.Resposity.Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            DemoResposity demoRes = new Test.DemoResposity();
            //Parallel.For(0, 100, (i) =>
            //{
            //    DemoInfo demo = new Test.DemoInfo();
            //    demo.FAge = i;
            //    demo.FName = "test name"+i;
            //    demo.FBirthDay = DateTime.Now;


            //    demoRes.Insert(demo, new string[] { "FID" });
            //});

            //  demoRes.Delete(new { FID = 1 });
            //demoRes.Delete();
            #region 测试更新
            var d1 = demoRes.GetInfo(new { FID = 2 });
            Console.WriteLine(d1.ToJson());
            DemoInfo demo = new Test.DemoInfo();
            demo.FAge = 222;
            demo.FName = "update";
            demo.FBirthDay = DateTime.Now;
            demoRes.Update(demo, new { FID = 2 }, new string[] { "FID" });

            d1 = demoRes.GetInfo(new { FID = 2 });
            Console.WriteLine(d1.ToJson());
            #endregion

            var list = demoRes.QueryList();
     //       Console.WriteLine(list.ToJson());

            list = demoRes.QueryList(new { FID = 5 });
            Console.WriteLine(list.ToJson());

            PageResult<DemoInfo> l1 =(PageResult<DemoInfo>)demoRes.QueryPageList<DemoInfo>("FID,FName", "Demo", "1=1", "FID asc", 1, 2);
            Console.WriteLine(l1.Data.ToJson());
        }
    }
}
