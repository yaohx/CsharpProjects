using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MB.Util.Validated;

namespace testUtil
{
    class testDataValidated
    {
        private static PropertyValidates<myDataInfo> _DATA_REGISTER = new PropertyValidates<myDataInfo>(true);

        public static void TestMain() {
            _DATA_REGISTER.Add(o => o.Name, "名称", 10, true);
            _DATA_REGISTER.Add(o => o.Code, "编码", 6, false);
            _DATA_REGISTER.Add(o => o.Amount, "金额", 60, 0, 2);

            myDataInfo info = new myDataInfo();
            info.Name = "32412";
            info.Code = "001";
            info.Amount = 100.22m;

            
            try {
                //MB.Bulk.Common.DataValidated.ValidatedHelper.DataValidated(_DATA_REGISTER["Code"], info.Code);
                //MB.Bulk.Common.DataValidated.ValidatedHelper.DataValidated(_DATA_REGISTER["Name"], info.Name);
                MB.Util.Validated.ValidatedHelper.EntityDataValidated<myDataInfo>(_DATA_REGISTER, info);
            }
            catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }
            Console.ReadLine();
        }
    }

    class myDataInfo
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Address { get; set; }
        public DateTime DateTime { get; set; }
        public decimal Amount { get; set; }
    }
}
